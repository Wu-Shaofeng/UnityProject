using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 分数显示类
/// 将分数显示在对应文本组件中
/// </summary>
public class ScoreDisplay : MonoBehaviour
{
    /// <summary>输出分数的文本UI组件</summary>
    static Text scoreText;
    /// <summary>更新分数时的文本缩放</summary>
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
    /// 更新分数到文本组件上
    /// </summary>
    /// <param name="score">分数</param>
    public static void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
    /// <summary>
    /// 增大文本组件的大小
    /// 在动态更新分数时,分数被放大
    /// </summary>
    public static void EnlargeText()
    {
        scoreText.rectTransform.localScale = updateScoreScale;
    }
    /// <summary>
    /// 恢复文本组件的大小
    /// 在结束更新分数时,分数缩小
    /// </summary>
    public static void RecoverText()
    {
        scoreText.rectTransform.localScale = Vector3.one;
    }
}
