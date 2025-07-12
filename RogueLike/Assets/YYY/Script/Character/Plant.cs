using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    [Header("物品类型")]
    public ObjectType objectType;
    public enum ObjectType
    {
        Torch,//火炬
        TortureDevice//刑具
    }



    public SpriteRenderer Object;
    public List<Sprite> spriteList = new List<Sprite>();
    public GameObject torch;

    void Start()
    {
        if (objectType ==ObjectType.TortureDevice&&!SetOver) 
        {

            int randomIndex = Random.Range(0, spriteList.Count);
            SetImage(randomIndex);

        }


        
    }
    bool SetOver = false;//只能设定一遍
    public void SetImage(int ImageIndex) 
    {
        Object.sprite = spriteList[ImageIndex];

        SetOver = true;
    }

    //伤害显示
    public GameObject SmokeEffect;
    public void ChangeHealth(int amount, int TypeOfAttack)//【攻击方式】-1穿透  0无  1剑击特效  2闪电特效 
    {




        Vector3 offset = new Vector3(0, 0, 2); // 这里的1表示沿Z轴上升的距离，可以根据需要调整
        Vector3 spawnPosition = transform.position + offset;
        GameObject effectPrefabs = Instantiate(SmokeEffect, spawnPosition, transform.rotation);
        Destroy(effectPrefabs, 2f);

        Destroy(gameObject);

    }

}
