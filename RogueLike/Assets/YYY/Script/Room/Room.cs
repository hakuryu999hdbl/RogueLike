using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public bool roomLeft, roomRight, roomUp, roomDown;
    //看清距离起点的距离
    public int stepToStart;
    public Text text;
    public int doorNumber;

    public GameObject ShowColor;

    public void UpdateRoom()
    {
        //计算距离初始点的网格距离
        stepToStart = (int)(Mathf.Abs(transform.position.x / 70) + Mathf.Abs(transform.position.y / 15));

        text.text = stepToStart.ToString();

        if (roomUp)
            doorNumber++;
        if (roomDown)
            doorNumber++;
        if (roomLeft)
            doorNumber++;
        if (roomRight)
            doorNumber++;
    }


    //因为看不清而增加
    public void SetStartRoom()
    {

        ShowColor.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
    }
    public void SetEndRoom()
    {

        isBossRoom = true;


        //普通关卡生成Boss

        switch (GameFlowData.nextScene)
        {
            //case "Story_03":
            //    BossNumber = 1;
            //    break;
            //case "Story_05":
            //    BossNumber = 2;
            //    break;
            //case "Story_08":
            //    BossNumber = 4;
            //    break;
            //case "Story_10":
            //    BossNumber = 5;
            //    break;
            //case "Story_12":
            //    BossNumber = 6;
            //    break;


            // case "Story_01":
            // case "Story_02":
            //
            //     break;
            // case "Story_04":
            // case "Story_06":
            //
            //     break;
            // case "Story_07":
            //
            //     break;
            // case "Story_09":
            // case "Story_11":
            //
            //     break;
            //
            //
            // case "Arena":
            //
            //     break;


            default:
            case "Dungeon":
                 BossNumber = Random.Range(1, 10);//随机Boss   （频繁闪避的剑舞姬不能呆在小房间）
                 break;

            
            case "Story_01":
            case "Story_02":
                BossNumber = 1;//守卫队长
                break;

            //Boss房 黑魔导士

            case "Story_04":
                BossNumber = 13;//女仆长
                break;

            //Boss房 王女赛琳娜

            case "Story_06":
            case "Story_07":
                BossNumber = 9;//首席战斗修女
                break;

            case "Story_09":
                BossNumber = 8;//典狱长
                break;

            case "Story_11":
                BossNumber = 5;//皇太子亚历克西斯
                break;
        }



        ShowColor.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
    }


    [Header("生成Boss")]
    public GameObject Enemy;//生成Boss
    public bool isBossRoom = false;
    public int BossNumber;
    public bool isCreateBoss = false;//Boss只能刷一次

    [Header("多米纳斯召唤模式")]
    public bool isDominusBossRoom = false;         // inspector 勾选：这是皇帝最终战的房间                                               
  


    private void OnTriggerEnter2D(Collider2D collision)//检测到玩家显示
    {

        if (collision.gameObject.tag == "Player"&&isBossRoom && WaitOneTimeForSetBoss)//因为三番五次会遇到初始刷Boss，所以我将前1秒踩入不会刷Boss
        {
            if (!isCreateBoss) 
            {
              


                // 在该点生成敌人
                GameObject NewEnemy = Instantiate(Enemy, transform.position, Quaternion.identity);
                Enemy enemyScript = NewEnemy.GetComponentInChildren<Enemy>();
                if (isDominusBossRoom)
                {
                    enemyScript.BecomeShadow();//召唤物
 
                }
                else
                {
                    enemyScript.BossNumber = BossNumber;       
                }
               

               

                //如果亚历克西斯在场，那么赛琳娜也在场
                if (BossNumber == 5)
                {
                    Invoke("SetSelene", 10f);
                }

                BGM.instance.Stop();
                UIManager.instance.PlayBossMusic();//Boss出现才有声音


                isCreateBoss = true;
            }

       


        }//生成Boss


    }

    public void SetSelene() 
    {
        GameObject NewEnemy2 = Instantiate(Enemy, transform.position, Quaternion.identity);
        Enemy enemyScript2 = NewEnemy2.GetComponentInChildren<Enemy>();
        enemyScript2.BossNumber = 3;
    }//魔族化赛琳娜




    public BoxCollider2D box;//开始游戏后1秒之后，将Boss房尺寸缩小，变成进入和锁门同时进行
    private void Start()
    {
        // 延迟1秒后执行 ResizeCollider
        Invoke(nameof(ResizeCollider), 1f);
    }
    void ResizeCollider()
    {
        if (isBossRoom) 
        {
            if (box != null)
            {
                box.size = new Vector2(40f, 24f);
                box.offset = new Vector2(1f, 0f);
            }
        }

        WaitOneTimeForSetBoss = true;

    }

    bool WaitOneTimeForSetBoss = false;//因为三番五次会遇到初始刷Boss，所以我将前1秒踩入不会刷Boss



 
    public void SetShop()
    {
        GameObject NewEnemy = Instantiate(UIManager.instance._RoomGenerator.RBQ, transform.position, Quaternion.identity);
        //NewEnemy.GetComponentInChildren<RBQ>().wallmap = this;//RBQ需要知道wallMap是因为自己生下的Enemy需要知道
        NewEnemy.GetComponentInChildren<RBQ>().RBQState = 3;
    }

    public void SetMagicCircle()
    {
        Instantiate(UIManager.instance._RoomGenerator.MagicCircle, transform.position, Quaternion.identity);
    }
}
