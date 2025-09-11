using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMap : MonoBehaviour
{

    /// <summary>
    /// 尝试与Room取得联系
    /// </summary>
    #region
    Vector2Int GridPos => new Vector2Int(
   Mathf.RoundToInt(transform.position.x / Room.CellX),
   Mathf.RoundToInt(transform.position.y / Room.CellY)
);
    private Room linkedRoom;

    void SetBoss()
    {
        // 根据自己的网格坐标找到房间
        if (!RoomRegistry.TryGetRoom(GridPos, out linkedRoom))
        {
            Debug.LogWarning($"[WallMap] 找不到对应Room，Grid={GridPos} pos={transform.position}");
        }

        if (linkedRoom.roomType == Room.RoomType.Boss) 
        {
            Debug.Log("Boss房");
        }
    }
    #endregion


    /// <summary>
    /// 房间小地图显示
    /// </summary>
    #region
    GameObject mapSprite;
    RoomGenerator _RoomGenerator;//寻找RoomGenerator,用于传送自己坐标告知RoomGenerator玩家到哪个房间了

    private void Start()
    {
        SetBoss();

        _RoomGenerator = GameObject.FindGameObjectWithTag("RoomGenerator").GetComponent<RoomGenerator>();//寻找RoomGenerator


        // 开始时门是打开状态
        //UnLockRoom();
    }

    private void OnEnable()
    {
        mapSprite = transform.parent.GetChild(0).gameObject;//获取子物体

        Invoke("HideMap", 0.2f);


    }
    void HideMap()
    {

        if (!PlayerInRoom) { mapSprite.SetActive(false); }
        if (PlayerInRoom) { isClean = 2; }//这个是游戏开始的时候触发，玩家第一个房间自动清理干净
    }

    public void ShowMap()
    {
        mapSprite.SetActive(true);

    }//玩家进入地图柜子等显示

    #endregion












    /// <summary>
    /// 玩家进入离开/房间上锁解锁
    /// </summary>
    #region
    bool PlayerInRoom = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //ShowMap();
            PlayerInRoom = true;




            // 告知RoomGenerator玩家进入的房间位置
            Invoke("PlayerInThisRoomToRoomGenerator", 0.1f);
        }

        if (other.CompareTag("Friend")|| other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().wallmap = this;
            //Debug.Log("敌人队友读取wallmap");
        }//队友敌人立刻读取当下WallMap最新信息


    }//玩家进入显示房间小地图
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRoom = false;
        }


    }//玩家进入显示房间小地图
    void PlayerInThisRoomToRoomGenerator()
    {
        // 获取房间的位置
        Vector3 roomPosition = transform.position;
        _RoomGenerator.SetPlayerRoom(roomPosition);
        //Debug.Log("玩家所在房间位置"+roomPosition);

    }//稍微晚一点


    [Header("门动画列表")]
    public List<Gate> AllGate = new List<Gate>(); //当前房间里的所有门

    public int isClean = 0;//0未打开  1刷敌   2清零

    public void LockRoom()
    {
        ShowMap();//玩家和队友都进入房间后才显示地图

        //踩到门的时候
        if (isClean==0)
        {
            foreach (var gate in AllGate)
            {
                gate.Close();  //锁上自己【门】列表的所有门，Gate脚本Gate里有门开关动画器和对应的碰撞体
            }

            //_RoomGenerator.SetEnemy();
            SetEnemy();
            SetRBQ();
            isClean = 1;
        }


    }
    void UnLockRoom()
    {
        //打开自己列表所有门

        foreach (var gate in AllGate)
        {
            gate.Open(); // 设为打开动画
        }

        SetShop();//在房间中央设置商店

    }


    #endregion

    /// <summary>
    /// 设置敌人与RBQ
    /// </summary>
    #region
    [Header("设置敌人与RBQ")]
    public int EnemyCount;

    [Header("敌人出生点列表")]
    public List<Transform> spawnPoints = new List<Transform>();

    public void SetEnemy()
    {
        int enemyToSpawn = Random.Range(2,7);
        for (int i = 0; i < enemyToSpawn; i++)
        {
            // 随机选一个出生点
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            // 添加随机偏移
            Vector2 randomOffset = new Vector2(
                 Random.Range(-0.5f, 0.5f),  // X方向偏移（左右）
                Random.Range(-0.5f, 0.5f)   // Y方向偏移（上下）
            );
            Vector3 spawnPosition = spawnPoint.position + (Vector3)randomOffset;

            // 在该点生成敌人
            GameObject NewEnemy = Instantiate(_RoomGenerator.Enemy, spawnPosition, Quaternion.identity);

            Enemy enemyScript = NewEnemy.GetComponentInChildren<Enemy>();
            if (enemyScript != null)
            {
                //enemyScript.wallmap = this;//告诉自己生成的Enemy出生WallMap
                EnemyCount++; // 每生成一个就记一次
            }

            if (linkedRoom.roomType == Room.RoomType.Boss&& i == 1) { enemyScript.BecomeBoss_Selene(); }
        } 


    }

    public void SetRBQ()
    {
        int enemyToSpawn = Mathf.Min(spawnPoints.Count, Random.Range(1, 3)); // 最多不超过可用点数量

        for (int i = 0; i < enemyToSpawn; i++)
        {
            Transform spawnPoint = spawnPoints[i];

            // 可选：添加一点小偏移避免完全贴边（也可以不加）
            Vector2 offset = new Vector2(
                Random.Range(-0.2f, 0.2f),
                Random.Range(-0.2f, 0.2f)
            );

            Vector3 spawnPosition = spawnPoint.position + (Vector3)offset;
            //告诉自己生成的RBQ出生WallMap
            GameObject NewEnemy = Instantiate(_RoomGenerator.RBQ, spawnPosition, Quaternion.identity);
            //NewEnemy.GetComponentInChildren<RBQ>().wallmap = this;//RBQ需要知道wallMap是因为自己生下的Enemy需要知道
        }


    }

    public void SetShop() 
    {
        GameObject NewEnemy = Instantiate(_RoomGenerator.RBQ, transform.position, Quaternion.identity);
        //NewEnemy.GetComponentInChildren<RBQ>().wallmap = this;//RBQ需要知道wallMap是因为自己生下的Enemy需要知道
        NewEnemy.GetComponentInChildren<RBQ>().RBQState = 3;
    }

    public void CheckEnemyList()
    {
        EnemyCount--;
        if (EnemyCount == 0)
        {
            isClean = 2;
            UnLockRoom();
            Debug.Log("房间清理干净");

            //奖励一个队友
           // _RoomGenerator.SetFriend();
        }

    }
    #endregion



    public void ChangeTargetPlace(GameObject MoveTarget)
    {


        if (spawnPoints.Count <= 1) return;

        // 找一个不等于当前位置的随机点
        Transform currentTarget = MoveTarget.transform;
        Transform newTarget;

        do
        {
            newTarget = spawnPoints[Random.Range(0, spawnPoints.Count)];
        } while (newTarget == currentTarget && spawnPoints.Count > 1);

        MoveTarget.transform.position = newTarget.position;


    }
}
