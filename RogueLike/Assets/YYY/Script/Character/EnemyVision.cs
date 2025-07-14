using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public Enemy Enemy;
    public bool isFriend;
    public CircleCollider2D circleCollider2D;//我自己的视野范围

    public enum VisionType
    {
        PatrolVision,//负责从巡逻进入战斗
        AttackRangeVision,//负责进入战斗状态

    }

    [Header("敌人视觉类型")]
    public VisionType visionType = VisionType.PatrolVision;



    private void OnTriggerStay2D(Collider2D collision)//检测到玩家显示
    {

        if (!isFriend)
        {
            if (collision.gameObject.tag == "Player")
            {

                //if (visionType == VisionType.PatrolVision)
                //{
                //    if (Enemy.isPatrol) { Enemy.isPatrol = false; }  //敌人从巡逻进入战斗
                //}


                if (visionType == VisionType.AttackRangeVision) 
                {
                    Enemy.isAttack = true;
                    if (!Enemy.isPatrol) { Enemy.Draw(); }  //敌人第一次碰到目标需要拔刀)
                }
               

                Enemy.CurrentTarget = Enemy._Player.gameObject;
            }//敌人攻击玩家

            if (collision.gameObject.tag == "Friend")
            {

                //if (visionType == VisionType.PatrolVision)
                //{
                //    if (Enemy.isPatrol) { Enemy.isPatrol = false; }  //敌人从巡逻进入战斗
                //}

                if (visionType == VisionType.AttackRangeVision)
                {
                    Enemy.isAttack = true;
                    if (!Enemy.isPatrol) { Enemy.Draw(); }  //敌人第一次碰到目标需要拔刀
                }

                Enemy.CurrentTarget = collision.gameObject;

            }//敌人攻击队友



        }
        else
        {
            if (collision.gameObject.tag == "Enemy")
            {
                //if (visionType == VisionType.PatrolVision)
                //{
                //    if (Enemy.isPatrol) { Enemy.isPatrol = false; }  //队友从巡逻进入战斗
                //}

                if (visionType == VisionType.AttackRangeVision)
                {
                    Enemy.isAttack = true;
                    if (!Enemy.isPatrol) { Enemy.Draw(); }  //敌人第一次碰到目标需要拔刀
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
                if (visionType == VisionType.AttackRangeVision)
                {
                    Enemy.isAttack = false;
                }
            }//敌人停止攻击玩家
    
            if (collision.gameObject.tag == "Friend")
            {
    
                if (visionType == VisionType.AttackRangeVision)
                {
                    Enemy.isAttack = false;
                }
    
            }//敌人停止攻击队友
    
        }
        else
        {

            if (collision.gameObject.tag == "Enemy")
            {
                if (visionType == VisionType.AttackRangeVision)
                {
                    Enemy.isAttack = false;
                }
    
            }//队友停止攻击敌人


        }//队友停止射击敌人
    
    
    
    }
}
