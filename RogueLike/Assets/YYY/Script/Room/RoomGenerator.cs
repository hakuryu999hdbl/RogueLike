using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class RoomGenerator : MonoBehaviour
{
    //枚举
    public enum Direction { up, down, left, right };//up0,down1,left3,right4;
    public Direction direction;

    [Header("房间信息")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor, endColor;
    private GameObject endRoom;//最远的房间

    [Header("位置控制")]
    public Transform generatorPoint;
    public float xOffset;
    public float yOffset;
    public LayerMask roomLayer;//需要判断是否房间重叠，检测room层
    public int maxStep;

    public List<Room> rooms = new List<Room>();

    //筛选最适合做Boss战房间
    List<GameObject> farRooms = new List<GameObject>();
    List<GameObject> lessFarRooms = new List<GameObject>();
    List<GameObject> oneWayRooms = new List<GameObject>();


    //房间预设体
    GameObject L, R, U, B, LU, LR, LB, UR, UB, RB, LUR, LUB, URB, LRB, LURB;
    public WallType wallType;



    // Start is called before the first frame update
    void Start()
    {
        //随机天气
        //SkyBoxNumber = Random.Range(0, 4);
        SetFog();

        //随机房间大小
        //roomNumber = Random.Range(4, 8);
        //roomNumber = 1;

        ChoosePlace();

        Invoke("Scan", 0.2f);//这样因该能等到全部生成完

        //结局CG赛琳娜王座
        if (roomNumber == -4)
        {
            Instantiate(CG_SeleneThrone, new Vector3(0, 0, 0), Quaternion.identity);

            cg_Manager = CG_SeleneThrone.GetComponent<CG_Manager>();

            return;
        }

        //结局CG腐蝕胎巢
        if (roomNumber == -3)
        {
            Instantiate(CG_CorruptedWombNest, new Vector3(0, 0, 0), Quaternion.identity);

            cg_Manager = CG_CorruptedWombNest.GetComponent<CG_Manager>();

            return;
        }


        //结局CG惩戒修会圣堂
        if (roomNumber == -2)
        {
            Instantiate(CG_Chapel, new Vector3(0, 0, 0), Quaternion.identity);

            cg_Manager = CG_Chapel.GetComponent<CG_Manager>();

            return;
        }

        //结局CG惩戒回廊
        if (roomNumber == -1)
        {
            Instantiate(CG_HallofDiscipline, new Vector3(0, 0, 0), Quaternion.identity);

            cg_Manager = CG_HallofDiscipline.GetComponent<CG_Manager>();

            return;
        }


        //一般CG的调教室
        if (roomNumber == 0)
        {
            Instantiate(CG_Torture, new Vector3(0, 0, 0), Quaternion.identity);

            return;
        }

        //只有一个房间的时候（结局CG游街处刑广场）
        if (roomNumber == 1) 
        { 
            Instantiate(CG_ExecutionSquare, new Vector3(0, 0, 0), Quaternion.identity);

            cg_Manager = CG_ExecutionSquare.GetComponent<CG_Manager>();

            return;
        }

        if (roomNumber == 8) { Instantiate(BossRoom_Captain, new Vector3(0, 0, 0), Quaternion.identity); return; }//卫兵队长Boss房
        if (roomNumber == 9) { Instantiate(BossRoom_Selene, new Vector3(0, 0, 0), Quaternion.identity); return; }//王女Boss房
        if (roomNumber == 10) { Instantiate(BossRoom_Morgan, new Vector3(0, 0, 0), Quaternion.identity); return; }//宰相Boss房
        if (roomNumber == 11) { Instantiate(BossRoom_Alexis, new Vector3(0, 0, 0), Quaternion.identity); return; }//皇太子Boss房
        if (roomNumber == 12) { Instantiate(BossRoom_Dominus, new Vector3(0, 0, 0), Quaternion.identity); return; }//皇帝Boss房

        if (roomNumber == 13) { Instantiate(Room_Arena, new Vector3(0, 0, 0), Quaternion.identity); return; }//角斗场

        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<Room>());//将生成的房间添加到列表
            ChangePointPos();//每生成后转移位置
        }

        rooms[0].GetComponent<Room>().SetStartRoom();
        //rooms[roomNumber - 1].GetComponent<SpriteRenderer>().color = endColor;

        endRoom = rooms[0].gameObject;
        foreach (var room in rooms)//检测每个房间
        {
            if (room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)//比较二者距离
            {
                endRoom = room.gameObject;
            }

            SetupRoom(room, room.transform.position);
        }
        FindEndRoom();
        endRoom.GetComponent<Room>().SetEndRoom();








        // 将玩家初始所在的房间设置为房间列表中的第一个房间
        playerRoom = rooms[0].GetComponent<Room>();
        // 游戏一开始RoomGenerator直接把当前所有房间的位置告诉Move_Target
        foreach (var room in rooms)
        {
            roomPositions.Add(room.transform.position);
        }










        Invoke("PlayRegionBGM", 0.3f);//让主菜单的音乐先行






        //Invoke("SetEnemy", 1f);
        //Invoke("SetEnemy", 1.5f);
        //Invoke("SetEnemy", 2f);
        //Invoke("SetEnemy", 2.5f);
        //Invoke("SetEnemy", 3f);
        //Invoke("SetEnemy", 3.5f);
        //
        //Invoke("SetFriend", 6f);






    }






    /// <summary>
    /// 设置寻路，离开场景进入地图标准与寻路
    /// </summary>
    #region
    [Header("设置寻路")]
    public AstarPath AstarPath;
    void Scan()
    {
        AstarPath.Scan();
    }



    #endregion


    /// <summary>
    /// 雾气与天气
    /// </summary>
    #region
    [Header("雾气与天气")]
    public SkyboxSample SkyboxSample;
    public int SkyBoxNumber;
    public void SetFog()
    {



        // 启用线性雾效
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;

        // 设置雾效开始和结束距离
        RenderSettings.fogStartDistance = 0f;
        RenderSettings.fogEndDistance = 100f;



        switch (SkyBoxNumber)
        {
            case 0:
                RenderSettings.fog = false;
                SkyboxSample.Night();

                break;
            case 1:
                RenderSettings.fog = false;
                SkyboxSample.Day();
                break;
            case 2:
                SkyboxSample.WhiteSky();
                RenderSettings.fogColor = Color.gray;

                RenderSettings.fogStartDistance = 0f;           // 开始出现雾
                RenderSettings.fogEndDistance = 50f;             // 完全被雾覆盖

                // 设置雾效模式和浓度（电脑版打开）
                //RenderSettings.fogMode = FogMode.ExponentialSquared; // 使用指数雾
                //RenderSettings.fogDensity = 0.05f; // 雾的浓度（根据需要调整）
                //RenderSettings.fogColor = new Color(0.8f, 0.8f, 0.8f); // 稍深的灰色

                break;
            case 3:      
                SkyboxSample.RedSky();
                RenderSettings.fogColor = Color.red;


                RenderSettings.fogStartDistance = 0f;           // 开始出现雾
                RenderSettings.fogEndDistance = 50f;             // 完全被雾覆盖

                break;
        }

    }
    #endregion





    /// <summary>
    ///  生成房间
    /// </summary>
    #region
    [Header("房间类型")]
    public int RoomType;//0监狱  1地牢
    public void ChoosePlace()
    {
        switch (RoomType)
        {
            case 0:
                L = wallType.singleLeft;
                R = wallType.singleRight;
                U = wallType.singleUp;
                B = wallType.singleBottom;

                LU = wallType.doubleLU;
                LR = wallType.doubleLR;
                LB = wallType.doubleLB;

                UR = wallType.doubleUR;
                UB = wallType.doubleUB;
                RB = wallType.doubleRB;

                LUR = wallType.tripleLUR;
                LUB = wallType.tripleLUB;
                URB = wallType.tripleURB;
                LRB = wallType.tripleLRB;

                LURB = wallType.fourDoors;
                break;

            case 1:
                L = wallType.Dungeon_singleLeft;
                R = wallType.Dungeon_singleRight;
                U = wallType.Dungeon_singleUp;
                B = wallType.Dungeon_singleBottom;

                LU = wallType.Dungeon_doubleLU;
                LR = wallType.Dungeon_doubleLR;
                LB = wallType.Dungeon_doubleLB;

                UR = wallType.Dungeon_doubleUR;
                UB = wallType.Dungeon_doubleUB;
                RB = wallType.Dungeon_doubleRB;

                LUR = wallType.Dungeon_tripleLUR;
                LUB = wallType.Dungeon_tripleLUB;
                URB = wallType.Dungeon_tripleURB;
                LRB = wallType.Dungeon_tripleLRB;

                LURB = wallType.Dungeon_fourDoors;
                break;



        }


    }//选择关卡

    public void ChangePointPos()
    {
        direction = (Direction)Random.Range(0, 4);

        do
            switch (direction)
            {
                case Direction.up://当方向是向上的时候
                    generatorPoint.position += new Vector3(0, yOffset, 0);
                    break;
                case Direction.down://当方向是向下的时候
                    generatorPoint.position += new Vector3(0, -yOffset, 0);
                    break;
                case Direction.left://当方向是向左的时候
                    generatorPoint.position += new Vector3(-xOffset, 0, 0);
                    break;
                case Direction.right://当方向是向右的时候
                    generatorPoint.position += new Vector3(xOffset, 0, 0);
                    break;
            } while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, roomLayer));
    }//更改生成点（随机方向移动位置

    public void SetupRoom(Room newRoom, Vector3 roomPosition)
    {
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yOffset, 0), 0.2f, roomLayer);//当前房间的位置加上向上位移
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yOffset, 0), 0.2f, roomLayer);//当前房间的位置加上向下位移
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0, 0), 0.2f, roomLayer);//当前房间的位置加上向左位移
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0, 0), 0.2f, roomLayer);//当前房间的位置加上向右位移

        newRoom.UpdateRoom();

        switch (newRoom.doorNumber)
        {
            case 1:
                if (newRoom.roomUp)
                    Instantiate(U, roomPosition, Quaternion.identity);
                if (newRoom.roomDown)
                    Instantiate(B, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft)
                    Instantiate(L, roomPosition, Quaternion.identity);
                if (newRoom.roomRight)
                    Instantiate(R, roomPosition, Quaternion.identity);
                break;
            case 2:
                if (newRoom.roomLeft && newRoom.roomUp)
                    Instantiate(LU, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomRight)
                    Instantiate(LR, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomDown)
                    Instantiate(LB, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomRight)
                    Instantiate(UR, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomDown)
                    Instantiate(UB, roomPosition, Quaternion.identity);
                if (newRoom.roomRight && newRoom.roomDown)
                    Instantiate(RB, roomPosition, Quaternion.identity);
                break;
            case 3:
                if (newRoom.roomLeft && newRoom.roomUp && newRoom.roomRight)
                    Instantiate(LUR, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomRight && newRoom.roomDown)
                    Instantiate(LRB, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomRight && newRoom.roomDown)
                    Instantiate(URB, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomUp && newRoom.roomDown)
                    Instantiate(LUB, roomPosition, Quaternion.identity);
                break;
            case 4:
                if (newRoom.roomLeft && newRoom.roomUp && newRoom.roomRight && newRoom.roomDown)
                    Instantiate(LURB, roomPosition, Quaternion.identity);
                break;
        }
    }//检测上下左右有没有房间

    public void FindEndRoom()
    {
        //最大数值 最远距离数字
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].stepToStart > maxStep)
                maxStep = rooms[i].stepToStart;
        }
        //获得最远房间和第二远
        foreach (var room in rooms)
        {
            if (room.stepToStart == maxStep)
                farRooms.Add(room.gameObject);
            if (room.stepToStart == maxStep - 1)
                lessFarRooms.Add(room.gameObject);
        }
        for (int i = 0; i < farRooms.Count; i++)
        {
            if (farRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(farRooms[i]);//最远房间里的单侧门加入
        }

        for (int i = 0; i < lessFarRooms.Count; i++)
        {
            if (lessFarRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(lessFarRooms[i]);//第二远远房间里的单侧门加入
        }
        if (oneWayRooms.Count != 0)
        {
            endRoom = oneWayRooms[Random.Range(0, oneWayRooms.Count)];
        }
        else
        {
            endRoom = farRooms[Random.Range(0, farRooms.Count)];
        }
    }//检测最远房间
    #endregion

    /// <summary>
    /// 房间实体
    /// </summary>
    #region
    [Header("固定地图")]
    public GameObject CG_Chapel;//惩戒修会圣堂
    public GameObject CG_HallofDiscipline;//惩戒回廊
    public GameObject CG_ExecutionSquare;//处刑广场
    public GameObject CG_Torture;//调教室
    public GameObject CG_CorruptedWombNest;//腐蝕胎巢
    public GameObject CG_SeleneThrone;//赛琳娜王座

    public GameObject BossRoom_Captain;//卫兵队长Boss战
    public GameObject BossRoom_Selene;//王女Boss战
    public GameObject BossRoom_Morgan;//宰相Boss战
    public GameObject BossRoom_Alexis;//魔族化王女和魔族化皇太子Boss战
    public GameObject BossRoom_Dominus;//魔族化皇帝Boss战

    public GameObject Room_Arena;//一对一角斗场

    [System.Serializable]//即使没有挂MonoBehaviour可以被系统识别
    public class WallType
    {



        public GameObject
            singleLeft, singleRight, singleUp, singleBottom,
            doubleLU, doubleLR, doubleLB, doubleUR, doubleUB, doubleRB,
            tripleLUR, tripleLUB, tripleURB, tripleLRB,
            fourDoors,

             Dungeon_singleLeft, Dungeon_singleRight, Dungeon_singleUp, Dungeon_singleBottom,
             Dungeon_doubleLU, Dungeon_doubleLR, Dungeon_doubleLB, Dungeon_doubleUR, Dungeon_doubleUB, Dungeon_doubleRB,
             Dungeon_tripleLUR, Dungeon_tripleLUB, Dungeon_tripleURB, Dungeon_tripleLRB,
             Dungeon_fourDoors;

    }
    #endregion





    /// <summary>
    ///  任何物体的随机端口,传送到随机房间/距离玩家最远的房间
    /// </summary>
    #region
    [Header("设置随机端口终点")]
    public GameObject _Player;//玩家
    // 定义偏移量范围
    public float offsetRange = 2.0f;


    public void ChangeTargetPlace(GameObject MoveTarget, int WhichRoom)//-2玩家一模一样位置 -1玩家位置附近 0 玩家当前房间  1距离玩家最远房间  2除了玩家之外的随机房间  3随机找一个房间
    {



        //敌人死亡后再次选择最近的
        //Invoke("CheckNearestEnemy", 0.1f);//要让敌人重刷之后，过一会再触发

        // 生成随机偏移量
        float offsetX = Random.Range(-offsetRange, offsetRange);
        float offsetY = Random.Range(-offsetRange, offsetRange);



        switch (WhichRoom)
        {

            case -2:

                //拉到玩家当前一模一样的位置
                MoveTarget.transform.position = _Player.transform.position;
                break;
            case -1:

                //拉到玩家当前位置
                MoveTarget.transform.position = _Player.transform.position + new Vector3(offsetX, offsetY, 0f);
                break;
            case 0:

                //拉到玩家当前房间
                MoveTarget.transform.position = playerRoom.transform.position + new Vector3(offsetX, offsetY, 0f);
                break;
            case 1:

                // 找到距离玩家最远的房间
                Vector3 farthestRoomPosition = FindFarthestRoomFromPlayer();

                MoveTarget.transform.position = farthestRoomPosition += new Vector3(offsetX, offsetY, 0f);
                break;
            case 2:

                // 随机找一个非玩家所在的房间
                MoveTarget.transform.position = FindRandomRoomExceptPlayerRoom() + new Vector3(offsetX, offsetY, 0f);
                break;
            case 3:

                //随机找一个房间
                int randomIndex = Random.Range(0, roomPositions.Count);
                MoveTarget.transform.position = roomPositions[randomIndex] += new Vector3(offsetX, offsetY, 0f);

                break;

        }




    }

    //将玩家拉到房间中央
    public void SetPlayerToRoomCenter()
    {
        _Player.transform.position = playerRoom.transform.position;
    }



    Vector3 FindFarthestRoomFromPlayer()
    {

        if (roomPositions == null || roomPositions.Count == 0)
        {
            Debug.LogError("房间列表未初始化或为空，无法找到随机房间！");
            return Vector3.zero; // 返回一个默认值，防止报错
        }//如果没有一个房间的话就没有办法确认玩家不在的房间


        Vector3 playerPosition = _Player.transform.position;
        Vector3 farthestRoomPosition = Vector3.zero;
        float maxDistance = float.MinValue;

        // 遍历所有房间，找到距离玩家最远的房间
        foreach (var room in rooms)
        {
            float distanceToPlayer = Vector3.Distance(room.transform.position, playerPosition);

            if (distanceToPlayer > maxDistance)
            {
                maxDistance = distanceToPlayer;
                farthestRoomPosition = room.transform.position;
            }
        }

        return farthestRoomPosition;
    }//获取距离玩家最远的房间




    //知晓所有房间的位置列表
    public List<Vector3> roomPositions = new List<Vector3>();
    Vector3 FindRandomRoomExceptPlayerRoom()
    {
        List<Vector3> availableRooms = new List<Vector3>(roomPositions);

        // 移除玩家所在的房间
        availableRooms.Remove(playerRoom.transform.position);

        // 在剩下的房间中随机选择
        int randomIndex = Random.Range(0, availableRooms.Count);
        return availableRooms[randomIndex];
    }   // 随机找一个不是玩家所在的房间




    //各个房间的WallMap传送自己坐标给RoomGenerator告诉玩家所处房间
    public Room playerRoom; // 玩家当前所在的房间            
    public void SetPlayerRoom(Vector3 roomPosition)
    {
        foreach (var room in rooms)
        {
            if (room.transform.position == roomPosition)
            {
                playerRoom = room;
                break;
            }
        }
    }  // 设置玩家所在的房间 
    #endregion




    /// <summary>
    /// 设置敌人队友RBQ
    /// </summary>
    #region
    [Header("设置敌人队友RBQ")]
    public GameObject Enemy;
    public GameObject RBQ;
    public GameObject MagicCircle;
    public GameObject Tentacle;
    public GameObject Item;


    public void SetEnemy()
    {


        GameObject NewEnemy = Instantiate(Enemy, transform.position, Quaternion.identity);
     

        ChangeTargetPlace(NewEnemy, -2);
    }
    public void SetFriend(int EnemyClass=0)//默认女孩
    {

        GameObject NewEnemy = Instantiate(Enemy, transform.position, Quaternion.identity);
     


        Enemy enemy = NewEnemy.transform.Find("Enemy").GetComponent<Enemy>();
        switch (EnemyClass) 
        {
            case 0:
                enemy.BecomeSoldier_Girl=true;
                break;

            case 1:
                enemy.BecomeSoldier_Man = true;
                break;
        }
        enemy.ConvertToFriend();


        ChangeTargetPlace(NewEnemy, -2);
    }



    #endregion


    /// <summary>
    /// 基于所有存档（排除当前操纵的存档）批量生成队友并应用数据
    /// </summary>
    #region
    public void SetAllFriends()
    {
        //（排除当前操纵的存档）批量生成队友
        SpawnFriendsFromOtherSaves(_Player.GetComponent<Player>().currentSaveName);
    }
    public void SpawnFriendsFromOtherSaves(string currentPlayerName)
    {
        var others = SaveManager.LoadAllSavesExcept(currentPlayerName);
        if (others == null || others.Count == 0)
        {
            Debug.Log("[SpawnFriendsFromOtherSaves] 没有可用的其他存档。");
            return;
        }

        foreach (var data in others)
        {
            // 1) 生成实体
            GameObject newGO = Instantiate(Enemy, transform.position, Quaternion.identity);

            // 2) 取到内部的 Enemy 组件
            Enemy enemy = newGO.transform.Find("Enemy").GetComponent<Enemy>();
            if (enemy == null)
            {
                Debug.LogError("生成的对象上找不到 Enemy 组件（路径 'Enemy'）。");
                Destroy(newGO);
                continue;
            }

            // 3) 转为友军
            enemy.ConvertToFriend();

            // 4) 套用该存档数据（皮肤/数值/武器 等）
            enemy.ApplySaveData(data);

            // 5) 放到合适的跟随/分组位置
            ChangeTargetPlace(newGO, -1);
        }
    }
    #endregion


    /// <summary>
    /// CG结局 场景调用专项
    /// </summary>
    #region
    [Header("CG结局控制")]
    public CG_Manager cg_Manager;
    //RBQ列表
    public List<GameObject> RBQ_List = new List<GameObject>();


   


    public void DelayCreatSetFriend_RBQ() 
    {

        Invoke("SetFriend_RBQ_ToPosition", 1f);
        Invoke("Set_RBQ_ToPosition", 1.2f);
        Invoke("Set_RBQ_ToPosition", 1.5f);
        Invoke("Set_RBQ_ToPosition", 2f);
        Invoke("Set_RBQ_ToPosition", 2.5f);
        Invoke("Set_RBQ_ToPosition", 3f);
    }

  

    public void SetFriend_RBQ_ToPosition() 
    {
        var others = SaveManager.LoadAllSavesExcept(null);
        foreach (var data in others)
        {
            // 1) 生成实体
            GameObject newGO = Instantiate(Enemy, transform.position, Quaternion.identity);

            // 2) 取到内部的 Enemy 组件
            Enemy enemy = newGO.transform.Find("Enemy").GetComponent<Enemy>();
            if (enemy == null)
            {
                Debug.LogError("生成的对象上找不到 Enemy 组件（路径 'Enemy'）。");
                Destroy(newGO);
                continue;
            }

            // 3) 转为友军
            enemy.ConvertToFriend();

            // 4) 套用该存档数据（皮肤/数值/武器 等）
            enemy.ApplySaveData(data);

            // 5) 放到合适的跟随/分组位置
            // 生成随机偏移量
            float offsetX = Random.Range(-offsetRange, offsetRange);
            float offsetY = Random.Range(-offsetRange, offsetRange);

            //拉到指定位置
            enemy.transform.position = cg_Manager.Camera_Position_1.transform.position + new Vector3(offsetX, offsetY, 0f);

            //显示RBQ被抓着头发走
            enemy.CG_End_RBQ_Man_CarryUp();//生成存档内人数

            //拉入列表方便集体转移位置
            RBQ_List.Add(newGO);

           
        }
    }
    public void Set_RBQ_ToPosition() 
    {
        GameObject NewEnemy = Instantiate(Enemy, transform.position, Quaternion.identity);



        Enemy enemy = NewEnemy.transform.Find("Enemy").GetComponent<Enemy>();
        enemy.BecomeSoldier_Girl = true;
        enemy.ConvertToFriend();

        // 5) 放到合适的跟随/分组位置
        // 生成随机偏移量
        float offsetX = Random.Range(-offsetRange, offsetRange);
        float offsetY = Random.Range(-offsetRange, offsetRange);

        //拉到指定位置
        enemy.transform.position = cg_Manager.Camera_Position_1.transform.position + new Vector3(offsetX, offsetY, 0f);

        //显示RBQ被抓着头发走
        enemy.CG_End_RBQ_Man_CarryUp();//生成凑人数

        //拉入列表方便集体转移位置
        RBQ_List.Add(NewEnemy);
    }//为了让RBQ数量看起来更加充实一些


    public void DestoryAll_RBQ() 
    {
        // 逐个销毁
        foreach (GameObject obj in RBQ_List)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        // 清空列表
        RBQ_List.Clear();
    }

    public void ArrangeRBQ()
    {
        // 清理掉所有 null 槽
        RBQ_List.RemoveAll(item => item == null);

        // 遍历 RBQ_List，按点位排布
        for (int i = 0; i < RBQ_List.Count; i++)
        {
            if (i < cg_Manager.RBQ_Positions.Count) // 有对应点位
            {
                //RBQ_List[i].transform.position = cg_Manager.RBQ_Positions[i].position;


                Enemy enemy = RBQ_List[i].transform.Find("Enemy").GetComponent<Enemy>();
                enemy.CG_End_RBQ_Pillory(0);//先动画，再移位，再定死无法走

                enemy.gameObject.transform.position = cg_Manager.RBQ_Positions[i].position;


            }
            else
            {
                // 超过点位数量的多余物体销毁
                if (RBQ_List[i] != null)
                {
                    Destroy(RBQ_List[i]);
                }
            }
        }

        // 移除多余引用
        if (RBQ_List.Count > cg_Manager.RBQ_Positions.Count)
        {
            RBQ_List.RemoveRange(cg_Manager.RBQ_Positions.Count, RBQ_List.Count - cg_Manager.RBQ_Positions.Count);
        }
    }//先动画，再移位，再定死无法走

    public void SetRBQSide() 
    {
        foreach (GameObject obj in RBQ_List)
        {
            if (obj != null)
            {
                Enemy enemy = obj.transform.Find("Enemy").GetComponent<Enemy>();

                enemy.CG_End_RBQ_Pillory(1);//全体设置为侧面

            }
        }

    }//全体设置为侧面
    public void SetRBQFront()
    {
        foreach (GameObject obj in RBQ_List)
        {
            if (obj != null)
            {
                Enemy enemy = obj.transform.Find("Enemy").GetComponent<Enemy>();

                enemy.CG_End_RBQ_Pillory(0);//全体设置为正面

            }
        }

    }//全体设置为正面

    public void SetRBQFront2()
    {
        foreach (GameObject obj in RBQ_List)
        {
            if (obj != null)
            {
                Enemy enemy = obj.transform.Find("Enemy").GetComponent<Enemy>();

                enemy.CG_End_RBQ_Pillory(2);//全体设置为正面（强奸）

            }
        }

    }//全体设置为正面（强奸）





    #endregion



    /// <summary>
    /// 关卡信息
    /// </summary>
    #region
    [Header("关卡信息")]
    public GameObject BossIcon;
    public GameObject Stage_Information;
    public Text _Stage_Information;


    public void ShowInformationOfStage(int Information)//-3已达最大奴隶数量！ -2金币不够  -1敌人增援   0新的CG解锁    1敌人出现（锁门）   2敌人消灭（开门    3请先创建人物    4关卡尚未解锁     5战斗中无法打开菜单    6移动中无法打开菜单  7新的游戏模式解锁  8此模式下无法打开菜单  9第X波   10角斗场最高记录    11地下城当高纪录    12地下城当前记录终结，最高纪录   13新的可选种族解锁
    {
        switch (Information) 
        {
            case -3: // 已达最大奴隶数量！
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        _Stage_Information.text = "奴隷の最大数に達しました！";
                        break;
                    case 1: // 简体
                        _Stage_Information.text = "已达最大奴隶数量！";
                        break;
                    case 2: // 繁体
                        _Stage_Information.text = "已達最大奴隸數量！";
                        break;
                    case 3: // 英语
                        _Stage_Information.text = "Maximum slave capacity reached!";
                        break;
                    case 4: // 韩语
                        _Stage_Information.text = "노예의 최대 수에 도달했습니다!";
                        break;
                }

                break;

            case -2: // 金币不够
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        _Stage_Information.text = "金貨が足りません！";
                        break;
                    case 1: // 简体
                        _Stage_Information.text = "金币不足！";
                        break;
                    case 2: // 繁体
                        _Stage_Information.text = "金幣不足！";
                        break;
                    case 3: // 英语
                        _Stage_Information.text = "Not enough gold!";
                        break;
                    case 4: // 韩语
                        _Stage_Information.text = "골드가 부족합니다!";
                        break;
                }

                break;


            case -1: // 敌人增援
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        _Stage_Information.text = "敵の増援";
                        break;
                    case 1: // 简体
                        _Stage_Information.text = "敌人增援";
                        break;
                    case 2: // 繁体
                        _Stage_Information.text = "敵人增援";
                        break;
                    case 3: // 英语
                        _Stage_Information.text = "Enemy Reinforcements";
                        break;
                    case 4: // 韩语
                        _Stage_Information.text = "적 증원군";
                        break;
                }
                BossIcon.SetActive(true);
                break;


            case 0:

                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        _Stage_Information.text = "新しいCGが解放されました";
                        break;
                    case 1: // 简体
                        _Stage_Information.text = "新的CG解锁";
                        break;
                    case 2: // 繁体
                        _Stage_Information.text = "新的CG解鎖";
                        break;
                    case 3: // 英语
                        _Stage_Information.text = "New CG Unlocked";
                        break;
                    case 4: // 韩语
                        _Stage_Information.text = "새 CG 해금";
                        break;
                }


                break;

            case 1: // 敌人出现
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "敵出現";   // 日语
                        break;
                    case 1:
                        _Stage_Information.text = "敌人出现"; // 简体
                        break;
                    case 2:
                        _Stage_Information.text = "敵人出現"; // 繁体
                        break;
                    case 3:
                        _Stage_Information.text = "Enemy Appears"; // 英语
                        break;
                    case 4:
                        _Stage_Information.text = "적 등장"; // 韩语
                        break;
                }
                BossIcon.SetActive(true);
                break;

            case 2: // 清剿完毕
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "掃討完了"; // 日语
                        break;
                    case 1:
                        _Stage_Information.text = "清剿完毕"; // 简体
                        break;
                    case 2:
                        _Stage_Information.text = "清剿完畢"; // 繁体
                        break;
                    case 3:
                        _Stage_Information.text = "Cleared"; // 英语
                        break;
                    case 4:
                        _Stage_Information.text = "소탕 완료"; // 韩语
                        break;
                }
                break;

            case 3: // 请先创建人物
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "キャラを作成してください"; // 日语
                        break;
                    case 1:
                        _Stage_Information.text = "请先创建人物"; // 简体
                        break;
                    case 2:
                        _Stage_Information.text = "請先創建人物"; // 繁体
                        break;
                    case 3:
                        _Stage_Information.text = "Create a Character First"; // 英语
                        break;
                    case 4:
                        _Stage_Information.text = "캐릭터를 먼저 생성하세요"; // 韩语
                        break;
                }
                break;

            case 4: // 关卡尚未解锁
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "ステージ未解放"; // 日语
                        break;
                    case 1:
                        _Stage_Information.text = "关卡尚未解锁"; // 简体
                        break;
                    case 2:
                        _Stage_Information.text = "關卡尚未解鎖"; // 繁体
                        break;
                    case 3:
                        _Stage_Information.text = "Stage Locked"; // 英语
                        break;
                    case 4:
                        _Stage_Information.text = "스테이지 잠금"; // 韩语
                        break;
                }
                break;


            case 5: // 战斗中无法打开菜单
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "戦闘中はメニューを開けません"; // 日语
                        break;
                    case 1:
                        _Stage_Information.text = "战斗中无法打开菜单"; // 简体
                        break;
                    case 2:
                        _Stage_Information.text = "戰鬥中無法打開選單"; // 繁体
                        break;
                    case 3:
                        _Stage_Information.text = "Cannot open menu in battle"; // 英语
                        break;
                    case 4:
                        _Stage_Information.text = "전투 중 메뉴를 열 수 없습니다"; // 韩语
                        break;
                }
                break;

            case 6: // XX中无法打开菜单 
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        _Stage_Information.text = "移動・攻撃・回避中はメニューを開けません。";
                        break;
                    case 1: // 简体中文
                        _Stage_Information.text = "移动、攻击或闪避中无法打开菜单";
                        break;
                    case 2: // 繁体中文
                        _Stage_Information.text = "移動、攻擊或閃避中無法開啟選單";
                        break;
                    case 3: // 英语
                        _Stage_Information.text = "You cannot open the menu while moving, attacking, or dodging.";
                        break;
                    case 4: // 韩语
                        _Stage_Information.text = "이동·공격·회피 중에는 메뉴를 열 수 없습니다.";
                        break;
                }
                break;

            case 7: //新的游戏模式解锁 
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "新しいゲームモードが解放されました"; // 日语
                        break;
                    case 1:
                        _Stage_Information.text = "新的游戏模式解锁"; // 简体
                        break;
                    case 2:
                        _Stage_Information.text = "新的遊戲模式解鎖"; // 繁体
                        break;
                    case 3:
                        _Stage_Information.text = "New Game Mode Unlocked"; // 英语
                        break;
                    case 4:
                        _Stage_Information.text = "새 게임 모드 해금"; // 韩语
                        break;
                }
                break;

            case 8: // 此模式下无法打开菜单
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "このモードではメニューを開けません"; // 日语
                        break;
                    case 1:
                        _Stage_Information.text = "此模式下无法打开菜单"; // 简体
                        break;
                    case 2:
                        _Stage_Information.text = "此模式下無法打開選單"; // 繁体
                        break;
                    case 3:
                        _Stage_Information.text = "Cannot open menu in this mode"; // 英语
                        break;
                    case 4:
                        _Stage_Information.text = "이 모드에서는 메뉴를 열 수 없습니다"; // 韩语
                        break;
                }
                break;

            case 9: // 第X波敌人
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        _Stage_Information.text = "第" + (GameFlowData.RoomLevel + 1) + "波の敵";
                        break;
                    case 1: // 简体中文
                        _Stage_Information.text = "第" + (GameFlowData.RoomLevel + 1) + "波敌人";
                        break;
                    case 2: // 繁体中文
                        _Stage_Information.text = "第" + (GameFlowData.RoomLevel + 1) + "波敵人";
                        break;
                    case 3: // 英语
                        _Stage_Information.text = "Wave " + (GameFlowData.RoomLevel + 1);
                        break;
                    case 4: // 韩语
                        _Stage_Information.text = (GameFlowData.RoomLevel + 1) + "번째 웨이브";
                        break;
                }
                break;


            case 10: //角斗场最高记录

                int bestWave = PlayerPrefs.GetInt("Arena_Wave", 0);
                string colorTag = "#FFD700"; // 金黄色，可改为红色 "#FF4444"、蓝色 "#00BFFF" 等

                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        _Stage_Information.text = $"最高記録：第<color={colorTag}>{bestWave}</color>波";
                        break;
                    case 1: // 简体中文
                        _Stage_Information.text = $"最高纪录：第<color={colorTag}>{bestWave}</color>波";
                        break;
                    case 2: // 繁体中文
                        _Stage_Information.text = $"最高紀錄：第<color={colorTag}>{bestWave}</color>波";
                        break;
                    case 3: // 英语
                        _Stage_Information.text = $"Best Record: Wave <color={colorTag}>{bestWave}</color>";
                        break;
                    case 4: // 韩语
                        _Stage_Information.text = $"최고 기록: <color={colorTag}>{bestWave}</color>번째 웨이브";
                        break;
                }
                break;

            case 11: //地下城当前纪录 
                int currentStreak = PlayerPrefs.GetInt("Dungeon_Streak", 0);
                string colorA = "#FFD700";

                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        _Stage_Information.text = $"現在の地下城連勝：<color={colorA}>{currentStreak}</color>";
                        break;
                    case 1: // 简体中文
                        _Stage_Information.text = $"当前地下城连胜纪录：<color={colorA}>{currentStreak}</color>";
                        break;
                    case 2: // 繁体中文
                        _Stage_Information.text = $"當前地下城連勝紀錄：<color={colorA}>{currentStreak}</color>";
                        break;
                    case 3: // 英语
                        _Stage_Information.text = $"Current Dungeon Streak: <color={colorA}>{currentStreak}</color>";
                        break;
                    case 4: // 韩语
                        _Stage_Information.text = $"현재 던전 연승: <color={colorA}>{currentStreak}</color>";
                        break;
                }
                break;

            case 12: //地下城当前记录终结，最高纪录
                int streak = PlayerPrefs.GetInt("Dungeon_Streak", 0);
                int best2 = PlayerPrefs.GetInt("Dungeon_BestStreak", 0);
                string colorB = "#FFD700";

                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        _Stage_Information.text =
                            $"連勝 <color={colorB}>{streak}</color> で終結\n最高記録：<color={colorB}>{best2}</color> 連勝";
                        break;
                    case 1: // 简体中文
                        _Stage_Information.text =
                            $"本次连胜在 <color={colorB}>{streak}</color> 终结\n最高纪录：<color={colorB}>{best2}</color> 连胜";
                        break;
                    case 2: // 繁体中文
                        _Stage_Information.text =
                            $"本次連勝於 <color={colorB}>{streak}</color> 終結\n最高紀錄：<color={colorB}>{best2}</color> 連勝";
                        break;
                    case 3: // 英语
                        _Stage_Information.text =
                            $"Streak ended at <color={colorB}>{streak}</color>\nBest Record: <color={colorB}>{best2}</color>";
                        break;
                    case 4: // 韩语
                        _Stage_Information.text =
                            $"이번 연승은 <color={colorB}>{streak}</color>에서 종료\n최고 기록: <color={colorB}>{best2}</color>연승";
                        break;
                }

                break;

            case 13: //新的可选种族解锁
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        _Stage_Information.text = "新しい選択可能な種族が解放されました";
                        break;
                    case 1: // 简体
                        _Stage_Information.text = "新的可选种族解锁";
                        break;
                    case 2: // 繁体
                        _Stage_Information.text = "新的可選種族已解鎖";
                        break;
                    case 3: // 英语
                        _Stage_Information.text = "A new selectable race has been unlocked";
                        break;
                    case 4: // 韩语
                        _Stage_Information.text = "새로 선택할 수 있는 종족이 해금되었습니다";
                        break;
                }
                break;

        }


        Stage_Information.SetActive(true);

        //Debug.Log("显示信息");
    }



    #endregion

    /// <summary>
    /// 结算页面
    /// </summary>
    #region
    [Header("结算页面")]
    public GameObject ResultCavans;


    public void ShowResult()
    {

        MissionIcon(true);


        Invoke("TimeStop",0.2f);

        //让BGM停止好播放结尾音乐
        BGM.instance.Stop();

       
        StartCoroutine(ResultDetailDelay());


        switch (GameFlowData.nextScene) 
        {
            case "Story_01":
                UIManager.instance.Achieventment_ACH_FIRST_MISSION();//【初めての潜入】第1章クリア
                break;

            case "Story_03":
                UIManager.instance.Achieventment_ACH_UNLOCK_HIGHELF();//【高等精霊の目覚め】高等精霊のキャラクターを解放。【防止说DL版解锁后，Steam版再玩解锁不了】
                break;

            case "Story_05":
                UIManager.instance.Achieventment_ACH_BEAT_PRINCESS();//【王女との対峙】王女セリーネ撃破
                break;

            case "Story_12":
                UIManager.instance.Achieventment_ACH_SEE_TRUTH();//【真実の目撃者】帝国の正体を知る
                break;
        }




    }//获胜端口

    void TimeStop() 
    {
        Time.timeScale = 0f;
    }

    IEnumerator ResultDetailDelay()
    {
        // 使用 real-time，不受 TimeScale 影响
        yield return new WaitForSecondsRealtime(1f);

        ResultDetail();
    }
    void ResultDetail()
    {
        ResultCavans.SetActive(true);
      

        UIManager.instance.StageClean();
    }


    public Image MissionSuccess;
    public Sprite MissionSuccess_E, MissionSuccess_J, MissionSuccess_C, MissionFailure_E, MissionFailure_J, MissionFailure_C;

    public void MissionIcon(bool isWin)
    {
        UIManager.instance.player.isInputBlocked = true;//切断玩家的方向攻击等输入(在跳出一瞬间切断)
        

        if (isWin)
        {
            switch (PlayerPrefs.GetInt("language"))
            {
                case 0:
                case 2:
                    MissionSuccess.sprite = MissionSuccess_J;
                    break;
                case 1:
                    MissionSuccess.sprite = MissionSuccess_C;
                    break;
                case 3:
                case 4:
                    MissionSuccess.sprite = MissionSuccess_E;
                    break;
            }

            AudioManager.instance.AudioPlay(AudioManager.instance.SE_Win);
        }
        else
        {
            switch (PlayerPrefs.GetInt("language"))
            {
                case 0:
                case 2:
                    MissionSuccess.sprite = MissionFailure_J;
                    break;
                case 1:
                    MissionSuccess.sprite = MissionFailure_C;
                    break;
                case 3:
                case 4:
                    MissionSuccess.sprite = MissionFailure_E;
                    break;
            }

            AudioManager.instance.AudioPlay(AudioManager.instance.SE_Slap);
        }
        MissionSuccess.gameObject.SetActive(true);
    }

    #endregion
}

