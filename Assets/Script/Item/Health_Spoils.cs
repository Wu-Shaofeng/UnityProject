using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����ֵս��Ʒ��
/// ʰȡ��ָ�����ֵ
/// </summary>
public class Health_Spoils : MonoBehaviour
{
    /// <summary>�������ֵ</summary>
    [SerializeField] private int health;
    /// <summary>ʰȡ(����)��Ч</summary>
    [SerializeField] private AudioClip drinkSFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<CharacterController>(out CharacterController character))
        {
            AudioManager.instance.PlayRandomSFX(drinkSFX);
            character.Recovery(health, 0);
            Destroy(gameObject);
        }
    }
}
