using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_Manager : MonoBehaviour
{
    public Transform Camera_Position_1;
    public List<Transform> RBQ_Positions = new List<Transform>();

   private void Start()
   {
        Instantiate(HideItem,transform.position, transform.rotation);
   }

    public GameObject HideItem;
    public GameObject Man;


    public void Creat_Man() 
    {
        Instantiate(Man, transform.position, transform.rotation);

    }
    public void Creat_HideItem()
    {
        Instantiate(HideItem, transform.position, transform.rotation);

    }


}
