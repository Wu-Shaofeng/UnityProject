using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ������ʾ��
/// ��������ʾ�ڶ�Ӧ�ı������
/// </summary>
public class ScoreDisplay : MonoBehaviour
{
    /// <summary>����������ı�UI���</summary>
    static Text scoreText;
    /// <summary>���·���ʱ���ı�����</summary>
    static Vector3 updateScoreScale = new Vector3(1f, 1.2f, 1f);


    void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    void Start()
    {
        ScoreManager.instance.ResetScore();
    }
    /// <summary>
    /// ���·������ı������
    /// </summary>
    /// <param name="score">����</param>
    public static void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
    /// <summary>
    /// �����ı�����Ĵ�С
    /// �ڶ�̬���·���ʱ,�������Ŵ�
    /// </summary>
    public static void EnlargeText()
    {
        scoreText.rectTransform.localScale = updateScoreScale;
    }
    /// <summary>
    /// �ָ��ı�����Ĵ�С
    /// �ڽ������·���ʱ,������С
    /// </summary>
    public static void RecoverText()
    {
        scoreText.rectTransform.localScale = Vector3.one;
    }
}
