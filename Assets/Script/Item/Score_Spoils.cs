using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����ս��Ʒ��
/// ʰȡ���÷���
/// </summary>
public class Score_Spoils : MonoBehaviour
{
    /// <summary>��ȡ����</summary>
    [SerializeField] private int score;
    /// <summary>ʰȡ��Ч</summary>
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
