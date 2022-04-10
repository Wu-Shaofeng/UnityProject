using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// BOSS����������
/// </summary>
public class BossGate : MonoBehaviour
{
    /// <summary>�����</summary>
    [SerializeField] GameObject leftGate;
    /// <summary>�Ҵ���</summary>
    [SerializeField] GameObject rightGate;
    /// <summary>BOSS</summary>
    [SerializeField] GameObject boss;
    /// <summary>BOSSѪ��</summary>
    [SerializeField] GameObject bossBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // Boss��������������
        if(bossBar.activeSelf && boss == null)
        {
            OpenGate();
        }
    }
    /// <summary>
    /// ����,�����޷��뿪,ֻ����������֮�������ս��
    /// </summary>
    void CloseGate()
    {
        leftGate.SetActive(true);
        rightGate.SetActive(true);
        leftGate.GetComponent<Animator>().SetTrigger("Close");
        rightGate.GetComponent<Animator>().SetTrigger("Close");
    }
    /// <summary>
    /// ����
    /// </summary>
    public void OpenGate()
    {
        leftGate.SetActive(false);
        rightGate.SetActive(false);
        bossBar.SetActive(false);
    }
    /// <summary>
    /// ����BOSS,����BOSSѪ��
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
