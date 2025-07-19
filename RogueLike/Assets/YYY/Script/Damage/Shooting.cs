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

    [Header("子弹来源")]
    public BulletOwnerType ownerType;
    public enum BulletOwnerType
    {
        Enemy,
        Friend
    }

  

    [Header("子弹基础参数")]

    public float speed = 60f;
    int Damage = -100;
    public float lifetime = 3f;
    private Vector3 direction;



    public void SetDirection(Vector3 dir, BulletOwnerType owner)
    {
        direction = dir.normalized;
        ownerType = owner;
        Destroy(gameObject, lifetime); // 自动销毁

      

    }

  


    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }




    [Header("特殊类型子弹")]
    public int specialBullet;
    public GameObject Bullet, Arrow, Electricity, Flame;
    public void SetSpecialBullet(int SpecialBullet)// 0一般子弹   1弩箭   2魔法雷电球   3魔法火焰球
    {
        //特殊子弹
        switch (SpecialBullet)
        {
            case 0:
                Bullet.SetActive(true);
                CurrentBulletEffect = FireEffect;
                speed = 60f;

                lifetime = 3f;
                break;
            case 1:
                Arrow.SetActive(true);
                CurrentBulletEffect = FireEffect;
                TypeOfAttack = 1;//剑伤
                speed = 40f;
                lifetime = 3f;
                break;
            case 2:
                Electricity.SetActive(true);
                CurrentBulletEffect = LightingEffect;
                speed = 20f;
                TypeOfAttack = 2;//雷伤
                lifetime = 5f;
                break;
            case 3:
                Flame.SetActive(true);
                CurrentBulletEffect = BlastEffect;
                speed = 20f;

                lifetime = 5f;
                break;
        }
        specialBullet = SpecialBullet;
    }

 

  

    GameObject CurrentBulletEffect;//当前的这种子弹打到墙壁上弹出哪种特效

    public GameObject FireEffect;//子弹火星
    public GameObject BlastEffect;//爆炸特效
    public GameObject LightingEffect;//爆炸特效

    public Transform rayTarget;//特效的的位置






    private void OnTriggerEnter2D(Collider2D other)
    {


        // 判断目标是否是敌人阵营
        if (ownerType == BulletOwnerType.Friend && other.CompareTag("Enemy"))
        {

            switch (specialBullet)
            {
                case 0:
                case 1:
                    if (isCritial) { other.gameObject.GetComponent<Enemy>().CritialAttack(); }//触发暴击（最先结算可以pass防御判断）
                    other.GetComponent<Enemy>()?.ChangeHealth(appliedDamage, TypeOfAttack);
                    break;
                case 2:
                case 3:
                    GameObject EffectPrefabs = Instantiate(CurrentBulletEffect, rayTarget.transform.position, transform.rotation);
                    Strike strike = EffectPrefabs.transform.Find("Attack_Collider").GetComponent<Strike>();
                    strike.DamageToEnemy = true;
                    Destroy(EffectPrefabs, 1f);

                    break;


            }

            Destroy(gameObject);
            return;
        }

        if (ownerType == BulletOwnerType.Enemy && (other.CompareTag("Player") || other.CompareTag("Friend")))
        {

            switch (specialBullet)
            {
                case 0:
                case 1:

                    if (other.CompareTag("Player"))
                        other.GetComponent<Player>()?.ChangeHealth(appliedDamage, TypeOfAttack);
                    else if (other.CompareTag("Friend"))
                        other.GetComponent<Enemy>()?.ChangeHealth(appliedDamage, TypeOfAttack); // 队友是Enemy脚本

                    break;
                case 2:
                case 3:
                    GameObject EffectPrefabs = Instantiate(CurrentBulletEffect, rayTarget.transform.position, transform.rotation);
                    Strike strike = EffectPrefabs.transform.Find("Attack_Collider").GetComponent<Strike>();
                    strike.DamageToPlayer = true;
                    strike.DamageToFriend = true;
                    Destroy(EffectPrefabs, 1f);

                    break;


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


        if (other.CompareTag("obstacle"))
        {
            GameObject EffectPrefabs = Instantiate(CurrentBulletEffect, rayTarget.transform.position, transform.rotation);
            Destroy(EffectPrefabs, 2f);


            if (other.gameObject.GetComponent<Plant>() != null)
            {

                other.gameObject.GetComponent<Plant>().ChangeHealth(appliedDamage, TypeOfAttack);//普通伤害

            }

            Destroy(gameObject);
        }//打到墙壁上产生火花
    }

}
