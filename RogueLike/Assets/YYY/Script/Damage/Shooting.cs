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

    public void Init(int damage, int prefabs_damage, bool isCrit, float charge, int specialType,
                 Vector3 dir, BulletOwnerType owner)
    {
        Damage = damage;
        PrefabsDamage = prefabs_damage;
        isCritial = isCrit;
        chargeTime = charge;
        SetSpecialBullet(specialType);

        //Debug.Log("最初蓄力时间" + chargeTime);

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

        //Debug.Log("子弹最终伤害" + appliedDamage + "子弹基础伤害" + baseDamage + "传送伤害" +Damage);

        SetDirection(dir, owner);


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
    public int Damage = -100;//带入对应的值(子弹伤害)
    public int PrefabsDamage = -300;//带入对应的值（子弹生成物的法术伤害）
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
    public GameObject Bullet, Arrow, Electricity, Flame, Ice,Poison,Wind,Darkness;
    public void SetSpecialBullet(int SpecialBullet)//-1暗黑魔法  0一般子弹   1弩箭   2魔法雷电球   3魔法火焰球  4冰魔法  5毒魔法
    {
        //特殊子弹
        switch (SpecialBullet)
        {

            case -1:
                Darkness.SetActive(true);
                CurrentBulletEffect = DarknessEffect;
                //speed = 50f;
                speed = 20f;
                TypeOfAttack = -1;//暗黑

                lifetime = 1f;
                break;


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
                lifetime = 3f;
                break;
            case 3:
                Flame.SetActive(true);
                CurrentBulletEffect = BlastEffect;
                speed = 20f;
                TypeOfAttack = 4;//火伤
                lifetime = 3f;
                break;
            case 4:
                Ice.SetActive(true);
                CurrentBulletEffect = IceEffect;
                speed = 20f;
                TypeOfAttack = 3;//冻结
                lifetime = 3f;
                break;

            case 5:
                Poison.SetActive(true);
                CurrentBulletEffect = PoisonEffect;
                speed = 20f;
                TypeOfAttack = 5;//毒物

                lifetime = 3f;
                break;


            case 6:
                Wind.SetActive(true);
                CurrentBulletEffect = TyphoonEffect;
                speed = 20f;
                TypeOfAttack = 6;//吹飞

                lifetime = 2f;
                break;
        }
        specialBullet = SpecialBullet;
    }

 

  

    GameObject CurrentBulletEffect;//当前的这种子弹打到墙壁上弹出哪种特效

    public GameObject FireEffect;//火星特效
    public GameObject BlastEffect;//爆炸特效
    public GameObject LightingEffect;//雷柱特效
    public GameObject IceEffect;//冰特效
    public GameObject PoisonEffect;//毒气特效
    public GameObject TyphoonEffect;//飓风特效
    public GameObject DarknessEffect;//暗黑特效

    public Transform rayTarget;//特效的的位置






    private void OnTriggerEnter2D(Collider2D other)
    {


        // 判断目标是否是敌人阵营
        if (ownerType == BulletOwnerType.Friend && other.CompareTag("Enemy"))
        {

            switch (specialBullet)
            {
                //子弹，弓箭，冰弹
                case 0:
                case 1:
                case 4:
               

                    if (isCritial) { other.gameObject.GetComponent<Enemy>().CritialAttack(); }//触发暴击（最先结算可以pass防御判断）
                    other.GetComponent<Enemy>()?.ChangeHealth(appliedDamage, TypeOfAttack);
                    break;

                //火球，雷球,风球,暗黑
                case 2:
                case 3:
                case 6:
                case -1:
                    GameObject EffectPrefabs = Instantiate(CurrentBulletEffect, rayTarget.transform.position, transform.rotation);
                    var s = EffectPrefabs.transform.Find("Attack_Collider").GetComponent<Spell>();
                    s.DamageToEnemy = true;
                    s.Init(PrefabsDamage, TypeOfAttack, isCritial, chargeTime);// ← 直接把算好的值传进去

                    Destroy(EffectPrefabs, 1f);

                    break;

                //毒球
                case 5:
                    GameObject EffectPrefabs2 = Instantiate(CurrentBulletEffect, rayTarget.transform.position, transform.rotation);
                    var s2 = EffectPrefabs2.transform.Find("Attack_Collider").GetComponent<Spell>();
                    s2.DamageToEnemy = true;
                    s2.Init(PrefabsDamage/5, TypeOfAttack, isCritial, chargeTime);// ← 直接把算好的值传进去
                    //持续性伤害过强大幅削减

                    Destroy(EffectPrefabs2, chargeTime);//蓄力越久留存越久
                    Debug.Log("蓄力时间" + chargeTime);
                    break;

            }

           

            if (specialBullet!=6) 
            {
                Destroy(gameObject);
            } //风能贯穿



            return;
        }

        if (ownerType == BulletOwnerType.Enemy && (other.CompareTag("Player") || other.CompareTag("Friend")))
        {

            switch (specialBullet)
            {

                //子弹，弓箭，冰弹
                case 0:
                case 1:
                case 4:
              

                    if (other.CompareTag("Player"))
                        other.GetComponent<Player>()?.ChangeHealth(appliedDamage, TypeOfAttack);
                    else if (other.CompareTag("Friend"))
                        other.GetComponent<Enemy>()?.ChangeHealth(appliedDamage, TypeOfAttack); // 队友是Enemy脚本

                    break;

                //火球，雷球,风球,暗黑
                case 2:
                case 3:
                case 6:
                case -1:
                    GameObject EffectPrefabs = Instantiate(CurrentBulletEffect, rayTarget.transform.position, transform.rotation);
                    var s = EffectPrefabs.transform.Find("Attack_Collider").GetComponent<Spell>();
                    s.DamageToPlayer = true;
                    s.DamageToFriend = true;
                    s.Init(PrefabsDamage, TypeOfAttack, isCritial,chargeTime);// ← 直接把算好的值传进去

                    Destroy(EffectPrefabs, 1f);

                    break;

                // 毒球
                case 5:
                    GameObject EffectPrefabs2 = Instantiate(CurrentBulletEffect, rayTarget.transform.position, transform.rotation);
                    var s2 = EffectPrefabs2.transform.Find("Attack_Collider").GetComponent<Spell>();
                    s2.DamageToPlayer = true;
                    s2.DamageToFriend = true;
                    s2.Init(PrefabsDamage/5, TypeOfAttack, isCritial, chargeTime);// ← 直接把算好的值传进去
                    //持续性伤害过强大幅削减(敌人因为没有chargetime，所以默认0.2秒)


                    Destroy(EffectPrefabs2, chargeTime);//蓄力越久留存越久
                    Debug.Log("蓄力时间" + chargeTime);

                    break;

            }


            if (specialBullet != 6)
            {
                Destroy(gameObject);
            } //风能贯穿

            return;
        }


        if (ownerType == BulletOwnerType.Enemy && other.CompareTag("Attack"))
        {
            if (other.gameObject.GetComponent<Dodge_Range>() != null)
            {

                other.gameObject.GetComponent<Dodge_Range>().Dodge();//直接触发闪避

            }
        }


        if (other.CompareTag("obstacle")&&GameFlowData.BulletCanThroughtWall==false)
        {
            GameObject EffectPrefabs = Instantiate(CurrentBulletEffect, rayTarget.transform.position, transform.rotation);
            switch (specialBullet)
            {
                case 5:
                    Destroy(EffectPrefabs, chargeTime);//蓄力越久留存越久
                    break;
                default:
                    Destroy(EffectPrefabs, 0.5f);
                    break;
            }
            

            Destroy(gameObject);
        }//打到墙壁上产生火花

        if (other.CompareTag("obstacle") )
        {
            

            if (other.gameObject.GetComponent<Plant>() != null)
            {

                other.gameObject.GetComponent<Plant>().ChangeHealth(appliedDamage, TypeOfAttack);//普通伤害


                //打到障碍物还是需要发出特效
                GameObject EffectPrefabs = Instantiate(CurrentBulletEffect, rayTarget.transform.position, transform.rotation);
                switch (specialBullet)
                {
                    case 5:
                        Destroy(EffectPrefabs, chargeTime);//蓄力越久留存越久
                        break;
                    default:
                        Destroy(EffectPrefabs, 0.5f);
                        break;
                }


                Destroy(gameObject);
            }



            if (other.gameObject.GetComponent<Plant_Tentacle>() != null)
            {

                other.gameObject.GetComponent<Plant_Tentacle>().ChangeHealth(appliedDamage, TypeOfAttack);//普通伤害

            }


        }//打到障碍物
    }

}
