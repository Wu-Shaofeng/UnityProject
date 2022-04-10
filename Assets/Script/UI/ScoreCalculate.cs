using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ����������
/// ͬ������ʾ��,һ���Ǿ�̬��,һ������ͨ��
/// </summary>
public class ScoreCalculate : MonoBehaviour
{
    /// <summary>����������ı�UI���</summary>
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
