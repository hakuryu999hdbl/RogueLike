using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Player : MonoBehaviour
{
    [Header("基础数值")]
    public Animator anim;//接入Spine动画机
    private float inputX, inputY;
    private float StopX, StopY;
    private bool isRunning = false;
    int moveSpeed = 0;

    public Rigidbody2D rbody;//声明刚体
    float speed = 2; // 基础移动速度

    [Header("InputSystem和手柄")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionAsset inputActions;

    private InputAction runAction;

    public Joystick Joystick;

    private void Start()
    {
        // 获取Run动作（根据你的Action Map结构可能需要调整路径）
        runAction = inputActions.FindAction("Run");

        // 订阅输入事件
        runAction.started += OnRunStarted;
        runAction.canceled += OnRunCanceled;
    }
    private void OnRunStarted(InputAction.CallbackContext context)
    {
        isRunning = true;
    }
    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        isRunning = false;
    }

    public void ButtonSetRun()
    {
        isRunning = true;
    }
    public void ButtonSetStop()
    {
        isRunning = false;
    }

    Vector2 input;
    private void FixedUpdate()
    {
        //这个是拉杆控制，最优先，如果手柄没有输入，再检测手柄键盘等
        inputX = Joystick.Horizontal;
        inputY = Joystick.Vertical;
        Vector2 input = (transform.right * inputX + transform.up * inputY).normalized;//旋转摄像头

        if (inputX == 0 && inputY == 0)
        {

            input = moveAction.action.ReadValue<Vector2>();
            //Debug.Log("移动方向: " + input);

            // 记录原始输入值（四向判断用）
            inputX = input.x;
            inputY = input.y;

        }

        if (inputX > 0.5f) { inputX = 1; inputY = 0; }
        else if (inputX < -0.5f) { inputX = -1; inputY = 0; }
        else if (inputY > 0.5f && inputX > -0.5f && inputX < 0.5f) { inputX = 0; inputY = 1; }
        else if (inputY < -0.5f && inputX > -0.5f && inputX < 0.5f) { inputX = 0; inputY = -1; }
        else { inputX = 0; inputY = 0;  } // 静止时也归零

        // 保存上一次方向（用于静止状态播放对应Idle动画）
        if (inputX != 0 || inputY != 0)
        {
            StopX = inputX;
            StopY = inputY;
            if (isRunning) { moveSpeed = 2; speed = 4; } else { moveSpeed = 1; speed = 2; }
        }
        else
        {
            moveSpeed = 0;
        }

        if (inputY > -0.5f && inputY < 0.5f && inputX > -0.5f && inputX < 0.5f) { speed = 0; }//防止微微拉动拉杆也移动

        rbody.velocity = input * speed;
        // 传给 Spine 动画机
        anim.SetFloat("InputX", StopX);
        anim.SetFloat("InputY", StopY);

        anim.SetInteger("Speed", moveSpeed);


    }



}
