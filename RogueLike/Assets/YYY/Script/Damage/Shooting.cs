using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("伤害类型")]
    public int TypeOfAttack;

    private int appliedDamage; // 当前生效的随机伤害
    private int baseDamage; // 原始设定伤害
    [Header("暴击")]
    public bool isCritial = false;
    public float chargeTime = 0f; // 由 Player 传入的蓄力时间

    private void OnEnable()
    {
        TypeOfAttack = 1;//剑伤

        baseDamage = Damage; // 保存原始值
        appliedDamage = baseDamage + Random.Range(-50, 50); // 例如±10范围

        if (isCritial)
        {
            appliedDamage *= 3; //暴击三倍伤害
        }
        else
        {
            // 非暴击时，根据蓄力时间提升伤害：最大 1.5 倍（>=1秒）
            float chargeMultiplier = Mathf.Lerp(1f, 1.5f, Mathf.Clamp01(chargeTime));
            appliedDamage = Mathf.RoundToInt(appliedDamage * chargeMultiplier);
        }

    }//初始化随机伤害


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



            if (other.gameObject.GetComponent<Plant>() != null)
            {

                other.gameObject.GetComponent<Plant>().ChangeHealth(appliedDamage, TypeOfAttack);//普通伤害

            }

            Destroy(gameObject);
        }//打到墙壁上产生火花




        // 判断目标是否是敌人阵营
        if (ownerType == BulletOwnerType.Friend && other.CompareTag("Enemy"))
        {
            if (SpecialBullet != -1)
            {

                if (isCritial) { other.gameObject.GetComponent<Enemy>().CritialAttack(); }//触发暴击（最先结算可以pass防御判断）
                other.GetComponent<Enemy>()?.ChangeHealth(appliedDamage, TypeOfAttack);
            }
            Destroy(gameObject);
            return;
        }

        if (ownerType == BulletOwnerType.Enemy && (other.CompareTag("Player") || other.CompareTag("Friend")))
        {
            if (SpecialBullet != -1)
            {
                if (other.CompareTag("Player"))
                    other.GetComponent<Player>()?.ChangeHealth(appliedDamage, TypeOfAttack);
                else if (other.CompareTag("Friend"))
                    other.GetComponent<Enemy>()?.ChangeHealth(appliedDamage, TypeOfAttack); // 队友是Enemy脚本
            }
            Destroy(gameObject);
            return;
        }


        if (ownerType == BulletOwnerType.Enemy && other.CompareTag("Attack"))
        {
            if (other.gameObject.GetComponent<Dodge_Range>() != null)
            {

                other.gameObject.GetComponent<Dodge_Range>().Dodge();//直接触发闪避

            }
        }
    }



}
