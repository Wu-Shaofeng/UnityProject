using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 分数战利品类
/// 拾取后获得分数
/// </summary>
public class Score_Spoils : MonoBehaviour
{
    /// <summary>获取分数</summary>
    [SerializeField] private int score;
    /// <summary>拾取音效</summary>
    [SerializeField] private AudioClip scoreCilp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<CharacterController>(out CharacterController character))
        {
            AudioManager.instance.PlayRandomSFX(scoreCilp);
            ScoreManager.instance.GainScore(score);
            Destroy(gameObject);
        }
    }
}
