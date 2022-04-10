using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����ֵս��Ʒ��
/// ʰȡ��ָ�����ֵ
/// </summary>
public class Energy_Spoils : MonoBehaviour
{
    /// <summary>��õķ���ֵ</summary>
    [SerializeField] private int energy;
    /// <summary>ʰȡ(����)��Ч</summary>
    [SerializeField] private AudioClip dringSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<CharacterController>(out CharacterController character))
        {
            AudioManager.instance.PlayRandomSFX(dringSFX);
            character.Recovery(0, energy);
            Destroy(gameObject);
        }
    }
}
