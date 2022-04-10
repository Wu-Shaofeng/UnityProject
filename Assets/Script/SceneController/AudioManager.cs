using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��Ƶ������
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    /// <summary>��Ч������</summary>
    [SerializeField] AudioSource sFXPlayer;
    /// <summary>���ֲ�����</summary>
    [SerializeField] grumbleAMP gA;
    /// <summary>������������</summary>
    [SerializeField] AudioSource ambientPlayer;

    [SerializeField] float lowVolume;
    float OrignVolume;

    /// <summary>��Ƶ</summary>
    const float MIN_PITCH = 0.9f;
    /// <summary>��Ƶ</summary>
    const float MAX_PITCH = 1.1F;

    /// <summary>Ĭ����Ч��Ч����ǿ��</summary>
    float DEFAULT_SFX_STRENGTH = 1.0f;
    /// <summary>Ĭ�ϻ���������ǿ��</summary>
    float DEFAULT_MUSIC_STRENGTH = 1.0f;
    /// <summary>Ĭ�ϱ���������ǿ��</summary>
    float DEFAULT_AMBIENT_STRENGTH = 1.0f;
    /// <summary>Ĭ������Ч����ǿ��</summary>
    float DEFAULT_MAIN_STRENGTH = 1.0f;

    /// <summary>
    /// ʵʱ������Ƶ������������,������С������ҳ�����
    /// </summary>
    void Update()
    {
        ambientPlayer.volume = DEFAULT_AMBIENT_STRENGTH * DEFAULT_MAIN_STRENGTH;
        gA.setGlobalVolume(DEFAULT_MUSIC_STRENGTH * DEFAULT_MAIN_STRENGTH);
    }
    /// <summary>
    /// <para>��Ӧ������ģʽ</para>
    /// <para>ͨ����ǰ���״̬�л���Ӧ������</para>
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
    /// ������Ϸ��10�뿪ʼ��������
    /// </summary>
    void Start()
    {
        OrignVolume = ambientPlayer.volume;
        gA.PlaySong("MenuTrail", 0, 3.0f);
    }

    /// <summary>
    /// ������Ч
    /// </summary>
    /// <param name="audioClip">��Ч</param>
    public void PlaySFX(AudioClip audioClip)
    {
        sFXPlayer.PlayOneShot(audioClip, DEFAULT_SFX_STRENGTH * DEFAULT_MAIN_STRENGTH);
    }

    /// <summary>
    /// ���Զ��岥����Ч
    /// </summary>
    /// <param name="audioClip">��Ч</param>
    /// <param name="volume">�Զ�������</param>
    public void PlaySFX(AudioClip audioClip, float volume)
    {
        sFXPlayer.PlayOneShot(audioClip, DEFAULT_SFX_STRENGTH * DEFAULT_MAIN_STRENGTH * volume);
    }

    /// <summary>
    /// ������[0.9, 1.1]֮�����ѡȡһ��Ƶ�ʣ�������Ч
    /// </summary>
    /// <param name="audioClip">��Ч</param>
    public void PlayRandomSFX(AudioClip audioClip)
    {
        sFXPlayer.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        PlaySFX(audioClip);
    }

    /// <summary>
    /// ������[0.9, 1.1]֮�����ѡȡһ��Ƶ�ʣ����Զ�������������Ч
    /// </summary>
    /// <param name="audioClip">��Ч</param>
    /// <param name="volume">�Զ�������</param>
    public void PlayRandomSFX(AudioClip audioClip, float volume)
    {
        sFXPlayer.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        PlaySFX(audioClip, volume);
    }

    /// <summary>
    /// ������[0.9, 1.1]֮�����ѡȡһ��Ƶ�ʣ����������Ч�б��е�һ����Ч
    /// </summary>
    /// <param name="audioClip">��Ч�б�</param>
    public void PlayRandomSFX(AudioClip[] audioClip)
    {
        PlayRandomSFX(audioClip[Random.Range(0, audioClip.Length)]);
    }

    /// <summary>
    /// ������[0.9, 1.1]֮�����ѡȡһ��Ƶ�ʣ����Զ����������������Ч�б��е�һ����Ч
    /// </summary>
    /// <param name="audioClip">��Ч�б�</param>
    public void PlayRandomSFX(AudioClip[] audioClip, float volume)
    {
        PlayRandomSFX(audioClip[Random.Range(0, audioClip.Length)], volume);
    }

    /// <summary>
    /// �����������л�������ģʽ
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
    /// �����������л���ս��ģʽ
    /// </summary>
    public void CrossFadeToBattleMode()
    {
        if (mode == adaptiveAudioMode.Battle || mode == adaptiveAudioMode.GameOver)// ����Ϸ�������л�����
            return;
        mode = adaptiveAudioMode.Battle;
        int layerIndex = Random.Range(0, 2);
        gA.CrossFadeToNewSong("BattleTrail", layerIndex);
    }
    
    /// <summary>
    /// �����������л������˵�����
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
    /// �����������л���ս����
    /// </summary>
    public void CrossFadeToGameOverMode()
    {
        mode = adaptiveAudioMode.GameOver;
        gA.CrossFadeToNewSong("GameOverTrail", 0);
    }

    /// <summary>
    /// �����������л���ʤ����
    /// </summary>
    public void CrossFadeToGameClearMode()
    {
        mode = adaptiveAudioMode.GameClear;
        gA.CrossFadeToNewSong("GameClearTrail", 0);
    }

    /// <summary>
    /// ���ͻ�����,����
    /// �������ƶ����ϰ����·�ʱ���øú���
    /// ��������������
    /// </summary>
    public void TurnDownAmbientMusic()
    {
        ambientPlayer.volume = lowVolume * DEFAULT_AMBIENT_STRENGTH * DEFAULT_MAIN_STRENGTH;
    }

    /// <summary>
    /// ��߻�����,����
    /// ������ͷ�����ϰ���ʱ���øú���
    /// ��������������
    /// </summary>
    public void TurnUpAmbientMusic()
    {
        ambientPlayer.volume = OrignVolume * DEFAULT_AMBIENT_STRENGTH * DEFAULT_MAIN_STRENGTH;
    }

    /// <summary>
    /// �رջ�����
    /// ���л�����������ʱ����
    /// </summary>
    public void TurnOffAmbientMusic()
    {
        ambientPlayer.Stop();
    }

    /// <summary>
    /// ����������
    /// ���л�����Ϸ����ʱ����
    /// </summary>
    public void TurnOnAmbientMusic()
    {
        ambientPlayer.Play();
        ambientPlayer.loop = true;
    }

    /// <summary>
    /// �������ֹ�������Ĭ������
    /// </summary>
    /// <param name="sFXVolume">��Ч����</param>
    /// <param name="musicVolume">��������</param>
    /// <param name="ambientVolume">��������</param>
    /// <param name="mainVolume">������</param>
    public void UpdateDefaultVolume(float sFXVolume, float musicVolume, float ambientVolume, float mainVolume)
    {
        DEFAULT_SFX_STRENGTH = sFXVolume;
        DEFAULT_MUSIC_STRENGTH = musicVolume;
        DEFAULT_AMBIENT_STRENGTH = ambientVolume;
        DEFAULT_MAIN_STRENGTH = mainVolume;
    }
}
