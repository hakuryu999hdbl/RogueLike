using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{



    [Header("子弹基础参数")]

    public float speed = 2f;
    int Damage = -100;
    public float lifetime = 3f;
    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifetime); // 自动销毁

        //特殊子弹
        switch (SpecialBullet)
        {
            case 0:
                CurrentBulletEffect = FireEffect;//当前的这种子弹打到墙壁上弹出火花
                break;

        }
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }




    [Header("特殊类型子弹")]
    public int SpecialBullet;//-1空包弹     0一般子弹
    public SpriteRenderer spriteReRenderer;

    public GameObject FireEffect;//子弹火星
    GameObject CurrentBulletEffect;//当前的这种子弹打到墙壁上弹出哪种特效

    public Transform rayTarget;//特效的的位置


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            if (SpecialBullet != -1)
            {

                other.gameObject.GetComponent<Player>().ChangeHealth(Damage, 1);


            }//只要不是空包弹

            Destroy(gameObject);
        }//打到玩家

        if (other.CompareTag("obstacle"))
        {
            if (SpecialBullet != -1)
            {
                GameObject EffectPrefabs = Instantiate(CurrentBulletEffect, rayTarget.transform.position, transform.rotation);
                Destroy(EffectPrefabs, 2f);          

            }//只要不是空包弹，就能有效果
            Destroy(gameObject);
        }//打到墙壁上产生火花


    }



}
