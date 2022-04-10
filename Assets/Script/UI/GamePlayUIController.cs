using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 游戏场景的UI控制类
/// </summary>
public class GamePlayUIController : MonoBehaviour
{
    /// <summary>角色血量蓝量界面</summary>
    [SerializeField] Canvas canvasHUD;
    /// <summary>暂停界面</summary>
    [SerializeField] Canvas canvasPause;
    /// <summary>音频设置界面</summary>
    [SerializeField] Canvas canvasOption;
    /// <summary>游戏结束界面</summary>
    [SerializeField] Canvas canvasGameOver;
    /// <summary>游戏通关界面</summary>
    [SerializeField] Canvas canvasGameClear;
    /// <summary>返回游戏按钮</summary>
    [SerializeField] Button resumeButton;
    /// <summary>设置按钮</summary>
    [SerializeField] Button optionButton;
    /// <summary>返回主菜单按钮</summary>
    [SerializeField] Button returnButton;
    /// <summary>暂停音效</summary>
    [SerializeField] AudioClip pauseCilp;
    /// <summary>返回游戏音效</summary>
    [SerializeField] AudioClip resumeClip;

    /// <summary>HDR血条的数值，人物生命值</summary>
    [SerializeField] Text playerHealth;
    /// <summary>人物</summary>
    [SerializeField] GameObject player;
    /// <summary>游戏结束动画机</summary>
    [SerializeField] Animator animatorGameOver;
    /// <summary>游戏通关动画机</summary>
    [SerializeField] Animator animatorGameClear;

    bool gameOver;

    bool gameClear;
    /// <summary>
    /// 为按钮添加监听
    /// </summary>
    void OnEnable()
    {
        resumeButton.onClick.AddListener(OnResumeButtonOnClick);
        optionButton.onClick.AddListener(OnOptionsButtonOnClick);
        returnButton.onClick.AddListener(OnReturnButtonOnClick);
    }
    /// <summary>
    /// 撤销按钮监听
    /// </summary>
    void OnDisable()
    {
        returnButton.onClick.RemoveAllListeners();
        optionButton.onClick.RemoveAllListeners();
        resumeButton.onClick.RemoveAllListeners();
    }

    void Update()
    {
        // 按下P键，进入/退出暂停菜单
        if (Input.GetKeyDown(KeyCode.P) && !gameOver)
        {
            if (TimeController.instance.isPause)
                Resume();
            else
                Pause();
        }
        if (!gameOver && playerHealth.text == "0")
            GameOver();
        if(player.transform.position.x >= 148 && !gameClear)
            GameClear();
    }
    /// <summary>
    /// 启用暂停菜单
    /// </summary>
    void Pause()
    {
        player.GetComponent<CharacterController>().enabled = false;
        AudioManager.instance.PlayRandomSFX(pauseCilp);
        TimeController.instance.Pause();
        canvasHUD.enabled = false;
        canvasPause.enabled = true;
    }
    /// <summary>
    /// 关闭暂停菜单
    /// </summary>
    void Resume()
    {
        AudioManager.instance.PlayRandomSFX(resumeClip);
        OnResumeButtonOnClick();
        player.GetComponent<CharacterController>().enabled = true;
    }
    /// <summary>
    /// 游戏结束功能
    /// 禁用HDR血条蓝条积分显示和暂停界面
    /// 启动游戏结束界面
    /// 关闭人物的操控脚本，禁止对人物的输入控制
    /// </summary>
    void GameOver()
    {
        AudioManager.instance.CrossFadeToGameOverMode();
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        gameOver = true;
        canvasHUD.enabled = false;
        canvasPause.enabled = false;
        canvasGameOver.enabled = true;
        animatorGameOver.SetTrigger("GameClear");
        StartCoroutine(RestartGame());
    }
    /// <summary>
    /// 重载游戏场景,重新开始的协程
    /// 按任意键重新开始
    /// </summary>
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5.0f);
        while (true)
        {
            if (Input.anyKeyDown)
            {
                animatorGameOver.SetTrigger("Restart");
                canvasGameOver.enabled = false;
                SceneLoader.instance.LoadGameScene();
                break;
            }
            yield return null;
        }
    }
    /// <summary>
    /// 游戏通关功能
    /// 禁用HDR血条蓝条积分显示和暂停界面
    /// 启动游戏通关界面
    /// 关闭人物的操控脚本，禁止对人物的输入控制
    /// </summary>
    void GameClear()
    {
        gameClear = true;
        AudioManager.instance.CrossFadeToGameClearMode();
        player.GetComponent<CharacterController>().enabled = false;
        canvasHUD.enabled = false;
        canvasPause.enabled = false;
        canvasGameClear.enabled = true;
        animatorGameClear.SetTrigger("GameClear");
        ScoreManager.instance.CalculateScore();
        StartCoroutine(ReturnMenu());
    }
    /// <summary>
    /// 返回主菜单的协程
    /// R键返回菜单
    /// </summary>
    /// <returns></returns>
    IEnumerator ReturnMenu()
    {
        yield return new WaitForSeconds(5.0f);
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                canvasGameClear.enabled = false;
                animatorGameClear.SetTrigger("Restart");
                SceneLoader.instance.LoadMenuScene();
                break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// 点击返回游戏按钮的响应函数
    /// </summary>
    void OnResumeButtonOnClick()
    {
        canvasHUD.enabled = true;
        canvasPause.enabled = false;
        TimeController.instance.Resume();
    }
    /// <summary>
    /// 点击设置按钮的响应函数
    /// </summary>
    void OnOptionsButtonOnClick()
    {
        canvasPause.enabled = false;
        canvasOption.enabled = true;
    }
    /// <summary>
    /// 点击返回主菜单按钮的响应函数
    /// </summary>
    void OnReturnButtonOnClick()
    {
        canvasPause.enabled = false;
        SceneLoader.instance.LoadMenuScene();
    }
}
