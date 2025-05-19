using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    public int Damage;
    public bool DamageToPlayer = true;
    public bool DamageToEnemy = true;
    public bool DamageToFriend = true;

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
    }

}
