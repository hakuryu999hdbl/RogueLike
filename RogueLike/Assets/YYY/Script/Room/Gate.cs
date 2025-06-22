using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Animator anim;
    public WallMap wallmap;
    [Header("主动触发声音")]
    public FrameEvents frameEvents;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {  

            if (wallmap.isClean==0) 
            {
                wallmap.LockRoom();


                // 找到所有带 Tag "Friend" 的对象
                GameObject[] friends = GameObject.FindGameObjectsWithTag("Friend");

                // 获取玩家位置
                Vector3 playerPos = collision.transform.position;

                foreach (GameObject friend in friends)
                {
                    // 偏移一点防止重叠
                    Vector3 offset = Random.insideUnitCircle.normalized * 1.5f;
                    friend.transform.position = playerPos + offset;
                }
            }
          

        }

    }


    public void Open() 
    {
        anim.SetBool("Open", true);
        frameEvents._SE_Gate_Open();
    }
    public void Close()
    {
        anim.SetBool("Open", false);
        frameEvents._SE_Gate_Close();
    }
}
