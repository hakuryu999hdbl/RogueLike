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

    int moveSpeed = 0;

    public Rigidbody2D rbody;//声明刚体
    float speed = 2; // 基础移动速度

    public AIPath aiPath;// A* 路径控制器


    //距离自己的目标远跑近停
    float runThreshold = 1.12f;    // 距离大于这个值就跑
    float stopThreshold = 1f; // 距离小于这个值就停


    // Update is called once per frame
    void FixedUpdate()
    {
        if (aiPath == null || !aiPath.hasPath) return;

        Vector2 current = transform.position;
        Vector2 target = aiPath.steeringTarget;
        Vector2 dir = (target - current).normalized;

        float dist = Vector2.Distance(current, target);
        Debug.Log("Distance to target: " + dist);


        // 设置速度与动画状态
        if (dist > runThreshold)
        {
            moveSpeed = 2;
            aiPath.maxSpeed = 4f;
            speed = 4;
        }
        else if (dist < stopThreshold)
        {
            moveSpeed = 0;
            aiPath.maxSpeed = 0f;
            speed = 0;
        }
        else
        {
            moveSpeed = 1;
            aiPath.maxSpeed = 0.6f;
            speed = 0.6f;
        }

        // 八方向判断
        if (dir.x > 0.5f) { inputX = 1; inputY = 0; }
        else if (dir.x < -0.5f) { inputX = -1; inputY = 0; }
        else if (dir.y > 0.5f) { inputX = 0; inputY = 1; }
        else if (dir.y < -0.5f) { inputX = 0; inputY = -1; }
        else { inputX = 0; inputY = 0; }

        if (inputX != 0 || inputY != 0)
        {
            StopX = inputX;
            StopY = inputY;
        }

        // 动画传参
        anim.SetFloat("InputX", StopX);
        anim.SetFloat("InputY", StopY);
        anim.SetInteger("Speed", moveSpeed);

        // 如果你不想用 aiPath 自带移动，而是自己控制刚体：
        if (rbody != null)
        {
            Vector2 moveVec = new Vector2(inputX, inputY).normalized * speed;
            rbody.velocity = moveVec;
        }
    }
}

