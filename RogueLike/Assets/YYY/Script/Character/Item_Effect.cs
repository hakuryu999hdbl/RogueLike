using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Effect : MonoBehaviour
{

    public GameObject Icon_Green;
    public GameObject Icon_Orange;

    public int Item_Type;//0无  1恢复体力和生命值  2金币

    private void Start()
    {

        Item_Type = Random.Range(0, 3);


        switch (Item_Type) 
        {
            case 0:
                Destroy(gameObject);
                break;
            case 1:
                Icon_Green.SetActive(true);
                break;
            case 2:
                Icon_Orange.SetActive(true);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player>() != null)
            {

                switch (Item_Type)
                {  
                    case 1:
                        collision.gameObject.GetComponent<Player>().RestoreHealth(collision.gameObject.GetComponent<Player>().maxHealth / 3);//固定回复当前部分
                        collision.gameObject.GetComponent<Player>().ChangeStrength(collision.gameObject.GetComponent<Player>().maxStrength / 3);//固定回复当前部分

                        AudioManager.instance.AudioPlay(AudioManager.instance.Effect_zipper);
                        break;
                    case 2:
                        UIManager.instance.ChangeMoney(Random.Range(50, 100));
                        break;
                }


                Destroy(gameObject);
            }
        }

    }
}
