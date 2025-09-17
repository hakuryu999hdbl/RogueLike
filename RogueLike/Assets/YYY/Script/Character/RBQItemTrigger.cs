using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBQItemTrigger : MonoBehaviour
{
    public enum ItemType { Sword, Pistol, Staff, Clothes, Stockings, Slave }
    public ItemType itemType;

    [HideInInspector] public RBQ rBQ;

    void Start()
    {
        if (rBQ == null)
            rBQ = GetComponentInParent<RBQ>();//吃自己父级
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // 显示提示UI

        rBQ.frameEvents._Bullet_OutOfBullet();//碰上去的哒哒声

        rBQ.Prompt_Take.SetActive(true);

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // 显示提示UI
        rBQ.ShowItemPrompt(itemType);

        other.GetComponent<Player>().InteractingButton.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // 隐藏提示UI
        rBQ.HidePrompt();

        rBQ.Prompt_Take.SetActive(false);

        other.GetComponent<Player>().InteractingButton.SetActive(false);
    }


}
