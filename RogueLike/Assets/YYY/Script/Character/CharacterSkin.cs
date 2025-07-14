using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class CharacterSkin : MonoBehaviour
{
    /// <summary>
    /// 皮肤
    /// </summary>
    #region
    [Header("皮肤")]
    SkeletonMecanim skeletonAnimation;
    Skin blendSkin = new Skin("BlendedSkin");// 创建一个新的混合皮肤


    // Start is called before the first frame update
    void Awake()
    {
        //换皮肤
        skeletonAnimation = GetComponent<SkeletonMecanim>();

        //初始皮肤
        //ShowCurrentAll();

    }


    public void ShowCurrentAll
        (
           int _YYY_headIndex, int _YYY_eyesIndex, int _YYY_bodyIndex,int _YYY_legsIndex,int _YYY_hatIndex,
           int _Man_headIndex, int _Man_bodyIndex, int _Man_hatIndex,
           int _Girl_headIndex, int _Girl_eyesIndex, int _Girl_bodyIndex, int _Girl_legsIndex, int _Girl_hatIndex,
           int _weaponIndex
        )
    {
        
        if (_YYY_headIndex != 0) { blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin($"YYY/Head/YYY_Head_color{_YYY_headIndex}")); }
        if (_YYY_eyesIndex != 0) { blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin($"YYY/Eyes/YYY_Eyes_color{_YYY_eyesIndex}")); }
        if (_YYY_bodyIndex != 0) { blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin($"YYY/Body/YYY_Body_color{_YYY_bodyIndex}")); }
        if (_YYY_legsIndex != 0) { blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin($"YYY/Legs/YYY_Legs_color{_YYY_legsIndex}")); }
        if (_YYY_hatIndex != 0) { blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin($"YYY/Hat/YYY_Hat_color{_YYY_hatIndex}")); }

        if (_Man_headIndex != 0) { blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin($"Man/Head/Man_Head_color{_Man_headIndex}")); }
        if (_Man_bodyIndex != 0) { blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin($"Man/Body/Man_Body_color{_Man_bodyIndex}")); }
        if (_Man_hatIndex != 0) { blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin($"Man/Hat/Man_Hat_color{_Man_hatIndex}")); }


        if (_Girl_headIndex != 0) { blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin($"Girl/Head/Girl_Head_color{_Girl_headIndex}")); }
        if (_Girl_eyesIndex != 0) { blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin($"Girl/Eyes/Girl_Eyes_color{_Girl_eyesIndex}")); }
        if (_Girl_bodyIndex != 0) { blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin($"Girl/Body/Girl_Body_color{_Girl_bodyIndex}")); }
        if (_Girl_legsIndex != 0) { blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin($"Girl/Legs/Girl_Legs_color{_Girl_legsIndex}")); }
        if (_Girl_hatIndex != 0) { blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin($"Girl/Hat/Girl_Hat_color{_Girl_hatIndex}")); }

        if (_weaponIndex != 0) { blendSkin.AddSkin(skeletonAnimation.Skeleton.Data.FindSkin($"Weapon/Weapon_color{_weaponIndex}")); }

        skeletonAnimation.Skeleton.SetSkin(blendSkin);
        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
    }

    public void HideSkeleton()
    {
        skeletonAnimation.Skeleton.A = 0f; // 完全透明
    }

    public void ShowSkeleton()
    {
        skeletonAnimation.Skeleton.A = 1f; // 完全不透明
    }


    #endregion


    /// <summary>
    /// 帧事件触发
    /// </summary>
    #region
    [Header("帧事件触发")]
    public Player player;
    public Enemy enemy;
    public RBQ rbq;
    public void AttackWarn()
    {
        if (player != null)
        {

        }


        if (enemy != null)
        {

            //enemy.ShootBullet();

        }



    } //攻击开始无法移动


    public void AttackShoot()
    {
        if (player != null)
        {

            player.ShootBullet();


        }
        if (enemy != null)
        {

            enemy.ShootBullet();
        }
    }




    public void AttackOver()
    {
        if (player != null)
        {

            player._Attack_Cancel();

        }
        if (enemy != null)
        {
            enemy.Attack_Cancel();

        }
    } //攻击结束可以移动



    void AttackStrike()
    {
        if (player != null)
        {
            if (player.isDie == false) { player.attack_Collider.SetActive(true); }//我方和敌方被击倒期间无法发出攻击碰撞体
            player.canCombo = true;
            player.AttackVoice();
        }
        if (enemy != null)
        {
            if (enemy.isDie == false) { enemy.attack_Collider.SetActive(true); }//我方和敌方被击倒期间无法发出攻击碰撞体
            enemy.AttackVoice();
        }

        Invoke("HideAttack", 0.2f);
    }//攻击碰撞体闪出来一下就消失

    void HideAttack()
    {

        if (player != null)
        {
            player.attack_Collider.SetActive(false);
        }
        if (enemy != null)
        {
            enemy.attack_Collider.SetActive(false);
        }

    }//攻击碰撞体消失


    #endregion

}
