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
    //综合播放
    public void PlayCG_End()
    {

        UIManager.instance.ReLoadScene();//前往CG页面
    }



    //这个是CG_End点击按钮触发，没有办法了写

    public void ToCG_End_00()
    {
        //GameFlowData.nextScene = "CG_AVG_01";
        //UIManager.instance.ReLoadScene();//前往CG页面

        UIManager.instance.CG_End_currentIndex = 0;
        UIManager.instance.UpdateHighlight_CG_End();
    }

    public void ToCG_End_01()
    {
        //GameFlowData.nextScene = "CG_AVG_01";
        //UIManager.instance.ReLoadScene();//前往CG页面

        UIManager.instance.CG_End_currentIndex = 1;
        UIManager.instance.UpdateHighlight_CG_End();
    }
    public void ToCG_End_02()
    {
        //GameFlowData.nextScene = "CG_AVG_02";
        //UIManager.instance.ReLoadScene();//前往CG页面

        UIManager.instance.CG_End_currentIndex = 2;
        UIManager.instance.UpdateHighlight_CG_End();
    }
    public void ToCG_End_03()
    {
        //GameFlowData.nextScene = "CG_AVG_03";
        //UIManager.instance.ReLoadScene();//前往CG页面

        UIManager.instance.CG_End_currentIndex = 3;
        UIManager.instance.UpdateHighlight_CG_End();
    }

    public void ToCG_End_04()
    {
        //GameFlowData.nextScene = "CG_AVG_04";
        //UIManager.instance.ReLoadScene();//前往CG页面

        UIManager.instance.CG_End_currentIndex = 4;
        UIManager.instance.UpdateHighlight_CG_End();
    }

    public void ToCG_End_05()
    {
        //GameFlowData.nextScene = "CG_AVG_05";
        //UIManager.instance.ReLoadScene();//前往CG页面

        UIManager.instance.CG_End_currentIndex = 5;
        UIManager.instance.UpdateHighlight_CG_End();
    }

    public void ToCG_End_06()
    {
        //GameFlowData.nextScene = "CG_AVG_06";
        //UIManager.instance.ReLoadScene();//前往CG页面

        UIManager.instance.CG_End_currentIndex = 6;
        UIManager.instance.UpdateHighlight_CG_End();
    }

    public void ToCG_End_07()
    {
        //GameFlowData.nextScene = "CG_AVG_07";
        //UIManager.instance.ReLoadScene();//前往CG页面

        UIManager.instance.CG_End_currentIndex = 7;
        UIManager.instance.UpdateHighlight_CG_End();
    }

    public void ToCG_End_08()
    {
        //GameFlowData.nextScene = "CG_AVG_08";
        //UIManager.instance.ReLoadScene();//前往CG页面

        UIManager.instance.CG_End_currentIndex = 8;
        UIManager.instance.UpdateHighlight_CG_End();
    }
}
