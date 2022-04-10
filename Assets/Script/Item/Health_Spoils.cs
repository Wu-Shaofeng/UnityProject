using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 生命值战利品类
/// 拾取后恢复生命值
/// </summary>
public class Health_Spoils : MonoBehaviour
{
    /// <summary>获得生命值</summary>
    [SerializeField] private int health;
    /// <summary>拾取(饮用)音效</summary>
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
