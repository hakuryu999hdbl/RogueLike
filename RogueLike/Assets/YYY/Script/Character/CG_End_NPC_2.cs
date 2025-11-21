using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_End_NPC_2 : MonoBehaviour
{

    public GameObject People;
    public GameObject CombatNun;

    // Start is called before the first frame update
    void Start()
    {
        switch (GameFlowData.nextScene)
        {
            case "CG_AVG_04":
            case "CG_AVG_05":
            case "CG_AVG_06":
                People.SetActive(false);
                CombatNun.SetActive(false);
                break;


            case "CG_AVG_07":
            case "CG_AVG_08":
                People.SetActive(false);
                CombatNun.SetActive(true);
                break;


            default:
                People.SetActive(true);
                CombatNun.SetActive(false);
                break;
        }

    }


}
