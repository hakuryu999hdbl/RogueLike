using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Attack : MonoBehaviour
{
    public GameObject Enemy_Strike;//敌人近战碰撞体
    public GameObject ShowArea;//攻击范围警告
    public GameObject DustEffect;//尘埃
    public GameObject Shadow;//影子
    public Animator anim;

    public int FollowDamage;//跟随型伤害  0触手  1暗影  2气场

    public Strike strike;//伤害攻击数值传输

    void Start()
    {
        InvokeRepeating("FlashWarning", 0.6f, 0.1f); // 每0.1秒闪烁一次
        Invoke("Attack", 1f);
        Destroy(gameObject, 2f);

        strike.Damage = -100 - UIManager.instance.player.Level * 20;
    }
    void FlashWarning()
    {
        // 切换黑色和白色
        if (ShowArea.GetComponent<SpriteRenderer>().color == Color.black)
        {
            ShowArea.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            ShowArea.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }

    void Attack()
    {
        CancelInvoke("FlashWarning"); // 停止闪烁
        ShowArea.SetActive(false);

        switch(FollowDamage) 
        {
            case 0:
               
                anim.Play("Tentacle_Attack");
                DustEffect.SetActive(true);
                break;
            case 1:
                anim.gameObject.SetActive(false);
                Shadow.SetActive(true);
                break;
            case 2:
                anim.gameObject.SetActive(false);
                DustEffect.SetActive(true);
                break;
        }

     

        //攻击
        Invoke("Strike_Over", 0.1f);
        Enemy_Strike.SetActive(true);
    }
    //void Strike_Over()
    //{
    //    Enemy_Strike.SetActive(false);
    //
    //}

}
