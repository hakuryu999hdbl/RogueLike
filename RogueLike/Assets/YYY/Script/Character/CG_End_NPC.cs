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


        switch (AnimtorNumber) 
        {
            case -3:
                if (GameFlowData.nextScene == "Story_05"|| GameFlowData.nextScene == "Story_08")
                {


                    anim.Play("RBQ_Torture_EggBirth");
                    anim.SetFloat("InputX", 0);
                    anim.SetFloat("InputY", -1);


                } //女性受罚产卵  正面
                break;

            case -2:
                if (GameFlowData.nextScene == "Story_05")
                {

                    anim.Play("RBQ_Torture_EggBirth");
                    anim.SetFloat("InputX", -1);
                    anim.SetFloat("InputY", 0);

                    //switch (Random.Range(0, 2)) 
                    //{
                    //    case 0:
                    //        anim.Play("RBQ_Torture_EggBirth");
                    //        anim.SetFloat("InputX", 0);
                    //        anim.SetFloat("InputY", -1);
                    //        break;
                    //    case 1:
                    //        anim.Play("RBQ_Torture_EggBirth");
                    //        anim.SetFloat("InputX", 1);
                    //        anim.SetFloat("InputY", 0);
                    //        break;
                    //}


                } //女性受罚  正面
                break;


            case -1:

                if(GameFlowData.nextScene== "CG_AVG_03")
                {
                    //保存 Girl 部位
                    Girl_headIndex = Random.Range(1, 13);  // 除去皇女
                    Girl_eyesIndex = Random.Range(1, 14);  // 1~13
                    Girl_bodyIndex = Random.Range(10, 13);//剑士射手法师
                    Girl_legsIndex = Random.Range(10, 13);//剑士射手法师
                    Girl_hatIndex = 1;

                  
                    anim.SetFloat("InputX", -1);
                    anim.SetFloat("InputY", 0);
                    anim.Play("RBQ_Punish_Cage_2");
                }  //狗笼肉货堆积

                if (GameFlowData.nextScene == "CG_AVG_01"|| GameFlowData.nextScene == "CG_AVG_02" || GameFlowData.nextScene == "CG_AVG_08")
                {
                  
                    anim.SetFloat("InputX", 0);
                    anim.SetFloat("InputY", -1);
                    anim.Play("Man_Default_Idle");

                    Man_headIndex = Random.Range(1, 5);//除去 皇子和皇帝
                    Man_bodyIndex = Random.Range(1, 5);//除去 皇子和皇帝
                    Man_hatIndex = Random.Range(1, 5);//除去 魔族角和绷带

                }  //男性站立 正面

                if (GameFlowData.nextScene == "CG_AVG_07")
                {


                    YYY_headIndex = Random.Range(1, 5);  //黑发主要
                    YYY_eyesIndex = Random.Range(1, 14);  // 1~13
                    YYY_bodyIndex = 7;//惩戒修女
                    int[] YYY_pool2 = { 2, 4, 5, 6, 7, 11, 12 };
                    YYY_legsIndex = YYY_pool2[UnityEngine.Random.Range(0, YYY_pool2.Length)];//和修女服搭配的丝袜
                    YYY_hatIndex = 7;//惩戒修女头巾

                    SetSkin();
                    anim.SetFloat("InputX", 0);
                    anim.SetFloat("InputY", -1);
                    anim.Play("Girl_Default_Idle");
                }  //惩戒修女  正面

                break;


            default:
            case 0:
                //女性站立 正面
                anim.SetFloat("InputX", 0);
                anim.SetFloat("InputY", -1);
                anim.Play("Girl_Default_Idle");
                //anim.Play("Girl_Spell_Idle");
                break;


            case 1:

                if (GameFlowData.nextScene == "CG_AVG_03")
                {
                    //保存 Girl 部位
                    Girl_headIndex = Random.Range(1, 13);  // 除去皇女
                    Girl_eyesIndex = Random.Range(1, 14);  // 1~13
                    Girl_bodyIndex = Random.Range(10, 13);//剑士射手法师
                    Girl_legsIndex = Random.Range(10, 13);//剑士射手法师
                    Girl_hatIndex = 1;

                
                    anim.SetFloat("InputX", -1);
                    anim.SetFloat("InputY", 0);
                    anim.Play("RBQ_Punish_Cage_2");

                }//狗笼肉货堆积

                if (GameFlowData.nextScene == "CG_AVG_01" || GameFlowData.nextScene == "CG_AVG_02" || GameFlowData.nextScene == "CG_AVG_08")
                {
                    
                    anim.SetFloat("InputX", -1);
                    anim.SetFloat("InputY", 0);
                    anim.Play("Man_Default_Idle");

                    Man_headIndex = Random.Range(1, 5);//除去 皇子和皇帝
                    Man_bodyIndex = Random.Range(1, 5);//除去 皇子和皇帝
                    Man_hatIndex = Random.Range(1, 5);//除去 魔族角和绷带
                }//男性站立 侧面

                if (GameFlowData.nextScene == "CG_AVG_07" )
                {


                    YYY_headIndex = Random.Range(1, 5);  //黑发主要
                    YYY_eyesIndex = Random.Range(1, 14);  // 1~13
                    YYY_bodyIndex = 7;//惩戒修女
                    int[] YYY_pool2 = { 2, 4, 5, 6, 7, 11, 12 };
                    YYY_legsIndex = YYY_pool2[UnityEngine.Random.Range(0, YYY_pool2.Length)];//和修女服搭配的丝袜
                    YYY_hatIndex = 7;//惩戒修女头巾

                    SetSkin();
                    anim.SetFloat("InputX", -1);
                    anim.SetFloat("InputY", 0);
                    anim.Play("Girl_Default_Idle");
                }  //惩戒修女  正面

                break;

            case 2:


                if (GameFlowData.nextScene == "CG_AVG_03")
                {
                  
                    anim.SetFloat("InputX", 0);
                    anim.SetFloat("InputY", -1);
                    anim.Play("NPC_Man_SlaveTrader");

                    Man_headIndex = 4;
                    Man_bodyIndex = 3;
                    Man_hatIndex = 4;
                }  //性奴商人站立 正面


                if (GameFlowData.nextScene == "CG_AVG_01" || GameFlowData.nextScene == "CG_AVG_02")
                {
                  
                    anim.SetFloat("InputX", 0);
                    anim.SetFloat("InputY", -1);
                    anim.Play("NPC_Girl_Read");
                }  //守卫队长宣读  正面

                if (GameFlowData.nextScene == "CG_AVG_07" || GameFlowData.nextScene == "CG_AVG_08")
                {


                    YYY_headIndex = 4;
                    YYY_eyesIndex = 6;
                    YYY_bodyIndex = 7;
                    YYY_legsIndex = 7;

                    YYY_hatIndex = 6;//首席战斗修女冠

                    weaponIndex = 3;//重弩 双刃斧 火焰法杖

                    SetSkin();
                    anim.SetFloat("InputX", 0);
                    anim.SetFloat("InputY", -1);
                    anim.Play("NPC_Girl_Read");
                }  //首席战斗修女宣读  正面

                break;

            case 3:


                if (GameFlowData.nextScene == "CG_AVG_03")
                {
                   
                    anim.SetFloat("InputX", -1);
                    anim.SetFloat("InputY", 0);
                    anim.Play("Man_Default_Idle");

                    Man_headIndex = 4;
                    Man_bodyIndex = 3;
                    Man_hatIndex = 4;

                } //性奴商人站立 侧面

                if (GameFlowData.nextScene == "CG_AVG_01" || GameFlowData.nextScene == "CG_AVG_02")
                {
                   
                    anim.SetFloat("InputX", -1);
                    anim.SetFloat("InputY", 0);
                    anim.Play("NPC_Girl_Read");
                } //守卫队长宣读  侧面

                if (GameFlowData.nextScene == "CG_AVG_07" || GameFlowData.nextScene == "CG_AVG_08")
                {


                    YYY_headIndex = 4;
                    YYY_eyesIndex = 6;
                    YYY_bodyIndex = 7;
                    YYY_legsIndex = 7;

                    YYY_hatIndex = 6;//首席战斗修女冠

                    weaponIndex = 3;//重弩 双刃斧 火焰法杖

                    SetSkin();
 
                    anim.SetFloat("InputX", -1);
                    anim.SetFloat("InputY", 0);
                    anim.Play("NPC_Girl_Read");

                }  //首席战斗修女宣读  侧面
                break;

            case 4:

                if (GameFlowData.nextScene == "CG_AVG_03")
                {
                    Destroy(gameObject);
                }

                if (GameFlowData.nextScene == "CG_AVG_01" || GameFlowData.nextScene == "CG_AVG_02")
                {
                   
                    anim.SetFloat("InputX", 0);
                    anim.SetFloat("InputY", -1);
                    anim.Play("NPC_Girl_Sit");
                } //女性坐姿  正面
                break;


            case 5:

                if (GameFlowData.nextScene == "CG_AVG_03")
                {
                    Destroy(gameObject);
                }

                if (GameFlowData.nextScene == "CG_AVG_01" || GameFlowData.nextScene == "CG_AVG_02")
                {
                    
                    anim.SetFloat("InputX", 1);
                    anim.SetFloat("InputY", 0);
                    anim.Play("NPC_Girl_Sit");
                }//女性坐姿  侧面
                break;
        }



        SetSkin();
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
