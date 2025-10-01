using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGOptionUI : MonoBehaviour
{
    public string cgKey; // 例如填入 "CG_RapeFront_1"

    public GameObject highlightObj; // 高亮显示的物体，比如绿色描边
    public bool unlocked = false; // 是否已解锁
    public void SetHighlight(bool on)
    {
        highlightObj.SetActive(on && unlocked); // 只在解锁时允许显示高亮
    }

    public void SetUnlockedFromPrefs()
    {
        unlocked = PlayerPrefs.GetInt(cgKey, 0) == 1;
        gameObject.SetActive(unlocked); // 未解锁隐藏自己
    }

    public void PlayCG() 
    {
        Debug.Log("播放【CG】" + cgKey);
        UIManager.instance.PlayPlayerCG(cgKey);

        UIManager.instance.To_CGScence();
    }

    public void PlayAVG() 
    {
        Debug.Log("播放【AVG】" + cgKey);
        UIManager.instance.PlayAVG(cgKey);

        UIManager.instance.To_AVGScene();
    }

    public int CG_Number;

    public void PlayCG_End()
    {
        //switch (CG_Number) 
        //{
        //    case 0:
        //        GameFlowData.nextScene = "CG";
        //        break;
        //    case 1:
        //        GameFlowData.nextScene = "CG_AVG_01";
        //        break;
        //    case 2:
        //        GameFlowData.nextScene = "CG_AVG_02";
        //        break;
        //    case 3:
        //        GameFlowData.nextScene = "CG_AVG_03";
        //        break;
        //}
        //GameFlowData.nextScene = cgKey;
        //Invoke("ReLoadScene", 0.2f);

        UIManager.instance.ReLoadScene();//前往CG页面
    }

    //void ReLoadScene() 
    //{
    //    UIManager.instance.ReLoadScene();//前往CG页面
    //}
}
