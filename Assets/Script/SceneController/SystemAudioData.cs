using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemAudioData : MonoBehaviour
{
    /// <summary>系统主音量</summary>
    static int mainVolume = 100;
    /// <summary>特效音量</summary>
    static int sFXVolume = 100;
    /// <summary>背景音量</summary>
    static int musicVolume = 100;
    /// <summary>环境音量</summary>
    static int ambientVolume = 100;
    /// <summary>
    /// 获得当前系统主音量
    /// </summary>
    /// <returns>系统主音量</returns>
    public static int getMainVolume()
    {
        return mainVolume;
    }
    /// <summary>
    /// 获得当前系统特效音量
    /// </summary>
    /// <returns>系统特效音量</returns>
    public static int getSFXVolume()
    {
        return sFXVolume;
    }
    /// <summary>
    /// 获得当前系统背景音量
    /// </summary>
    /// <returns>系统背景音量</returns>
    public static int getMusicVolume()
    {
        return musicVolume;
    }
    /// <summary>
    /// 获得当前系统环境音量
    /// </summary>
    /// <returns>系统环境音量</returns>
    public static int getAmbientVolume()
    {
        return ambientVolume;
    }
    /// <summary>
    /// 更新系统音量
    /// </summary>
    /// <param name="main">主音量</param>
    /// <param name="sfx">特效音量</param>
    /// <param name="music">背景音量</param>
    /// <param name="ambient">环境音量</param>
    public static void UpdateAudioData(float main, float sfx, float music, float ambient)
    {
        mainVolume = (int)main;
        sFXVolume = (int)sfx;
        musicVolume = (int)music;
        ambientVolume = (int)ambient;
    }
}
