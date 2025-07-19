using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBQ : MonoBehaviour
{
    [Header("主动触发声音")]
    public FrameEvents frameEvents;

    [Header("寻找RoomGenerator")]
    RoomGenerator _RoomGenerator;//寻找RoomGenerator



    [Header("基础数值")]
    public Animator anim;//接入Spine动画机
    private string[] tortureAnimations = { "RBQ_Torture_Impale", "RBQ_Torture_Strangle", "RBQ_Torture_CutDown" };

    public int RBQState = 0;//0单人拘束 1双人拷问中  2尸体
    public int CurrentRapeType=0;//1吊缚抽打 2后入奸
    public GameObject Torture_Rack;//刑架

    private float inputX, inputY;


    void Start()
    {
        //寻找RoomGenerator
        _RoomGenerator = GameObject.FindGameObjectWithTag("RoomGenerator").GetComponent<RoomGenerator>();


        RBQState = Random.Range(1, 3);

        // 随机动画
        if (RBQState == 1)
        {
            //string animName = punishAnims[Random.Range(0, punishAnims.Length)];
            //anim.Play(animName);

            CurrentRapeType = Random.Range(1, 3);

            switch (CurrentRapeType) 
            {
                case 1:
                    anim.Play("RBQ_Punish_Hang");
                    break;
                case 2:
                    anim.Play("RBQ_Punish_Rape");
                    break;

            }

        }
        else
        {
            int rand = Random.Range(0, tortureAnimations.Length);
            anim.Play(tortureAnimations[rand]);
        }



        // 根据方向旋转（可选，或控制朝向动画片段）
        ApplyFacingRotation();


        //随机皮肤
        SetRandomSkin();
    }

    void ApplyFacingRotation()
    {
        switch (Random.Range(1,5))
        {
            case 1:
                inputX = 1; inputY = 0;
                break;
            case 2:
                inputX = -1; inputY = 0;
                break;
            case 3:
                inputX = 0; inputY = 1;
                break;
            case 4:
                inputX = 0; inputY = -1;
                break;
        }

        // 动画传入方向
        anim.SetFloat("InputX", inputX);
        anim.SetFloat("InputY", inputY);
    }
    /// <summary>
    /// 触发点
    /// </summary>
    #region
    [Header("出生点WallMap")]
    public WallMap wallmap;


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            if (RBQState==1)
            {
                //出现敌人,停止拷问，冲向玩家
                GameObject NewEnemy = Instantiate(_RoomGenerator.Enemy, transform.position, Quaternion.identity);
                Enemy enemy = NewEnemy.transform.Find("Enemy").GetComponent<Enemy>();
                enemy.wallmap = wallmap;//告诉自己生成的Enemy出生点WallMap
                enemy.CanChangeSkin=false;
                StartCoroutine(DelayedApplySkin(enemy));
                enemy.ChangeClass(1);

                

                RBQState = 0;


                switch (CurrentRapeType)
                {
                    case 1:
                        anim.Play("RBQ_Punish_Hang_2");
                        break;
                    case 2:
                        anim.Play("RBQ_Punish_Rape_2");
                        break;

                }


            }
            else if (RBQState == 0)
            {
                //奖励一个队友
                GameObject NewEnemy = Instantiate(_RoomGenerator.Enemy, transform.position, Quaternion.identity);
                Enemy enemy = NewEnemy.transform.Find("Enemy").GetComponent<Enemy>();
                enemy.wallmap = wallmap;//告诉自己生成的Enemy出生点WallMap
                enemy.CanChangeSkin = false;
                StartCoroutine(DelayedApplySkin(enemy));
                enemy.ChangeClass(0);


                enemy.ConvertToFriend();




                //生成刑架
                switch (CurrentRapeType)
                {
                    case 1:
                        GameObject TortureDevice = Instantiate(Torture_Rack, transform.position, Quaternion.identity);
                        TortureDevice.GetComponent<Plant>().SetImage(0);
                        break;

                }



                // 消失自己(如果销毁的太快就容易传不进去)
                Destroy(gameObject,0.2f);
            }
           

          
        }
    }

    private IEnumerator DelayedApplySkin(Enemy enemy)
    {
        yield return new WaitForSeconds(0.1f); // 延迟 0.1 秒后赋值

        enemy.SaveCurrentSkin(
            YYY_headIndex, YYY_eyesIndex, YYY_bodyIndex, YYY_legsIndex, YYY_hatIndex,
            Man_headIndex, Man_bodyIndex, Man_hatIndex,
            Girl_headIndex, Girl_eyesIndex, Girl_bodyIndex, Girl_legsIndex, Girl_hatIndex,
            weaponIndex
        );
    }
    #endregion
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

    public void SetRandomSkin()
    {
        //YYY_headIndex = Random.Range(1, 14);  // 1~13
        //YYY_bodyIndex = Random.Range(1, 14);
        //YYY_legsIndex = Random.Range(1, 14);
        //YYY_hatIndex = Random.Range(1, 14);
        //
        //Man_headIndex = Random.Range(1, 7);   // 1~6
        //Man_bodyIndex = Random.Range(1, 7);
        //Man_hatIndex = Random.Range(1, 7);
        //
        //Girl_headIndex = Random.Range(1, 14);  // 1~13
        //Girl_bodyIndex = Random.Range(1, 14);
        //Girl_legsIndex = Random.Range(1, 14);
        //Girl_hatIndex = Random.Range(1, 14);
        //
        //weaponIndex = Random.Range(1, 5);   // 1~4



        YYY_headIndex = Random.Range(1, 14);  // 1~13
        YYY_eyesIndex = Random.Range(1, 14);  // 1~13
        YYY_bodyIndex = Random.Range(10, 13);
        YYY_legsIndex = Random.Range(10, 13);
        YYY_hatIndex = Random.Range(1, 5);

        Man_headIndex = Random.Range(1, 6);
        Man_bodyIndex = 2;
        Man_hatIndex = Random.Range(1, 3);

        Girl_headIndex = Random.Range(1, 14);  // 1~13
        Girl_eyesIndex = Random.Range(1, 14);  // 1~13
        Girl_bodyIndex = Random.Range(1, 14);
        Girl_legsIndex = Random.Range(1, 14);
        Girl_hatIndex = Random.Range(1, 14);

        weaponIndex = Random.Range(1, 7);


        SetSkin();
    }


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
