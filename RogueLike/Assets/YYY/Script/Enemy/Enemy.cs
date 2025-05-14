using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [Header("基础数值")]
    public Animator anim;//接入Spine动画机
    private float inputX, inputY;
    private float StopX, StopY;
    int moveSpeed = 0;//改动画器用的

    public Rigidbody2D rbody;//声明刚体

    public AIPath aiPath;// A* 路径控制器

    [Header("寻找玩家")]
    GameObject _Player;//玩家
    public Player player;

    private void Start()
    {
        //找玩家
        _Player = GameObject.FindGameObjectWithTag("Player");
        player = _Player.GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if (aiPath == null || !aiPath.hasPath) return;

        Vector2 current = transform.position;
        Vector2 target = aiPath.steeringTarget;

        Vector2 dir = (target - current).normalized;



        float dist = Vector2.Distance(current, target);

        // 设置速度与动画状态
        if (dist > 1)
        {

            if (player.isRunning)
            {
                moveSpeed = 2;
                aiPath.maxSpeed = 4f;
            }
            else
            {
                moveSpeed = 1;
                aiPath.maxSpeed = 2f;

            }

        }
        else
        {
            moveSpeed = 0;
            aiPath.maxSpeed = 0f;
        }








        // 八方向判断（上下左右为主）
        if (dir.x > 0.5f)
        {
            inputX = 1; inputY = 0;
        }
        else if (dir.x < -0.5f)
        {
            inputX = -1; inputY = 0;
        }
        else if (dir.y > 0.5f)
        {
            inputX = 0; inputY = 1;
        }
        else if (dir.y < -0.5f)
        {
            inputX = 0; inputY = -1;
        }
        else
        {
            inputX = 0; inputY = 0;
        }

        // 储存方向用于 idle 状态
        if (inputX != 0 || inputY != 0)
        {
            StopX = inputX;
            StopY = inputY;
        }

        // 动画传入方向
        anim.SetFloat("InputX", StopX);
        anim.SetFloat("InputY", StopY);
        anim.SetInteger("Speed", moveSpeed);

    }
}

