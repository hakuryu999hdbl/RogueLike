using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThrough : MonoBehaviour
{
    public GameObject Wall;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Wall.SetActive(false);
        }


    }//玩家进入隐藏墙壁

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Wall.SetActive(true);
        }


    }//玩家离开显示墙壁
}
