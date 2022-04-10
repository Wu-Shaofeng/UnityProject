using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 场景加载类
/// 负责场景的淡入淡出重载和切换
/// </summary>
public class SceneLoader : Singleton<SceneLoader>
{
    const string GAMEPLAY = "Game";
    const string MAINMENU = "Menu";
    /// <summary>
    /// 淡入淡出时间
    /// </summary>
    [SerializeField] float fadeTime = 3.0f;
    /// <summary>
    /// 淡入淡出幕布
    /// </summary>
    [SerializeField] UnityEngine.UI.Image fadeCanvas;

    Color color;
    /// <summary>
    /// 直接加载场景
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    /// <summary>
    /// 加载游戏场景
    /// </summary>
    public void LoadGameScene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadCoroutine(GAMEPLAY));
        AudioManager.instance.TurnOnAmbientMusic();
        AudioManager.instance.CrossFadeToTravelMode(true);
    }
    /// <summary>
    /// 加载主菜单场景
    /// </summary>
    public void LoadMenuScene()
    {
        StopAllCoroutines();
        AudioManager.instance.TurnOffAmbientMusic();
        StartCoroutine(LoadCoroutine(MAINMENU));
        AudioManager.instance.CrossFadeToMenuMode();
    }

    /// <summary>
    /// 异步淡入淡出加载场景的协程
    /// </summary>
    /// <param name="sceneName">场景名称</param>
    /// <returns></returns>
    IEnumerator LoadCoroutine(string sceneName)
    {
        var loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        // 淡出前禁止场景激活
        loadingOperation.allowSceneActivation = false;

        fadeCanvas.gameObject.SetActive(true);
        // 淡出
        while(color.a < 1f)
        {
            color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / fadeTime);
            fadeCanvas.color = color;
            yield return null;
        }
        // 等待新场景异步加载超过90%
        yield return new WaitUntil(() => loadingOperation.progress >= 0.9f);

        // 激活场景
        loadingOperation.allowSceneActivation = true;
        // 淡入
        while (color.a > 0f)
        {
            color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / fadeTime);
            fadeCanvas.color = color;
            yield return null;
        }
        fadeCanvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

}
