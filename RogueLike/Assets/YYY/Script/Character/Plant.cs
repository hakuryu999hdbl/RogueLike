using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    [Header("物品类型")]
    public SpriteRenderer Object;
    public List<Sprite> spriteList = new List<Sprite>();
    public GameObject Cage, torch;

    void Start()
    {
        switch (1) 
        {

            case 0:
                int randomIndex = Random.Range(0, spriteList.Count);
                Object.sprite = spriteList[randomIndex];

                Destroy(Cage);
                Destroy(torch);

                break;

            case 1:
                Destroy(Cage);
                Destroy(Object);
                break;
            case 2:
                Destroy(torch);
                Destroy(Object);
                break;

        }

        
    }


    //伤害显示
    public HudText HudText;
    public GameObject SmokeEffect;
    public void ChangeHealth(int amount, int TypeOfAttack)//【攻击方式】-1穿透  0无  1剑击特效  2闪电特效 
    {
        //显示伤害
        //HudText.HUD(amount);





        Vector3 offset = new Vector3(0, 0, 2); // 这里的1表示沿Z轴上升的距离，可以根据需要调整
        Vector3 spawnPosition = transform.position + offset;
        GameObject effectPrefabs = Instantiate(SmokeEffect, spawnPosition, transform.rotation);
        Destroy(effectPrefabs, 2f);

        Destroy(gameObject);

        //Invoke("Die", 1f);
    }

   // void Die()
   // {
   //     Destroy(gameObject);
   // }
}
