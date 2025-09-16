using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_RBQ : MonoBehaviour
{
    public SpriteRenderer Object;
    public List<Sprite> spriteList = new List<Sprite>();
    void Start()
    {
        int randomIndex = Random.Range(0, spriteList.Count);
        Object.sprite = spriteList[randomIndex];
    }

}
