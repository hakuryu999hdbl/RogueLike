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
}
