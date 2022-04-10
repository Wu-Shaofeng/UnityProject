using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ��Ƶ����UI������
/// </summary>
public class OptionUIController : MonoBehaviour
{
    /// <summary>������������</summary>
    [SerializeField] private Slider mainSilder;
    /// <summary>��Ч����������</summary>
    [SerializeField] private Slider sFXSilder;
    /// <summary>��������������</summary>
    [SerializeField] private Slider ambientSilder;
    /// <summary>��������������</summary>
    [SerializeField] private Slider musicSilder;
    /// <summary>���ذ�ť</summary>
    [SerializeField] private Button returnButton;
    /// <summary>���ð�ť</summary>
    [SerializeField] private Button resetButton;
    /// <summary>�˵�����</summary>
    [SerializeField] private Canvas menuCanvas;
    /// <summary>�������û���</summary>
    [SerializeField] private Canvas volumeCanvas;
    // ��ϵͳ��Ƶ���л�ȡ��ʼ������
    void Start()
    {
        mainSilder.value = SystemAudioData.getMainVolume();
        ambientSilder.value = SystemAudioData.getAmbientVolume();
        sFXSilder.value = SystemAudioData.getSFXVolume();
        musicSilder.value = SystemAudioData.getMusicVolume();
    }
    // ʵʱ��������
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
    /// Ϊ���ý���İ�ť��Ӽ����¼�
    /// </summary>
    void OnEnable()
    {
        returnButton.onClick.AddListener(OnReturnButtonOnClick);
        resetButton.onClick.AddListener(OnResetButtonOnClick);
    }
    /// <summary>
    /// �����ť�ļ����¼�
    /// </summary>
    void OnDisable()
    {
        returnButton.onClick.RemoveAllListeners();
        resetButton.onClick.RemoveAllListeners();
    }
    /// <summary>
    /// ���ذ�ť�ĵ����Ӧ�¼�
    /// </summary>
    void OnReturnButtonOnClick()
    {
        volumeCanvas.enabled = false;
        menuCanvas.enabled = true;
    }
    /// <summary>
    /// ���ð�ť�ĵ����Ӧ�¼�
    /// </summary>
    void OnResetButtonOnClick()
    {
        sFXSilder.value = 100;
        ambientSilder.value = 100;
        musicSilder.value = 100;
        mainSilder.value = 100;
    }

}
