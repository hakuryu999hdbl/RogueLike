using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 控制伤害显示
/// </summary>
public class HUD : MonoBehaviour
{
    /// <summary>
    /// 滚动速度
    /// </summary>
    private float speed = 2.5f;

    /// <summary>
    /// 计时器
    /// </summary>
    private float timer = 0f;

    /// <summary>
    /// 销毁时间
    /// </summary>
    private float time = 1.8f;

    /// <summary>
    /// 玩家红 敌人白 回复绿
    /// </summary>
    public int color;

    private void Update()
    {
        Scroll();
    }

    /// <summary>
    /// 冒泡效果
    /// </summary>
    private void Scroll()
    {
        //字体滚动
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);
        timer += Time.deltaTime;
        //字体缩小
        this.GetComponent<Text>().fontSize--;
        //字体渐变透明

        switch (color)
        {
            case 0:
                this.GetComponent<Text>().color = new Color(1, 1, 1, 1 - timer);//白
                break;
            case 1:
                this.GetComponent<Text>().color = new Color(1, 0, 0, 1 - timer);//红
                break;
            case 2:
                this.GetComponent<Text>().color = new Color(0, 1, 0, 1 - timer);//绿
                break;
        }




        Destroy(gameObject, time);
    }

}