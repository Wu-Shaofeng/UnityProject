using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 分数结算类
/// 同分数显示类,一个是静态类,一个是普通类
/// </summary>
public class ScoreCalculate : MonoBehaviour
{
    /// <summary>输出分数的文本UI组件</summary>
    [SerializeField] Text scoreText;

    Vector3 updateScoreScale = new Vector3(1f, 1.2f, 1f);

    void Start()
    {
        ScoreManager.instance.ResetScore();
    }
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void EnlargeText()
    {
        scoreText.rectTransform.localScale = updateScoreScale;
    }

    public void RecoverText()
    {
        scoreText.rectTransform.localScale = Vector3.one;
    }
}
