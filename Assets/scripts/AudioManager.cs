using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("脚步声")]
    public AudioClip footClip;
    [Header("点击按钮声")]
    public AudioClip buttonClip;
    [Header("获得道具提示声")]
    public AudioClip getItemClip;
    [Header("攻击声")]
    public AudioClip playerAttackClip;
    public AudioClip monsterAttackClip;
    public AudioClip bossAttackClip;

    AudioSource footSource;
    AudioSource buttonSource;
    AudioSource getItemSource;
    AudioSource playerAtkSource;
    AudioSource monsterAtkSource;
    AudioSource bossAtkSource;

    static AudioManager current;
    private void Awake()
    {
        current = this;
        DontDestroyOnLoad(gameObject);

        footSource = gameObject.AddComponent<AudioSource>();
        buttonSource = gameObject.AddComponent<AudioSource>();
        getItemSource = gameObject.AddComponent<AudioSource>();
        playerAtkSource = gameObject.AddComponent<AudioSource>();
        monsterAtkSource = gameObject.AddComponent<AudioSource>();
        bossAtkSource = gameObject.AddComponent<AudioSource>();
    }

    public static void PlayFootAudio()
    {
        current.footSource.clip = current.footClip;
        current.footSource.Play();
    }
    public static void StopFootAudio()
    {
        //current.footSource.clip = current.footClip;
        current.footSource.Stop();
    }
    public static void PlayGetItemAudio()
    {
        current.getItemSource.clip = current.getItemClip;
        current.getItemSource.Play();
    }

    public static void PlayButtonAudio()
    {
        current.buttonSource.clip = current.buttonClip;
        current.buttonSource.Play();
    }
    public static void PlayPlayerAtkAudio()
    {
        current.playerAtkSource.clip = current.playerAttackClip;
        current.playerAtkSource.Play();
    }
    public static void PlayMonsterAtkAudio()
    {
        current.monsterAtkSource.clip = current.monsterAttackClip;
        current.monsterAtkSource.Play();
    }

    public static void PlayBossAtkAudio()


    {
        current.bossAtkSource.clip = current.bossAttackClip;
        current.bossAtkSource.Play();
    }
}
