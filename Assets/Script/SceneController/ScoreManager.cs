using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 系统分数管理类
/// </summary>
public class ScoreManager : Singleton<ScoreManager>
{
    /// <summary>显示得分</summary>
    int score;
    /// <summary>玩家应得分</summary>
    int currentScore;
    /// <summary>用于通关分数统计的UI</summary>
    [SerializeField] ScoreCalculate calculate;

    public bool doneCalculate;

    /// <summary>
    /// 重置分数
    /// </summary>
    public void ResetScore()
    {
        score = 0;
        currentScore = 0;
        ScoreDisplay.UpdateScore(score);
    }
    /// <summary>
    /// 更新分数
    /// </summary>
    /// <param name="scorePoint">本次获得的分数</param>
    public void GainScore(int scorePoint)
    {
        currentScore += scorePoint;
        StartCoroutine(AccelerateScoreCoroutine());
    }
    /// <summary>
    /// 控制分数动态逐步增长的协程
    /// 分数以每帧加1的速度增长至目标值
    /// 增长过程中分数字体变大
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
    /// 重新记录总得分，用于通关界面的结算
    /// </summary>
    public void CalculateScore()
    {
        StopAllCoroutines();
        score = 0;
        StartCoroutine(CalculateScoreCoroutine());
    }
    /// <summary>
    /// 结算通关界面总得分的协程
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
