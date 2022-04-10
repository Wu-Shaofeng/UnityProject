using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����ս��Ʒ��
/// </summary>
public class Total_Spoils : MonoBehaviour
{
    /// <summary>�����ķ���</summary>
    private int treasureScore;
    /// <summary>����������ֵ</summary>
    private int treasureHealth;
    /// <summary>�����ķ���ֵ</summary>
    private int treasureEnergy;
    /// <summary>ʰȡ��Ч</summary>
    [SerializeField] AudioClip treasureSFX;

    // ������ñ��������
    void Start()
    {
        treasureScore = Random.Range(2, 10) * 100;
        treasureHealth = Random.Range(20, 40);
        treasureEnergy = Random.Range(20, 60);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ������Ӵ�����ս��Ʒ����ײ��ʱ
        if(collision.gameObject.TryGetComponent<CharacterController>(out CharacterController character))
        {
            AudioManager.instance.PlayRandomSFX(treasureSFX);// ����ʰȡ��Ч
            ScoreManager.instance.GainScore(treasureScore);// ��ȡ����
            character.Recovery(treasureHealth, treasureEnergy);// �ظ�����ֵ�ͷ���ֵ
            Destroy(gameObject);
        }
    }
}
