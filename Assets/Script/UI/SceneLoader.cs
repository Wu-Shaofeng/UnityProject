using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// ����������
/// ���𳡾��ĵ��뵭�����غ��л�
/// </summary>
public class SceneLoader : Singleton<SceneLoader>
{
    const string GAMEPLAY = "Game";
    const string MAINMENU = "Menu";
    /// <summary>
    /// ���뵭��ʱ��
    /// </summary>
    [SerializeField] float fadeTime = 3.0f;
    /// <summary>
    /// ���뵭��Ļ��
    /// </summary>
    [SerializeField] UnityEngine.UI.Image fadeCanvas;

    Color color;
    /// <summary>
    /// ֱ�Ӽ��س���
    /// </summary>
    /// <param name="sceneName">��������</param>
    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    /// <summary>
    /// ������Ϸ����
    /// </summary>
    public void LoadGameScene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadCoroutine(GAMEPLAY));
        AudioManager.instance.TurnOnAmbientMusic();
        AudioManager.instance.CrossFadeToTravelMode(true);
    }
    /// <summary>
    /// �������˵�����
    /// </summary>
    public void LoadMenuScene()
    {
        StopAllCoroutines();
        AudioManager.instance.TurnOffAmbientMusic();
        StartCoroutine(LoadCoroutine(MAINMENU));
        AudioManager.instance.CrossFadeToMenuMode();
    }

    /// <summary>
    /// �첽���뵭�����س�����Э��
    /// </summary>
    /// <param name="sceneName">��������</param>
    /// <returns></returns>
    IEnumerator LoadCoroutine(string sceneName)
    {
        var loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        // ����ǰ��ֹ��������
        loadingOperation.allowSceneActivation = false;

        fadeCanvas.gameObject.SetActive(true);
        // ����
        while(color.a < 1f)
        {
            color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / fadeTime);
            fadeCanvas.color = color;
            yield return null;
        }
        // �ȴ��³����첽���س���90%
        yield return new WaitUntil(() => loadingOperation.progress >= 0.9f);

        // �����
        loadingOperation.allowSceneActivation = true;
        // ����
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
