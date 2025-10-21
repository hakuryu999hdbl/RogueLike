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
        //SetBoss();

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
  
            PlayerInRoom = true;

            // 告知RoomGenerator玩家进入的房间位置
            Invoke("PlayerInThisRoomToRoomGenerator", 0.1f);
        }

        if (other.CompareTag("Friend")|| other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().wallmap = this;


            if (other.GetComponent<Enemy>().BossNumber!=0) 
            {

                isBossRoom = true;


            }//让Boss反向迫使wallMap知道自己是Boss房


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
    public bool isBossRoom = false;//在Boss房里敌人不刷
    public bool isArena = false;//角斗场的无限刷敌

    public void OnlyLockDoor() 
    {
        foreach (var gate in AllGate)
        {
            gate.Close();  //锁上自己【门】列表的所有门，Gate脚本Gate里有门开关动画器和对应的碰撞体
        }
        isClean = 1;


        GameFlowData.BulletCanThroughtWall = true;//只有在战斗的时候子弹可以穿墙
        Debug.Log("子弹可穿墙" + GameFlowData.BulletCanThroughtWall);

    }//在解救RBQ的时候触发一下

    public void LockRoom()
    {
        ShowMap();//玩家和队友都进入房间后才显示地图

        //踩到门的时候
        if (isClean==0)
        {

            //让房间刷敌数跟着玩家进入房间数来
            GameFlowData.RoomLevel += 1;





            foreach (var gate in AllGate)
            {
                gate.Close();  //锁上自己【门】列表的所有门，Gate脚本Gate里有门开关动画器和对应的碰撞体
            }


            if (isArena)
            {
                switch (GameFlowData.RoomLevel) 
                {
                  

                    case 1:
                    case 2:
                        SetEnemy(1);//刷男性敌人

                        break;

                    case 3:
                    case 4:
                    case 5:
                        SetEnemy(3);//刷男性女性敌人

                        break;

                    case 6:
                    case 7:
                    case 8:
                        SetEnemy(2);//刷触手敌人

                        break;

                      default:
                        SetEnemy();//全类型敌人

                        break;
                }
            }
            else if (isBossRoom)
            {


                //需要在Boss房内刷的敌人【0不刷敌人】
                switch (SetOtherEnemy)
                {
                    case 1:
                        SetEnemy(1);//Boss房间另外刷男性敌人
                        break;
                    case 2:
                        SetEnemy(2);//Boss房间另外刷触手敌人
                        break;
                    case 3:
                        SetEnemy(3);//Boss房间另外刷男性女性敌人
                        break;
                    case 4:
                        SetEnemy(4);//Boss房间另外刷肉铠
                        break;
                }

            }
            else 
            {


                //普通房间刷怪
                switch (GameFlowData.nextScene)
                {
                    case "Story_01":
                    case "Story_02":
                    case "Story_03":
                        SetEnemy(1);//第1，2，3关只有男性敌人
                        break;

                    case "Story_04":
                    case "Story_05":
                    case "Story_06":
                        SetEnemy(3);//第4，5，6关只有男性和女性敌人
                        break;
                    case "Story_07":
                    case "Story_08":
                    case "Story_09":
                        SetEnemy(2);//第7，8，9关只有触手怪敌人
                        break;

                    default:
                        SetEnemy();//[10,11,12全类型敌人] 地下城  竞技场   随机敌人
                        break;

                }





                SetRBQ();

                
            }
           
            isClean = 1;

            //_RoomGenerator.BossIcon.SetActive(true);
            //_RoomGenerator.Stage_Information.SetActive(true);

            _RoomGenerator.ShowInformationOfStage(1);


            GameFlowData.BulletCanThroughtWall = true;//只有在战斗的时候子弹可以穿墙
            Debug.Log("子弹可穿墙" + GameFlowData.BulletCanThroughtWall);
        }


    }
    void UnLockRoom()
    {

        if (isArena)
        {
            isClean = 0;//重刷敌人

            int currentWave = GameFlowData.RoomLevel + 1;
            int highestWave = PlayerPrefs.GetInt("Arena_Wave", 0);

            Debug.Log("目前角斗场最高波次：" + highestWave);
            Debug.Log("当前波次：" + currentWave);

            // 如果当前波次更高，则更新记录
            if (currentWave > highestWave)
            {
                PlayerPrefs.SetInt("Arena_Wave", currentWave);
                PlayerPrefs.Save();
                Debug.Log("新的最高波次记录：" + currentWave);
            }

            // 显示“第X波敌人”
            _RoomGenerator.ShowInformationOfStage(9);

            // 延迟锁门
            Invoke(nameof(LockRoom), 2f);
        }
        else 
        {
            //打开自己列表所有门

            foreach (var gate in AllGate)
            {
                gate.Open(); // 设为打开动画
            }

            if (!HasShop && !isBossRoom)
            {
                //ToDo：藏商店
                //SetShop();//在房间中央设置商店

                UIManager.instance.ShowBonusCavans();//开启三选一界面，只能开一次



                HasShop = true;
            }

            _RoomGenerator.ShowInformationOfStage(2);

            GameFlowData.BulletCanThroughtWall = false;//只有在战斗的时候子弹可以穿墙
            Debug.Log("子弹不可穿墙" + GameFlowData.BulletCanThroughtWall);
        }

      
    }
    bool HasShop = false;

    public bool isCanWinRoom = false;//当此房间的敌人（Boss）全部消灭，玩家获胜




    #endregion

    /// <summary>
    /// 设置敌人与RBQ
    /// </summary>
    #region
    [Header("设置敌人与RBQ")]
    public int EnemyCount;

    [Header("敌人出生点列表")]
    public List<Transform> spawnPoints = new List<Transform>();

    public int SetOtherEnemy;//0不设置其他敌人  1设置男性士兵   2设置触手怪物   3男女敌人   4肉铠

    //基础房间刷怪  Boss召唤刷怪   Boss房额外刷怪
    public void SetEnemy(int EnemySkin=0)// 0随机  1男性士兵   2触手怪物     3男女敌人  4肉铠
    {
        //上限
        int enemyToSpawn = Random.Range(1, 3);

        //队友数量
        //GameObject[] friends = GameObject.FindGameObjectsWithTag("Friend");
        //
        //int friendCount = friends.Length;
        //enemyToSpawn += friendCount;


        enemyToSpawn += GameFlowData.RoomLevel;
        Debug.Log("此房间敌人数量" + enemyToSpawn + "玩家进入房间数" + GameFlowData.RoomLevel);

        if (enemyToSpawn > 7) { enemyToSpawn = 7; }





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
                switch (EnemySkin) 
                {
                    case 1:
                        enemyScript.BecomeSoldier_Man = true;
                        break;
                    case 2:
                        enemyScript.BecomeTentacleMonster = true;
                        break;
                    case 3:
                        enemyScript.BecomeSoldier = true;
                        break;
                    case 4:
                        enemyScript.BecomeFleshArmor = true;
                        break;
                }

                //enemyScript.wallmap = this;//告诉自己生成的Enemy出生WallMap
                EnemyCount++; // 每生成一个就记一次
            }




        } 


    }

    public void SetRBQ()
    {
        int enemyToSpawn = Mathf.Min(spawnPoints.Count, Random.Range(2, 3)); // 最多不超过可用点数量

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
            NewEnemy.GetComponentInChildren<RBQ>().wallmap = this;//RBQ需要知道wallMap是因为    ⚪当Enemy生成的时候门需要重新关上        ×自己生下的Enemy需要知道
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
        // 只要场景中还有任意带 "Enemy" 标签的激活对象，就不解锁 
        StartCoroutine(CheckEnemyNextFrame());
    }


    private IEnumerator CheckEnemyNextFrame()
    {
        yield return null; // 等待一帧，等 Destroy 真正生效
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length <= 0)
        {
            isClean = 2;


            UnLockRoom();


            Debug.Log("房间清理干净（全场景无 Enemy）");


            if (isCanWinRoom) 
            {
                //完成关卡，结算画面
                _RoomGenerator.ShowResult();
            }

        }
        //Debug.Log("目前场景还剩Enemy数量" + enemies.Length);
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
