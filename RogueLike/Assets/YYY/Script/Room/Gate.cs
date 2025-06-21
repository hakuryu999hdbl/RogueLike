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



            wallmap.LockRoom();
          
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
