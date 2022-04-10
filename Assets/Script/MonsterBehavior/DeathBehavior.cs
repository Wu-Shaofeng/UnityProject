using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制怪物（动画机对象）死亡行为的状态机类
/// </summary>
public class DeathBehavior : StateMachineBehaviour
{
    [SerializeField] private int scorePoint;
    [SerializeField] private GameObject[] spoils;
    /* 死亡动画播放结束后，销毁怪物 */
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ScoreManager.instance.GainScore(scorePoint);
        int randomIndex = Random.Range(0, 14) / 2;
        bool canGenerate = Random.value > 0.5;
        if(canGenerate)
            PoolManager.Release(spoils[randomIndex], animator.transform.position);
        Destroy(animator.gameObject);
    }
}
