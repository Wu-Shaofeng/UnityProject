using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 法力值战利品类
/// 拾取后恢复法力值
/// </summary>
public class Energy_Spoils : MonoBehaviour
{
    /// <summary>获得的法力值</summary>
    [SerializeField] private int energy;
    /// <summary>拾取(饮用)音效</summary>
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
