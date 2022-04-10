using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ��ʾUI��
/// ����ָ��
/// </summary>
public class EnterDialog : MonoBehaviour
{
    /// <summary>������ʾ��ʾ�Ļ�������</summary>
    [SerializeField] GameObject dialogPannel;
    /// <summary>��ʾ�ı����</summary>
    [SerializeField] Text dialogText;
    /// <summary>��ʾ����</summary>
    [SerializeField] string str;

    // ������ײ������ʾ��ʾ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<CharacterController>(out CharacterController character))
        {
            dialogText.text = str;
            dialogPannel.SetActive(true);
        }
    }
    // �뿪��ײ�����ʾ��ʧ
    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogPannel.SetActive(false);
    }
}
