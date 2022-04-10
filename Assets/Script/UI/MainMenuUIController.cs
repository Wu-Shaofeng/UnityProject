using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    /// <summary>���˵�����</summary>
    [SerializeField] Canvas mainMenuCanvas;
    /// <summary>���ò˵�����</summary>
    [SerializeField] Canvas optionMenuCanvas;
    /// <summary>��ʼ��Ϸ��ť</summary>
    [SerializeField] Button startGameButton;
    /// <summary>���ð�ť</summary>
    [SerializeField] Button optionButton;
    /// <summary>�˳���Ϸ��ť</summary>
    [SerializeField] Button quitButton;
    /// <summary>
    /// Ϊ��ť���ü���
    /// </summary>
    void OnEnable()
    {
        startGameButton.onClick.AddListener(OnStartGameButtonOnClick);
        optionButton.onClick.AddListener(OnOptionButtonOnClick);
        quitButton.onClick.AddListener(OnQuitButtonOnClick);
    }

    void OnDisable()
    {
        startGameButton.onClick.RemoveAllListeners();
        optionButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }
    /// <summary>
    /// �������˵�����ɴ���ͣ�˵���ת������Ҫ���׵ؽ�����ͣ��ʱ��ָ�
    /// </summary>
    void Start()
    {
        Time.timeScale = 1f;    
    }
    /// <summary>
    /// �����ʼ��Ϸ��ť����Ӧ����
    /// </summary>
    void OnStartGameButtonOnClick()
    {
        mainMenuCanvas.enabled = false;
        SceneLoader.instance.LoadGameScene();
    }
    /// <summary>
    /// ������ð�ť����Ӧ����
    /// </summary>
    void OnOptionButtonOnClick()
    {
        mainMenuCanvas.enabled = false;
        optionMenuCanvas.enabled = true;
    }
    /// <summary>
    /// ����˳���Ϸ��ť����Ӧ����
    /// </summary>
    void OnQuitButtonOnClick()
    {
    #if UNITY_EDITOR // ���ڱ༭������
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
