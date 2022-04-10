using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 宝箱战利品类
/// </summary>
public class Total_Spoils : MonoBehaviour
{
    /// <summary>奖励的分数</summary>
    private int treasureScore;
    /// <summary>奖励的生命值</summary>
    private int treasureHealth;
    /// <summary>奖励的法力值</summary>
    private int treasureEnergy;
    /// <summary>拾取音效</summary>
    [SerializeField] AudioClip treasureSFX;

    // 随机配置宝箱掉落物
    void Start()
    {
        treasureScore = Random.Range(2, 10) * 100;
        treasureHealth = Random.Range(20, 40);
        treasureEnergy = Random.Range(20, 60);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 当人物接触宝箱战利品的碰撞体时
        if(collision.gameObject.TryGetComponent<CharacterController>(out CharacterController character))
        {
            AudioManager.instance.PlayRandomSFX(treasureSFX);// 播放拾取音效
            ScoreManager.instance.GainScore(treasureScore);// 获取分数
            character.Recovery(treasureHealth, treasureEnergy);// 回复生命值和法力值
            Destroy(gameObject);
        }
    }
}
