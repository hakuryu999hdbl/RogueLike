using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public enum BulletOwnerType
    {
        Enemy,
        Friend
    }

    [Header("子弹来源")]
    public BulletOwnerType ownerType;

    [Header("子弹基础参数")]

    public float speed = 2f;
    int Damage = -100;
    public float lifetime = 3f;
    private Vector3 direction;

    public void SetDirection(Vector3 dir, BulletOwnerType owner)
    {
        direction = dir.normalized;
        ownerType = owner;
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

        if (other.CompareTag("obstacle"))
        {
            if (SpecialBullet != -1)
            {
                GameObject EffectPrefabs = Instantiate(CurrentBulletEffect, rayTarget.transform.position, transform.rotation);
                Destroy(EffectPrefabs, 2f);          

            }//只要不是空包弹，就能有效果
            Destroy(gameObject);
        }//打到墙壁上产生火花




        // 判断目标是否是敌人阵营
        if (ownerType == BulletOwnerType.Friend && other.CompareTag("Enemy"))
        {
            if (SpecialBullet != -1)
            {
                other.GetComponent<Enemy>()?.ChangeHealth(Damage, 1);
            }
            Destroy(gameObject);
            return;
        }

        if (ownerType == BulletOwnerType.Enemy && (other.CompareTag("Player") || other.CompareTag("Friend")))
        {
            if (SpecialBullet != -1)
            {
                if (other.CompareTag("Player"))
                    other.GetComponent<Player>()?.ChangeHealth(Damage, 1);
                else if (other.CompareTag("Friend"))
                    other.GetComponent<Enemy>()?.ChangeHealth(Damage, 1); // 队友是Enemy脚本
            }
            Destroy(gameObject);
            return;
        }



    }



}
