using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    public int Damage;
    public bool DamageToPlayer = true;
    public bool DamageToEnemy = true;
    public bool DamageToFriend = true;

    public int TypeOfAttack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //敌人
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player>() != null && DamageToPlayer)
            {


                collision.gameObject.GetComponent<Player>().ChangeHealth(Damage, 1);//普通伤害

                //Debug.LogWarning("玩家受到近战伤害" + Damage );

            }
        }


        //玩家和队友伤害
        if (collision.gameObject.tag == "Enemy")
        {
 
            if (collision.gameObject.GetComponent<Enemy>() != null && DamageToEnemy)
            {

               

                collision.gameObject.GetComponent<Enemy>().ChangeHealth(Damage,TypeOfAttack);//普通伤害

                //Debug.LogWarning("玩家对敌人造成的伤害" + Damage);//（暴击的三倍伤害是在敌人内部计算的）

                //Damage = 0;//一定要清零

            }

            
        }

        if (collision.gameObject.tag == "Friend")
        {

            if (collision.gameObject.GetComponent<Enemy>() != null && DamageToFriend)
            {



                collision.gameObject.GetComponent<Enemy>().ChangeHealth(Damage, TypeOfAttack);//普通伤害

                //Debug.LogWarning("玩家对敌人造成的伤害" + Damage);//（暴击的三倍伤害是在敌人内部计算的）

                //Damage = 0;//一定要清零

            }


        }

    }

}
