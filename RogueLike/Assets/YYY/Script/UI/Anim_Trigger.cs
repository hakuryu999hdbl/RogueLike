using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Anim_Trigger : MonoBehaviour
{
    Button myButton;
    void Start()
    {
        myButton = GetComponent<Button>();
        if (myButton != null)
        {
            myButton.onClick.AddListener(OnButtonClick);
        }
    }
    void OnButtonClick()
    {
        if (myButton != null)
        {
            myButton.OnDeselect(null);
            //Debug.Log("按钮被点击！");
        }

    }

    public void HightLight_Voice()
    {
        //AudioManager.instance.AudioPlay(AudioManager.instance.Effect_tear1);//手动SE音频替换,筹码放置
    }
    public void Click_Voice()
    {
        AudioManager.instance.AudioPlay(AudioManager.instance.SE_Reba);//手动SE音频替换,筹码放置
    }

    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }

   // [Header("便捷切换")]
   // public List<GameObject> List; // 使用List来存储多个音乐
   //
   // bool isOn = false;
   //
   // public void SwitchOnOff()
   // {
   //     isOn = !isOn;
   //
   //     // 在这里添加更多逻辑来处理音乐的开/关
   //     if (isOn)
   //     {
   //         List[0].gameObject.SetActive(true);
   //     }
   //     else
   //     {
   //         List[0].gameObject.SetActive(false);
   //     }
   //
   // }//便捷切换
}
