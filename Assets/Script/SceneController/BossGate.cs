using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// BOSS出场控制类
/// </summary>
public class BossGate : MonoBehaviour
{
    /// <summary>左大门</summary>
    [SerializeField] GameObject leftGate;
    /// <summary>右大门</summary>
    [SerializeField] GameObject rightGate;
    /// <summary>BOSS</summary>
    [SerializeField] GameObject boss;
    /// <summary>BOSS血条</summary>
    [SerializeField] GameObject bossBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // Boss死亡，开启大门
        if(bossBar.activeSelf && boss == null)
        {
            OpenGate();
        }
    }
    /// <summary>
    /// 关门,人物无法离开,只能在两扇门之间和人物战斗
    /// </summary>
    void CloseGate()
    {
        leftGate.SetActive(true);
        rightGate.SetActive(true);
        leftGate.GetComponent<Animator>().SetTrigger("Close");
        rightGate.GetComponent<Animator>().SetTrigger("Close");
    }
    /// <summary>
    /// 开门
    /// </summary>
    public void OpenGate()
    {
        leftGate.SetActive(false);
        rightGate.SetActive(false);
        bossBar.SetActive(false);
    }
    /// <summary>
    /// 唤醒BOSS,启动BOSS血条
    /// </summary>
    void AwakeBoss()
    {
        boss.SetActive(true);
        bossBar.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CharacterController>(out CharacterController character))
        {
            if (boss != null)
            {
                CloseGate();
                AwakeBoss();
            }
        }
    }
}
