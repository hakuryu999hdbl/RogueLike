using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Attack : MonoBehaviour
{
    public GameObject Enemy_Strike;//敌人近战碰撞体
    public GameObject ShowArea;//攻击范围警告
    public GameObject DustEffect;
    public Animator anim;


    void Start()
    {
        InvokeRepeating("FlashWarning", 0.6f, 0.1f); // 每0.1秒闪烁一次
        Invoke("Attack", 1f);
        Destroy(gameObject, 2f);
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
        if (anim != null)
        {
            anim.Play("Tentacle_Attack");
        }
        DustEffect.SetActive(true);

        //攻击
        Invoke("Strike_Over", 0.1f);
        Enemy_Strike.SetActive(true);
    }
    void Strike_Over()
    {
        Enemy_Strike.SetActive(false);

    }

}
