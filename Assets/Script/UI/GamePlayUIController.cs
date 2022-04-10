using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ��Ϸ������UI������
/// </summary>
public class GamePlayUIController : MonoBehaviour
{
    /// <summary>��ɫѪ����������</summary>
    [SerializeField] Canvas canvasHUD;
    /// <summary>��ͣ����</summary>
    [SerializeField] Canvas canvasPause;
    /// <summary>��Ƶ���ý���</summary>
    [SerializeField] Canvas canvasOption;
    /// <summary>��Ϸ��������</summary>
    [SerializeField] Canvas canvasGameOver;
    /// <summary>��Ϸͨ�ؽ���</summary>
    [SerializeField] Canvas canvasGameClear;
    /// <summary>������Ϸ��ť</summary>
    [SerializeField] Button resumeButton;
    /// <summary>���ð�ť</summary>
    [SerializeField] Button optionButton;
    /// <summary>�������˵���ť</summary>
    [SerializeField] Button returnButton;
    /// <summary>��ͣ��Ч</summary>
    [SerializeField] AudioClip pauseCilp;
    /// <summary>������Ϸ��Ч</summary>
    [SerializeField] AudioClip resumeClip;

    /// <summary>HDRѪ������ֵ����������ֵ</summary>
    [SerializeField] Text playerHealth;
    /// <summary>����</summary>
    [SerializeField] GameObject player;
    /// <summary>��Ϸ����������</summary>
    [SerializeField] Animator animatorGameOver;
    /// <summary>��Ϸͨ�ض�����</summary>
    [SerializeField] Animator animatorGameClear;

    bool gameOver;

    bool gameClear;
    /// <summary>
    /// Ϊ��ť��Ӽ���
    /// </summary>
    void OnEnable()
    {
        resumeButton.onClick.AddListener(OnResumeButtonOnClick);
        optionButton.onClick.AddListener(OnOptionsButtonOnClick);
        returnButton.onClick.AddListener(OnReturnButtonOnClick);
    }
    /// <summary>
    /// ������ť����
    /// </summary>
    void OnDisable()
    {
        returnButton.onClick.RemoveAllListeners();
        optionButton.onClick.RemoveAllListeners();
        resumeButton.onClick.RemoveAllListeners();
    }

    void Update()
    {
        // ����P��������/�˳���ͣ�˵�
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
    /// ������ͣ�˵�
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
    /// �ر���ͣ�˵�
    /// </summary>
    void Resume()
    {
        AudioManager.instance.PlayRandomSFX(resumeClip);
        OnResumeButtonOnClick();
        player.GetComponent<CharacterController>().enabled = true;
    }
    /// <summary>
    /// ��Ϸ��������
    /// ����HDRѪ������������ʾ����ͣ����
    /// ������Ϸ��������
    /// �ر�����Ĳٿؽű�����ֹ��������������
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
    /// ������Ϸ����,���¿�ʼ��Э��
    /// ����������¿�ʼ
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
    /// ��Ϸͨ�ع���
    /// ����HDRѪ������������ʾ����ͣ����
    /// ������Ϸͨ�ؽ���
    /// �ر�����Ĳٿؽű�����ֹ��������������
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
    /// �������˵���Э��
    /// R�����ز˵�
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
    /// ���������Ϸ��ť����Ӧ����
    /// </summary>
    void OnResumeButtonOnClick()
    {
        canvasHUD.enabled = true;
        canvasPause.enabled = false;
        TimeController.instance.Resume();
    }
    /// <summary>
    /// ������ð�ť����Ӧ����
    /// </summary>
    void OnOptionsButtonOnClick()
    {
        canvasPause.enabled = false;
        canvasOption.enabled = true;
    }
    /// <summary>
    /// ����������˵���ť����Ӧ����
    /// </summary>
    void OnReturnButtonOnClick()
    {
        canvasPause.enabled = false;
        SceneLoader.instance.LoadMenuScene();
    }
}
