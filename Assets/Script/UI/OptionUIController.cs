using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 音频设置UI控制类
/// </summary>
public class OptionUIController : MonoBehaviour
{
    /// <summary>主音量滑动条</summary>
    [SerializeField] private Slider mainSilder;
    /// <summary>特效音量滑动条</summary>
    [SerializeField] private Slider sFXSilder;
    /// <summary>环境音量滑动条</summary>
    [SerializeField] private Slider ambientSilder;
    /// <summary>背景音量滑动条</summary>
    [SerializeField] private Slider musicSilder;
    /// <summary>返回按钮</summary>
    [SerializeField] private Button returnButton;
    /// <summary>重置按钮</summary>
    [SerializeField] private Button resetButton;
    /// <summary>菜单画布</summary>
    [SerializeField] private Canvas menuCanvas;
    /// <summary>音量设置画布</summary>
    [SerializeField] private Canvas volumeCanvas;
    // 从系统音频类中获取初始化音量
    void Start()
    {
        mainSilder.value = SystemAudioData.getMainVolume();
        ambientSilder.value = SystemAudioData.getAmbientVolume();
        sFXSilder.value = SystemAudioData.getSFXVolume();
        musicSilder.value = SystemAudioData.getMusicVolume();
    }
    // 实时更新音量
    void Update()
    {
        float sFXVolume = sFXSilder.value / 100;
        float musicVolume = musicSilder.value / 100;
        float ambientVolume = ambientSilder.value / 100;
        float mainVolume = mainSilder.value / 100;
        AudioManager.instance.UpdateDefaultVolume(sFXVolume, musicVolume, ambientVolume, mainVolume);
        SystemAudioData.UpdateAudioData(mainSilder.value, sFXSilder.value, musicSilder.value, ambientSilder.value);
    }
    /// <summary>
    /// 为设置界面的按钮添加监听事件
    /// </summary>
    void OnEnable()
    {
        returnButton.onClick.AddListener(OnReturnButtonOnClick);
        resetButton.onClick.AddListener(OnResetButtonOnClick);
    }
    /// <summary>
    /// 解除按钮的监听事件
    /// </summary>
    void OnDisable()
    {
        returnButton.onClick.RemoveAllListeners();
        resetButton.onClick.RemoveAllListeners();
    }
    /// <summary>
    /// 返回按钮的点击响应事件
    /// </summary>
    void OnReturnButtonOnClick()
    {
        volumeCanvas.enabled = false;
        menuCanvas.enabled = true;
    }
    /// <summary>
    /// 重置按钮的点击响应事件
    /// </summary>
    void OnResetButtonOnClick()
    {
        sFXSilder.value = 100;
        ambientSilder.value = 100;
        musicSilder.value = 100;
        mainSilder.value = 100;
    }

}
