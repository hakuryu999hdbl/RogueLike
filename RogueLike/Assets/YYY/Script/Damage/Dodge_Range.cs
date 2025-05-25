using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge_Range : MonoBehaviour
{

    public Player player;
    public void Dodge()
    {
        //Debug.Log("外援盘被打到了");
        if (player.isDodge)
        {
            player.DodgeEnemyAttack();
        }

    }
}
