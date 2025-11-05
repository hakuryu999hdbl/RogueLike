using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Tentacle : MonoBehaviour
{
    public FrameEvents FrameEvents;
    public Animator anim;
    public GameObject Map_Icon;//被干掉后会地图小标暂时消失
    public bool isHang;//是不是在空中的触手

    public WallMap wallmap;//死后检查

    private void Start()
    {
        if (isFade)
        {
            anim.Play("Tentacle_Wall_Fade_In");
        }
        else if (isEgg)
        {
            ToEgg();
        }
        else
        {
            Invoke("Birth", Random.Range(1, 5));//随机出现时间，给与层次不齐的感觉
        }
        

    }
    public bool isFade = false;//是否是装饰触手
    public bool isEgg = false;//是否是蛋的形式
    public bool isInvincible = false;//是否是循环出现的触手
    public void ToEgg() 
    {
        anim.Play("Egg_In");
        Rebirth();
    }

    void Birth()
    {
        switch (Random.Range(1, 4))
        {
           // case 0:
           //     anim.Play("Egg_In");
           //     break;
            case 1:
                anim.Play("Tentacle_Wall_1_In");
                break;
            case 2:
                anim.Play("Tentacle_Wall_2_In");
                break;
            case 3:
                anim.Play("Tentacle_Wall_3_In");
                break;
        }


        Rebirth();
    }


    void Rebirth()
    {
        anim.SetBool("Die", false);
        Map_Icon.SetActive(true);//地图小标出现
        //tag = "Enemy";


        // 启动用触发器(防止被多次烧伤)
        GetComponent<Collider2D>().enabled = true;
    }



    //伤害显示
    public GameObject SmokeEffect;
    public bool isScreaming;
    public HudText HudText;

    public void ChangeHealth(int amount, int TypeOfAttack)//【攻击方式】-1穿透  0无  1剑击特效  2闪电特效 
    {
        Debug.Log("触手收到攻击");

        if (anim.GetBool("Die") == false&&!isScreaming)
        {
            Vector3 offset = new Vector3(0, 0, 2); // 这里的1表示沿Z轴上升的距离，可以根据需要调整
            Vector3 spawnPosition = transform.position + offset;
            GameObject effectPrefabs = Instantiate(SmokeEffect, spawnPosition, transform.rotation);
            Destroy(effectPrefabs, 2f);

            switch (Random.Range(0, 3))
            {
                case 0:
                    FrameEvents._Attack_blood1();
                    break;
                case 1:
                    FrameEvents._Attack_blood2();
                    break;
                case 2:
                    FrameEvents._Attack_blood3();
                    break;
            }



            isScreaming = true;

            //显示伤害
            HudText.HUD(amount);

            //伤害冷却
            Invoke("HurtOver", 0.2f);

            Invoke("Die", 0.2f);

        }

    }


    void Die() 
    {
        anim.SetBool("Die", true);
        Map_Icon.SetActive(false); // 地图小标暂时消失
        //tag = "Untagged";


        Invoke("CheckForPlayerAndRebirth", 2f); // 等待几秒后检查是否复活


        if (!isInvincible) 
        {


            Invoke(nameof(Destroy),1f);

           
           

        }

    }
    public void CheckEnemyAfter3Time() 
    {
        Invoke(nameof(Destroy), 3f);
    }//生成的触手3秒后毁灭


    private bool isDestroyed = false;
    private void Destroy()
    {
        if (isDestroyed) return; // ✅ 避免重复调用
        isDestroyed = true;


        if (wallmap != null)
        {
            Debug.Log("调用 wallmap.CheckEnemyList()");
            wallmap.CheckEnemyList();

        }
        else
        {
            Debug.LogWarning("wallmap 是 null，无法调用 CheckEnemyList()");
        }

        Destroy(gameObject);
    }


    void HurtOver()
    {
        isScreaming = false;
    }//有1秒左右的伤害冷却



    [Header("暴击")]
    public GameObject Critial;

    public void CritialAttack()
    {


        Time.timeScale = 0;


        Critial.SetActive(true);//显示暴击

        UIManager.instance.player.ChangeCritical(-UIManager.instance.player.maxCritical);//暴击清零

    }//暴击

















    // 定义检测玩家范围的半径
    public float detectionRadius = 2.5f; // 半径可以在Inspector中调节
    public LayerMask playerLayerMask;// 定义玩家的LayerMask
    void CheckForPlayerAndRebirth()
    {
        // 检查玩家是否仍在触手范围内
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayerMask);
        if (player == null) // 玩家不在触手范围内
        {
            Rebirth();
        }
        else
        {
            // 玩家仍在触手范围内，稍后再检查
            Invoke("CheckForPlayerAndRebirth", 1f); // 每1秒检查一次，直到玩家离开
        }
    }


    /// <summary>
    /// 缠住玩家或者衍生物
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)//检测到敌人显示
    {
        if (collision.gameObject.tag == "Player")
        {


            if (anim.GetBool("Die") == false)
            {
                if (isHang)
                {
                    collision.gameObject.GetComponent<Player>().StartStruggle(2); //Debug.Log("玩家踩入触手陷阱");
                }
                else
                {
                    collision.gameObject.GetComponent<Player>().StartStruggle(1); //Debug.Log("玩家踩入触手陷阱");
                }




                collision.transform.position = transform.position;//触手拉过来
                Destroy(gameObject);



                //collision.gameObject.GetComponent<Player>().ChangeHealth(-200,0);


            }

            // 禁用触发器，避免重复触发(防止被多次烧伤)
            GetComponent<Collider2D>().enabled = false;

        }

    }

}
