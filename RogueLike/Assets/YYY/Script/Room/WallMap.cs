using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMap : MonoBehaviour
{
    /// <summary>
    /// 房间小地图显示
    /// </summary>
    #region
    GameObject mapSprite;
    RoomGenerator _RoomGenerator;//寻找RoomGenerator,用于传送自己坐标告知RoomGenerator玩家到哪个房间了

    private void Start()
    {
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
    /// 玩家进入离开
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
    }


    #endregion

    /// <summary>
    /// 设置敌人
    /// </summary>
    #region
    [Header("设置敌人")]
    //敌人列表
    //public List<GameObject> enemyList = new List<GameObject>();
    public int EnemyCount;
    public void SetEnemy()
    {
        int enemyToSpawn = Random.Range(4,8);
        for (int i = 0; i < enemyToSpawn; i++)
        {
            GameObject NewEnemy = Instantiate(_RoomGenerator.Enemy, transform.position, Quaternion.identity);

            Enemy enemyScript = NewEnemy.GetComponentInChildren<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.wallmap = this;
                EnemyCount++; // 每生成一个就记一次
            }
        }



        // enemyList.Add(NewEnemy);



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
            _RoomGenerator.SetFriend();
        }

        // 移除所有 null（已被销毁的敌人）
        // enemyList.RemoveAll(IsUnityObjectMissing);

        //if (enemyList.Count == 0)
        //{
        //    isClean = true;
        //    UnLockRoom();
        //    Debug.Log("房间清理干净");
        //}
    }
    private bool IsUnityObjectMissing(GameObject obj)
    {
        return obj == null || obj == default;
    }
    #endregion
}
