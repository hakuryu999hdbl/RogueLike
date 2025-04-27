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

        // ========================================
        // 检查玩家是否静止
        // ========================================
        bool playerIdle = !player.isRunning && player.moveInput.magnitude < 0.01f;

        if (playerIdle)
        {
            playerIdleTimer += Time.fixedDeltaTime;

            if (!isWandering && playerIdleTimer >= Random.Range(1f, 2f))
            {
                isWandering = true;
                aiPath.canMove = false; // 停止A星寻路
                nextWanderActionTime = 0f;
            }
        }
        else
        {
            if (isWandering)
            {
                isWandering = false;
                aiPath.canMove = true; // 恢复A星寻路
                rbody.velocity = Vector2.zero; // 重置自己给的速度
            }
            playerIdleTimer = 0f;
        }

        Vector2 moveDir;

        if (isWandering)
        {
            // 巡逻模式下
            nextWanderActionTime -= Time.fixedDeltaTime;
            if (nextWanderActionTime <= 0f)
            {
                nextWanderActionTime = Random.Range(0.5f, 3f);

                int rand = Random.Range(0, 5); // 0停 1上 2下 3左 4右
                switch (rand)
                {
                    case 0: wanderDirection = Vector2.zero; break;
                    case 1: wanderDirection = Vector2.up; break;
                    case 2: wanderDirection = Vector2.down; break;
                    case 3: wanderDirection = Vector2.left; break;
                    case 4: wanderDirection = Vector2.right; break;
                }
            }

            moveDir = wanderDirection;
        }
        else
        {
            // 正常寻路模式
            moveDir = dir;
        }

        // ========================================
        // 方向处理（每帧更新）
        // ========================================
        if (moveDir.x > 0.5f) { inputX = 1; inputY = 0; }
        else if (moveDir.x < -0.5f) { inputX = -1; inputY = 0; }
        else if (moveDir.y > 0.5f) { inputX = 0; inputY = 1; }
        else if (moveDir.y < -0.5f) { inputX = 0; inputY = -1; }
        else { inputX = 0; inputY = 0; }

        if (inputX != 0 || inputY != 0)
        {
            StopX = inputX;
            StopY = inputY;
        }

        anim.SetFloat("InputX", StopX);
        anim.SetFloat("InputY", StopY);

        // ========================================
        // 速度状态（加反应时差）
        // ========================================
        reactTimer += Time.fixedDeltaTime;
        if (reactTimer >= reactDelay)
        {
            reactTimer = 0f;
            ResetReactionDelay();

            if (isWandering)
            {
                if (wanderDirection == Vector2.zero)
                {
                    moveSpeed = 0;
                }
                else
                {
                    moveSpeed = 1;
                }
                aiPath.maxSpeed = 0f; // 巡逻时A星不管速度
            }
            else
            {
                if (dist > 1f)
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
            }

            anim.SetInteger("Speed", moveSpeed);
        }

        // ========================================
        // 最后: 自己给速度（只有巡逻时）
        // ========================================
        if (isWandering)
        {
            float patrolSpeed = 1f;
            rbody.velocity = new Vector2(inputX, inputY).normalized * patrolSpeed;
        }
    }

    [Header("随机0~1秒的触发时差")]
    private float reactTimer = 0f;
    private float reactDelay = 0f;
    void ResetReactionDelay()
    {
        reactDelay = Random.Range(0f, 1f); // 0到1秒之间
    } // 重置下一次反应需要的随机时间

    [Header("玩家站着不动1~2秒，四处走动")]
    private bool isWandering = false;
    private float playerIdleTimer = 0f;
    private float nextWanderActionTime = 0f;

    private Vector2 wanderDirection = Vector2.zero;
}

