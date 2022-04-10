using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemAudioData : MonoBehaviour
{
    /// <summary>ϵͳ������</summary>
    static int mainVolume = 100;
    /// <summary>��Ч����</summary>
    static int sFXVolume = 100;
    /// <summary>��������</summary>
    static int musicVolume = 100;
    /// <summary>��������</summary>
    static int ambientVolume = 100;
    /// <summary>
    /// ��õ�ǰϵͳ������
    /// </summary>
    /// <returns>ϵͳ������</returns>
    public static int getMainVolume()
    {
        return mainVolume;
    }
    /// <summary>
    /// ��õ�ǰϵͳ��Ч����
    /// </summary>
    /// <returns>ϵͳ��Ч����</returns>
    public static int getSFXVolume()
    {
        return sFXVolume;
    }
    /// <summary>
    /// ��õ�ǰϵͳ��������
    /// </summary>
    /// <returns>ϵͳ��������</returns>
    public static int getMusicVolume()
    {
        return musicVolume;
    }
    /// <summary>
    /// ��õ�ǰϵͳ��������
    /// </summary>
    /// <returns>ϵͳ��������</returns>
    public static int getAmbientVolume()
    {
        return ambientVolume;
    }
    /// <summary>
    /// ����ϵͳ����
    /// </summary>
    /// <param name="main">������</param>
    /// <param name="sfx">��Ч����</param>
    /// <param name="music">��������</param>
    /// <param name="ambient">��������</param>
    public static void UpdateAudioData(float main, float sfx, float music, float ambient)
    {
        mainVolume = (int)main;
        sFXVolume = (int)sfx;
        musicVolume = (int)music;
        ambientVolume = (int)ambient;
    }
}
