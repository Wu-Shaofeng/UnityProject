using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ϵͳ����������
/// </summary>
public class ScoreManager : Singleton<ScoreManager>
{
    /// <summary>��ʾ�÷�</summary>
    int score;
    /// <summary>���Ӧ�÷�</summary>
    int currentScore;
    /// <summary>����ͨ�ط���ͳ�Ƶ�UI</summary>
    [SerializeField] ScoreCalculate calculate;

    public bool doneCalculate;

    /// <summary>
    /// ���÷���
    /// </summary>
    public void ResetScore()
    {
        score = 0;
        currentScore = 0;
        ScoreDisplay.UpdateScore(score);
    }
    /// <summary>
    /// ���·���
    /// </summary>
    /// <param name="scorePoint">���λ�õķ���</param>
    public void GainScore(int scorePoint)
    {
        currentScore += scorePoint;
        StartCoroutine(AccelerateScoreCoroutine());
    }
    /// <summary>
    /// ���Ʒ�����̬��������Э��
    /// ������ÿ֡��1���ٶ�������Ŀ��ֵ
    /// ���������з���������
    /// </summary>
    /// <returns></returns>
    IEnumerator AccelerateScoreCoroutine()
    {
        ScoreDisplay.EnlargeText();
        while (score < currentScore)
        {
            score += 1;
            ScoreDisplay.UpdateScore(score);
            yield return null;
        }
        ScoreDisplay.RecoverText();
    }
    /// <summary>
    /// ���¼�¼�ܵ÷֣�����ͨ�ؽ���Ľ���
    /// </summary>
    public void CalculateScore()
    {
        StopAllCoroutines();
        score = 0;
        StartCoroutine(CalculateScoreCoroutine());
    }
    /// <summary>
    /// ����ͨ�ؽ����ܵ÷ֵ�Э��
    /// </summary>
    /// <returns></returns>
    IEnumerator CalculateScoreCoroutine()
    {
        calculate.EnlargeText();
        while (score < currentScore)
        {
            score += 1;
            calculate.UpdateScore(score);
            yield return null;
        }
        calculate.RecoverText();
        doneCalculate = true;
    }
}
