using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 音频管理类
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    /// <summary>音效播放器</summary>
    [SerializeField] AudioSource sFXPlayer;
    /// <summary>音乐播放器</summary>
    [SerializeField] grumbleAMP gA;
    /// <summary>环境音播放器</summary>
    [SerializeField] AudioSource ambientPlayer;

    [SerializeField] float lowVolume;
    float OrignVolume;

    /// <summary>低频</summary>
    const float MIN_PITCH = 0.9f;
    /// <summary>高频</summary>
    const float MAX_PITCH = 1.1F;

    /// <summary>默认特效音效播放强度</summary>
    float DEFAULT_SFX_STRENGTH = 1.0f;
    /// <summary>默认环境音播放强度</summary>
    float DEFAULT_MUSIC_STRENGTH = 1.0f;
    /// <summary>默认背景音播放强度</summary>
    float DEFAULT_AMBIENT_STRENGTH = 1.0f;
    /// <summary>默认总音效播放强度</summary>
    float DEFAULT_MAIN_STRENGTH = 1.0f;

    /// <summary>
    /// 实时更新音频管理器的音量,音量大小由设置页面控制
    /// </summary>
    void Update()
    {
        ambientPlayer.volume = DEFAULT_AMBIENT_STRENGTH * DEFAULT_MAIN_STRENGTH;
        gA.setGlobalVolume(DEFAULT_MUSIC_STRENGTH * DEFAULT_MAIN_STRENGTH);
    }
    /// <summary>
    /// <para>适应性音乐模式</para>
    /// <para>通过当前玩家状态切换对应的音乐</para>
    /// </summary>
    enum adaptiveAudioMode
    {
        Travel,
        Battle,
        Menu,
        GameOver,
        GameClear
    };
    adaptiveAudioMode mode = adaptiveAudioMode.Menu;

    /// <summary>
    /// 进入游戏后10秒开始播放音乐
    /// </summary>
    void Start()
    {
        OrignVolume = ambientPlayer.volume;
        gA.PlaySong("MenuTrail", 0, 3.0f);
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioClip">音效</param>
    public void PlaySFX(AudioClip audioClip)
    {
        sFXPlayer.PlayOneShot(audioClip, DEFAULT_SFX_STRENGTH * DEFAULT_MAIN_STRENGTH);
    }

    /// <summary>
    /// 以自定义播放音效
    /// </summary>
    /// <param name="audioClip">音效</param>
    /// <param name="volume">自定义音量</param>
    public void PlaySFX(AudioClip audioClip, float volume)
    {
        sFXPlayer.PlayOneShot(audioClip, DEFAULT_SFX_STRENGTH * DEFAULT_MAIN_STRENGTH * volume);
    }

    /// <summary>
    /// 在区间[0.9, 1.1]之间随机选取一个频率，播放音效
    /// </summary>
    /// <param name="audioClip">音效</param>
    public void PlayRandomSFX(AudioClip audioClip)
    {
        sFXPlayer.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        PlaySFX(audioClip);
    }

    /// <summary>
    /// 在区间[0.9, 1.1]之间随机选取一个频率，以自定义音量播放音效
    /// </summary>
    /// <param name="audioClip">音效</param>
    /// <param name="volume">自定义音量</param>
    public void PlayRandomSFX(AudioClip audioClip, float volume)
    {
        sFXPlayer.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        PlaySFX(audioClip, volume);
    }

    /// <summary>
    /// 在区间[0.9, 1.1]之间随机选取一个频率，随机播放音效列表中的一个音效
    /// </summary>
    /// <param name="audioClip">音效列表</param>
    public void PlayRandomSFX(AudioClip[] audioClip)
    {
        PlayRandomSFX(audioClip[Random.Range(0, audioClip.Length)]);
    }

    /// <summary>
    /// 在区间[0.9, 1.1]之间随机选取一个频率，以自定义音量随机播放音效列表中的一个音效
    /// </summary>
    /// <param name="audioClip">音效列表</param>
    public void PlayRandomSFX(AudioClip[] audioClip, float volume)
    {
        PlayRandomSFX(audioClip[Random.Range(0, audioClip.Length)], volume);
    }

    /// <summary>
    /// 将背景音乐切换至旅行模式
    /// </summary>
    public void CrossFadeToTravelMode(bool reload = false)
    {
        if ((mode == adaptiveAudioMode.Travel || (mode == adaptiveAudioMode.GameOver) && !reload) || mode == adaptiveAudioMode.GameClear)
            return;
        mode = adaptiveAudioMode.Travel;
        int layerIndex = Random.Range(0, 2);
        gA.CrossFadeToNewSong("TravelTrail", layerIndex);
    }

    /// <summary>
    /// 将背景音乐切换至战斗模式
    /// </summary>
    public void CrossFadeToBattleMode()
    {
        if (mode == adaptiveAudioMode.Battle || mode == adaptiveAudioMode.GameOver)// 若游戏结束则不切换音乐
            return;
        mode = adaptiveAudioMode.Battle;
        int layerIndex = Random.Range(0, 2);
        gA.CrossFadeToNewSong("BattleTrail", layerIndex);
    }
    
    /// <summary>
    /// 将背景音乐切换至主菜单音乐
    /// </summary>
    public void CrossFadeToMenuMode()
    {
        if (mode == adaptiveAudioMode.Menu || mode == adaptiveAudioMode.GameOver)
            return;
        mode = adaptiveAudioMode.Menu;
        Debug.Log("fuck");
        gA.CrossFadeToNewSong("MenuTrail", 0);
    }

    /// <summary>
    /// 将背景音乐切换至战败曲
    /// </summary>
    public void CrossFadeToGameOverMode()
    {
        mode = adaptiveAudioMode.GameOver;
        gA.CrossFadeToNewSong("GameOverTrail", 0);
    }

    /// <summary>
    /// 将背景音乐切换至胜利曲
    /// </summary>
    public void CrossFadeToGameClearMode()
    {
        mode = adaptiveAudioMode.GameClear;
        gA.CrossFadeToNewSong("GameClearTrail", 0);
    }

    /// <summary>
    /// 降低环境音,雨声
    /// 当人物移动至障碍物下方时调用该函数
    /// 听到的雨声减弱
    /// </summary>
    public void TurnDownAmbientMusic()
    {
        ambientPlayer.volume = lowVolume * DEFAULT_AMBIENT_STRENGTH * DEFAULT_MAIN_STRENGTH;
    }

    /// <summary>
    /// 提高环境音,雨声
    /// 当人物头顶无障碍物时调用该函数
    /// 听到的雨声增大
    /// </summary>
    public void TurnUpAmbientMusic()
    {
        ambientPlayer.volume = OrignVolume * DEFAULT_AMBIENT_STRENGTH * DEFAULT_MAIN_STRENGTH;
    }

    /// <summary>
    /// 关闭环境音
    /// 当切换至其他场景时调用
    /// </summary>
    public void TurnOffAmbientMusic()
    {
        ambientPlayer.Stop();
    }

    /// <summary>
    /// 启动环境音
    /// 当切换回游戏场景时调用
    /// </summary>
    public void TurnOnAmbientMusic()
    {
        ambientPlayer.Play();
        ambientPlayer.loop = true;
    }

    /// <summary>
    /// 更新音乐管理器的默认音量
    /// </summary>
    /// <param name="sFXVolume">特效音量</param>
    /// <param name="musicVolume">背景音量</param>
    /// <param name="ambientVolume">环境音量</param>
    /// <param name="mainVolume">主音量</param>
    public void UpdateDefaultVolume(float sFXVolume, float musicVolume, float ambientVolume, float mainVolume)
    {
        DEFAULT_SFX_STRENGTH = sFXVolume;
        DEFAULT_MUSIC_STRENGTH = musicVolume;
        DEFAULT_AMBIENT_STRENGTH = ambientVolume;
        DEFAULT_MAIN_STRENGTH = mainVolume;
    }
}
