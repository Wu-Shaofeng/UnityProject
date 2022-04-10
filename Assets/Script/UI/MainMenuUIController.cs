using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    /// <summary>主菜单画布</summary>
    [SerializeField] Canvas mainMenuCanvas;
    /// <summary>设置菜单画布</summary>
    [SerializeField] Canvas optionMenuCanvas;
    /// <summary>开始游戏按钮</summary>
    [SerializeField] Button startGameButton;
    /// <summary>设置按钮</summary>
    [SerializeField] Button optionButton;
    /// <summary>退出游戏按钮</summary>
    [SerializeField] Button quitButton;
    /// <summary>
    /// 为按钮设置监听
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
    /// 由于主菜单界面可从暂停菜单跳转，故需要稳妥地将被暂停的时间恢复
    /// </summary>
    void Start()
    {
        Time.timeScale = 1f;    
    }
    /// <summary>
    /// 点击开始游戏按钮的响应函数
    /// </summary>
    void OnStartGameButtonOnClick()
    {
        mainMenuCanvas.enabled = false;
        SceneLoader.instance.LoadGameScene();
    }
    /// <summary>
    /// 点击设置按钮的响应函数
    /// </summary>
    void OnOptionButtonOnClick()
    {
        mainMenuCanvas.enabled = false;
        optionMenuCanvas.enabled = true;
    }
    /// <summary>
    /// 点击退出游戏按钮的响应函数
    /// </summary>
    void OnQuitButtonOnClick()
    {
    #if UNITY_EDITOR // 若在编辑器运行
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
