using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ƹ������������������Ϊ��״̬����
/// </summary>
public class DeathBehavior : StateMachineBehaviour
{
    [SerializeField] private int scorePoint;
    [SerializeField] private GameObject[] spoils;
    /* �����������Ž��������ٹ��� */
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
