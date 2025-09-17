using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator anim;
    public bool isOpen = false;
    [Header("主动触发声音")]
    public FrameEvents frameEvents;

    private void OnEnable()
    {
        isOpen = false;//当隐藏后再出现依旧关上

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.gameObject.tag == "Player"|| collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Friend")
        {

            if (!isOpen)
            {
                anim.SetBool("Open", true);
                frameEvents._SE_Gate_Open();

                isOpen = true;

            }

        }
        
    }
   // private void OnTriggerExit2D(Collider2D collision)
   // {
   //
   //     if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Friend")
   //     {
   //         if (isOpen)
   //         {
   //             anim.SetBool("Open", false);
   //             frameEvents._SE_Gate_Close();
   //
   //             isOpen = false;
   //
   //         }
   //      
   //     }
   //
   // }
}
