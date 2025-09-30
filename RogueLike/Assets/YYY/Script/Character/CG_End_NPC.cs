using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_End_NPC : MonoBehaviour
{
    //专门用于播放结局CG剧情，套在Spine外面控制皮肤使用和简单动作表现NPC,无法移动

    public int AnimtorNumber;//播放指定动作
    public Animator anim;//接入Spine动画机
    private void Start()
    {
        SetSkin();

        switch (AnimtorNumber) 
        {
            default:
            case 0:
                anim.SetFloat("InputX", 0);
                anim.SetFloat("InputY", -1);
                anim.Play("Girl_Default_Idle");
                break;


            case 1:
                anim.SetFloat("InputX", -1);
                anim.SetFloat("InputY", 0);
                anim.Play("Man_Default_Idle");

                Man_headIndex = Random.Range(1, 5);//除去 皇子和皇帝
                Man_bodyIndex = Random.Range(1, 5);//除去 皇子和皇帝
                Man_hatIndex = Random.Range(1, 5);//除去 魔族角和绷带

                SetSkin();
                break;
        }
    }


    /// <summary>
    /// 皮肤
    /// </summary>
    #region
    [Header("皮肤")]
    public CharacterSkin characterSkin;

    public int YYY_headIndex;
    public int YYY_eyesIndex;
    public int YYY_bodyIndex;
    public int YYY_legsIndex;
    public int YYY_hatIndex;

    public int Man_headIndex;
    public int Man_bodyIndex;
    public int Man_hatIndex;

    public int Girl_headIndex;
    public int Girl_eyesIndex;
    public int Girl_bodyIndex;
    public int Girl_legsIndex;
    public int Girl_hatIndex;

    public int weaponIndex;





    public void SaveCurrentSkin
        (
           int _YYY_headIndex, int _YYY_eyesIndex, int _YYY_bodyIndex, int _YYY_legsIndex, int _YYY_hatIndex,
           int _Man_headIndex, int _Man_bodyIndex, int _Man_hatIndex,
           int _Girl_headIndex, int _Girl_eyesIndex, int _Girl_bodyIndex, int _Girl_legsIndex, int _Girl_hatIndex,
           int _weaponIndex

        )
    {
        // 保存 YYY 部位
        YYY_headIndex = _YYY_headIndex;
        YYY_eyesIndex = _YYY_eyesIndex;
        YYY_bodyIndex = _YYY_bodyIndex;
        YYY_legsIndex = _YYY_legsIndex;
        YYY_hatIndex = _YYY_hatIndex;

        // 保存 Man 部位
        Man_headIndex = _Man_headIndex;
        Man_bodyIndex = _Man_bodyIndex;
        Man_hatIndex = _Man_hatIndex;

        // 保存 Girl 部位
        Girl_headIndex = _Girl_headIndex;
        Girl_eyesIndex = _Girl_eyesIndex;
        Girl_bodyIndex = _Girl_bodyIndex;
        Girl_legsIndex = _Girl_legsIndex;
        Girl_hatIndex = _Girl_hatIndex;

        // 保存武器
        weaponIndex = _weaponIndex;

        SetSkin();
    }

    public void SetSkin()
    {


        characterSkin.ShowCurrentAll
            (
            YYY_headIndex, YYY_eyesIndex, YYY_bodyIndex, YYY_legsIndex, YYY_hatIndex,
            Man_headIndex, Man_bodyIndex, Man_hatIndex,
            Girl_headIndex, Girl_eyesIndex, Girl_bodyIndex, Girl_legsIndex, Girl_hatIndex,
            weaponIndex
            );

    }

    #endregion
}
