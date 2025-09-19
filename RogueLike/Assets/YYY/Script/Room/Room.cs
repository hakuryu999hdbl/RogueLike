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
            // case "Dungeon":
            //
            //     break;
        
            default:
                BossNumber = 1;
                break;
        }



        ShowColor.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
    }


    [Header("生成Boss")]
    public GameObject Enemy;//生成Boss
    public bool isBossRoom = false;
    public int BossNumber;

   

    private void OnTriggerEnter2D(Collider2D collision)//检测到玩家显示
    {

        if (collision.gameObject.tag == "Player"&&isBossRoom)
        {
            // 在该点生成敌人
            GameObject NewEnemy = Instantiate(Enemy, transform.position, Quaternion.identity);
            Enemy enemyScript = NewEnemy.GetComponentInChildren<Enemy>();
            enemyScript.BossNumber = BossNumber;

            //如果亚历克西斯在场，那么赛琳娜也在场
            if (BossNumber == 5) 
            {
                GameObject NewEnemy2 = Instantiate(Enemy, transform.position, Quaternion.identity);
                Enemy enemyScript2 = NewEnemy2.GetComponentInChildren<Enemy>();
                enemyScript2.BossNumber = 3;
            }


        }//生成Boss


    }


  
}
