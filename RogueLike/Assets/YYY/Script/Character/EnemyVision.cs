using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public Enemy Enemy;
    public bool isFriend;

    public enum VisionType
    {
        PatrolVision,//负责从巡逻进入战斗
        ShortRangeVision,//负责拔刀和进入战斗状态
        LongRangeVision//负责选中目标敌人
    }

    [Header("敌人视觉类型")]
    public VisionType visionType = VisionType.PatrolVision;



    private void OnTriggerStay2D(Collider2D collision)//检测到玩家显示
    {

        if (!isFriend)
        {
            if (collision.gameObject.tag == "Player")
            {

                if (visionType == VisionType.PatrolVision)
                {
                    if (Enemy.isPatrol) { Enemy.isPatrol = false; }  //敌人从巡逻进入战斗
                }


                if (visionType == VisionType.ShortRangeVision) 
                {
                    Enemy.isAttack = true;
                    if (!Enemy.isPatrol) { Enemy.anim.SetTrigger("DrawWeapon"); }  //敌人第一次碰到目标需要拔刀)
                }
               

                Enemy.CurrentTarget = Enemy._Player.gameObject;
            }//敌人攻击玩家

            if (collision.gameObject.tag == "Friend")
            {

                if (visionType == VisionType.PatrolVision)
                {
                    if (Enemy.isPatrol) { Enemy.isPatrol = false; }  //敌人从巡逻进入战斗
                }

                if (visionType == VisionType.ShortRangeVision)
                {
                    Enemy.isAttack = true;
                    if (!Enemy.isPatrol) { Enemy.anim.SetTrigger("DrawWeapon"); }  //敌人第一次碰到目标需要拔刀
                }

                Enemy.CurrentTarget = collision.gameObject;

            }//敌人攻击队友



        }
        else
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if (visionType == VisionType.PatrolVision)
                {
                    if (Enemy.isPatrol) { Enemy.isPatrol = false; }  //队友从巡逻进入战斗
                }

                if (visionType == VisionType.ShortRangeVision)
                {
                    Enemy.isAttack = true;
                    if (!Enemy.isPatrol) { Enemy.anim.SetTrigger("DrawWeapon"); }  //敌人第一次碰到目标需要拔刀
                }

                Enemy.CurrentTarget = collision.gameObject;

            }//队友攻击敌人



        }

      
    }

    private void OnTriggerExit2D(Collider2D collision)//检测到玩家显示
    {
        if (!isFriend)
        {
            if (collision.gameObject.tag == "Player" )
            {
                if (visionType == VisionType.ShortRangeVision)
                {
                    Enemy.isAttack = false;
                }
            }//敌人停止攻击玩家

            if (collision.gameObject.tag == "Friend")
            {

                if (visionType == VisionType.ShortRangeVision)
                {
                    Enemy.isAttack = false;
                }

            }//敌人停止攻击队友

        }
        else
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if (visionType == VisionType.ShortRangeVision)
                {
                    Enemy.isAttack = false;
                }

            }//队友停止攻击敌人



        }//队友停止射击敌人



    }
}
