using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class CharacterSkin : MonoBehaviour
{
    [Header("皮肤")]
    SkeletonMecanim skeletonAnimation;
    Skin blendSkin = new Skin("BlendedSkin");// 创建一个新的混合皮肤

    // Start is called before the first frame update
    void Start()
    {
        //换皮肤
        skeletonAnimation = GetComponent<SkeletonMecanim>();

        //初始皮肤
        ShowCurrentAll();

    }
    public void ShowCurrentAll()
    {
        //初始设置为混合皮肤
        //ShowCurrentBody();
        //ShowCurrentHead();
        //ShowCurrentLegs();

        blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin("YYY/Body/YYY_Body_color1"));
        blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin("YYY/Head/YYY_Head_color1"));
        blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin("YYY/Legs/YYY_Legs_color1"));
        blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin("YYY/Hat/YYY_Hat_color10"));//叶语嫣发饰

        blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin("Weapon/Weapon_color3"));

        skeletonAnimation.Skeleton.SetSkin(blendSkin);
        skeletonAnimation.Skeleton.SetSlotsToSetupPose();

        Debug.Log("设置皮肤");
    }



    [Header("帧事件触发")]
    public Player player;
   // public Enemy enemy;
    public void AttackWarn()
    {
        if (player != null)
        {
            //攻击开始无法移动
            player.canMove = false;
            Invoke("StartAttack", 0.5f);
        }
        //if (enemy != null)
        //{
        //    //攻击开始无法移动
        //    enemy.canMove = false;
        //    //Invoke("StartAttack", 0.5f);
        //}

    }
    public void AttackOver()
    {
        if (player != null)
        {
            //攻击结束可以移动
            player.canMove = true;
            player.attack_Collider.SetActive(false);
        }
        //if (enemy != null)
        //{
        //    enemy.canMove = true;
        //}
    }

    void StartAttack()
    {
        if (player != null)
        {
            player.attack_Collider.SetActive(true);
        }
        //if (enemy != null)
        //{
        //    enemy.attack_Collider.SetActive(true);
        //}
    }
}
