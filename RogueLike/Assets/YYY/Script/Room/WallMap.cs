using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMap : MonoBehaviour
{
    GameObject mapSprite;


   RoomGenerator _RoomGenerator;//寻找RoomGenerator,用于传送自己坐标告知RoomGenerator玩家到哪个房间了
   
   private void Start()
   {
       _RoomGenerator = GameObject.FindGameObjectWithTag("RoomGenerator").GetComponent<RoomGenerator>();//寻找RoomGenerator
   }

    private void OnEnable()
    {
        mapSprite = transform.parent.GetChild(0).gameObject;//获取子物体
        mapSprite.SetActive(false);


    }



}
