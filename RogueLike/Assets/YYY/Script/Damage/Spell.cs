using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [Header("伤害对象")]
    public int Damage;
    public bool DamageToPlayer = true;
    public bool DamageToEnemy = true;
    public bool DamageToFriend = true;

    [Header("伤害类型")]
    public int TypeOfAttack;

    private int appliedDamage; // 当前生效的随机伤害
    private int baseDamage; // 原始设定伤害

    [Header("暴击")]
    public bool isCritial = false;
    public float chargeTime = 0f; // 由 Player 传入的蓄力时间

    // 显式初始化，外部调用
    public void Init(int damage, int typeOfAttack, bool isCrit, float charge)
    {
        Damage = damage;
        TypeOfAttack = typeOfAttack;
        isCritial = isCrit;
        chargeTime = charge;



        baseDamage = Damage; // 保存原始值
        appliedDamage = baseDamage + Random.Range(-5, 5); // 例如±10范围

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
    private void OnDisable()
    {
        appliedDamage = baseDamage; // 恢复初始值
        isCritial = false;
    }//隐藏时清除


    [Header("是否是持续性伤害")]
    public bool isStayDamage = false;

    //单次伤害
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!isStayDamage)
        {

            //敌人
            if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject.GetComponent<Player>() != null && DamageToPlayer)
                {


                    collision.gameObject.GetComponent<Player>().ChangeHealth(appliedDamage, TypeOfAttack);//普通伤害


                }
            }
            if (collision.gameObject.tag == "Attack")
            {
                if (collision.gameObject.GetComponent<Dodge_Range>() != null && DamageToPlayer)
                {

                    collision.gameObject.GetComponent<Dodge_Range>().Dodge();//直接触发闪避

                }
            }



            //玩家和队友伤害
            if (collision.gameObject.tag == "Enemy")
            {

                if (DamageToEnemy)
                {

                    if (isCritial)
                    {
                        collision.gameObject.GetComponent<Enemy>()?.CritialAttack();
                        //collision.gameObject.GetComponent<Plant_Tentacle>()?.CritialAttack();

                    }//触发暴击（最先结算可以pass防御判断）


                    //持续性伤害不能有暴击
                    // ===== 局内短期Buff整数倍率 =====
                    int buffMult = 1; // 默认倍率 = 1
                    if (GameFlowData.Staff_Buff >= 2)
                        buffMult = GameFlowData.Staff_Buff;
                    appliedDamage *= buffMult; // 💥 应用整数倍率


                    collision.gameObject.GetComponent<Enemy>()?.ChangeHealth(appliedDamage, TypeOfAttack);//普通伤害
                    //collision.gameObject.GetComponent<Plant_Tentacle>()?.ChangeHealth(appliedDamage, TypeOfAttack);//普通伤害


                }


            }


            //敌人
            if (collision.gameObject.tag == "Friend")
            {

                if (collision.gameObject.GetComponent<Enemy>() != null && DamageToFriend)
                {



                    collision.gameObject.GetComponent<Enemy>().ChangeHealth(appliedDamage, TypeOfAttack);//普通伤害



                }


            }

            //障碍物
            if (collision.gameObject.tag == "obstacle")
            {

                if (collision.gameObject.GetComponent<Plant>() != null)
                {

                    collision.gameObject.GetComponent<Plant>().ChangeHealth(appliedDamage, TypeOfAttack);//普通伤害

                }


            }

        }
    }

    //持续性伤害
    private void OnTriggerStay2D(Collider2D collision)
    {


        if (isStayDamage)
        {

            //敌人
            if (collision.gameObject.tag == "Player")
            {
                if (collision.gameObject.GetComponent<Player>() != null && DamageToPlayer)
                {


                    collision.gameObject.GetComponent<Player>().ChangeHealth(appliedDamage, TypeOfAttack);//普通伤害


                }
            }
            if (collision.gameObject.tag == "Attack")
            {
                if (collision.gameObject.GetComponent<Dodge_Range>() != null && DamageToPlayer)
                {

                    collision.gameObject.GetComponent<Dodge_Range>().Dodge();//直接触发闪避

                }
            }



            //玩家和队友伤害
            if (collision.gameObject.tag == "Enemy")
            {

                if (DamageToEnemy)
                {

                    //持续伤害不能适用于buff
                    collision.gameObject.GetComponent<Enemy>()?.ChangeHealth(appliedDamage, TypeOfAttack);//普通伤害
                    //collision.gameObject.GetComponent<Plant_Tentacle>()?.ChangeHealth(appliedDamage, TypeOfAttack);//普通伤害



                }


            }


            //敌人
            if (collision.gameObject.tag == "Friend")
            {

                if (collision.gameObject.GetComponent<Enemy>() != null && DamageToFriend)
                {



                    collision.gameObject.GetComponent<Enemy>().ChangeHealth(appliedDamage, TypeOfAttack);//普通伤害



                }


            }

            //障碍物
            if (collision.gameObject.tag == "obstacle")
            {

                if (collision.gameObject.GetComponent<Plant>() != null)
                {

                    collision.gameObject.GetComponent<Plant>().ChangeHealth(appliedDamage, TypeOfAttack);//普通伤害

                }
             
            }

        }

    }
}
