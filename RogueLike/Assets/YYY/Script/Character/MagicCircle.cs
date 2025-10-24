using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{

    public ParticleSystem ps;
    public Animator MagicFormationAnim;//魔法阵

    void Start() 
    {
        MagicFormationAnim.SetBool("Show", true);
        ps.Play();
        Invoke(nameof(DestroySelf), 10f);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().InMagicCircle = true;
        }
        if (other.CompareTag("Friend"))
        {
            other.GetComponent<Enemy>().InMagicCircle = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().InMagicCircle = false;
        }
        if (other.CompareTag("Friend"))
        {
            other.GetComponent<Enemy>().InMagicCircle = false;
        }
    }



    // 可在生成新关卡时自动销毁
    public void DestroySelf()
    {
        MagicFormationAnim.SetBool("Show",false);
        ps.Stop();
        Destroy(gameObject,3f);
    }
}
