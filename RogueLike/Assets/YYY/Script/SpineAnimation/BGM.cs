using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{


    public static BGM instance { get; private set; }
    public AudioSource audioS;
    public bool isPlaying;


    [Header("Boss音乐")]
    public List<AudioClip> BossMusicList; // 使用List来存储多个音乐

    [Header("关卡音乐")]
    public List<AudioClip> DungeonMusicList; // 使用List来存储多个音乐
    public List<AudioClip> RuinsMusicList; // 使用List来存储多个音乐

    [Header("背景音乐")]
    public List<AudioClip> BackgroundMusicList;// 使用List来存储多个音乐


    void Start()
    {
        instance = this;
        audioS = GetComponent<AudioSource>();
    }

    public void AudioPlayBossMusic(int BGMNumber)
    {
        if (!isPlaying && BossMusicList.Count > 0)
        {

            if (BGMNumber < 0)
            {
                // 从列表中随机选择一首音乐
                audioS.clip = BossMusicList[Random.Range(0, BossMusicList.Count)];
            }
            else
            {
                audioS.clip = BossMusicList[BGMNumber];
            }//如果是小于0，那么随机播放，如果大于0，那么指定该序号播放


            //audioS.PlayOneShot(randomClip);

            // 将音频片段赋值给AudioSource的clip，并播放
            audioS.loop = true;  // 确保启用了循环播放
            audioS.Play();
            isPlaying = true;
        }

    }

    public void AudioPlayDungeonMusic(int BGMNumber)
    {
        if (!isPlaying && DungeonMusicList.Count > 0)
        {

            if (BGMNumber < 0)
            {
                // 从列表中随机选择一首音乐
                audioS.clip = DungeonMusicList[Random.Range(0, DungeonMusicList.Count)];
            }
            else
            {
                audioS.clip = DungeonMusicList[BGMNumber];
            }//如果是小于0，那么随机播放，如果大于0，那么指定该序号播放
          

            //audioS.PlayOneShot(randomClip);

            // 将音频片段赋值给AudioSource的clip，并播放
            audioS.loop = true;  // 确保启用了循环播放
            audioS.Play();
            isPlaying = true;
        }

    }

    public void AudioPlayRuinsMusic(int BGMNumber)
    {
        if (!isPlaying && DungeonMusicList.Count > 0)
        {

            if (BGMNumber < 0)
            {
                // 从列表中随机选择一首音乐
                audioS.clip = RuinsMusicList[Random.Range(0, RuinsMusicList.Count)];
            }
            else
            {
                audioS.clip = RuinsMusicList[BGMNumber];
            }//如果是小于0，那么随机播放，如果大于0，那么指定该序号播放


            //audioS.PlayOneShot(randomClip);

            // 将音频片段赋值给AudioSource的clip，并播放
            audioS.loop = true;  // 确保启用了循环播放
            audioS.Play();
            isPlaying = true;
        }

    }

    public void AudioPlayBackgroundMusic(int BGMNumber)
    {
        if (!isPlaying && BackgroundMusicList.Count > 0)
        {

            if (BGMNumber < 0)
            {
                // 从列表中随机选择一首音乐
                audioS.clip = BackgroundMusicList[Random.Range(0, BackgroundMusicList.Count)];
            }
            else
            {
                audioS.clip = BackgroundMusicList[BGMNumber];
            }//如果是小于0，那么随机播放，如果大于0，那么指定该序号播放


            // 将音频片段赋值给AudioSource的clip，并播放
            audioS.loop = true;  // 确保启用了循环播放
            audioS.Play();
            isPlaying = true;
        }

    }

    public void Stop()
    {
        audioS.Stop();
        isPlaying = false;
    }
}

