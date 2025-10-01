using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_Manager : MonoBehaviour
{
    public Transform Camera_Position_1;
    public List<Transform> RBQ_Positions = new List<Transform>();

   private void Start()
   {
        Creat_HideItem_Front();
   }

    public GameObject HideItem_Front;
    public GameObject HideItem_Side;


    public void Creat_HideItem_Side() 
    {
        Instantiate(HideItem_Side, transform.position, transform.rotation);

    }
    public void Creat_HideItem_Front()
    {
        Instantiate(HideItem_Front, transform.position, transform.rotation);

    }


}
