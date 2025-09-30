using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_Manager : MonoBehaviour
{
    public Transform Camera_Position_1;
    public List<Transform> RBQ_Positions = new List<Transform>();



    public GameObject HideItem;


    //public List<GameObject> HideObject = new List<GameObject>();

    public void Hide_All() 
    {
        HideItem.SetActive(false);

        // 逐个销毁
        //foreach (GameObject obj in HideObject)
        //{
        //    if (obj != null)
        //    {
        //        obj.SetActive(false);
        //    }
        //}
        //// 清空列表
        //HideObject.Clear();

        Debug.Log("隐藏多余物品");
    }

}
