using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���˵�����������(�ֱ�)
/// ������ͼƬ�����ƶ�ʵ��
/// </summary>
public class BackgroundScroll : MonoBehaviour
{
    /// <summary>���ͼƬ</summary>
    [SerializeField] GameObject front;
    /// <summary>�Ҳ�ͼƬ</summary>
    [SerializeField] GameObject back;
    /// <summary>�����ٶ�</summary>
    [SerializeField] float moveSpeed;
    /// <summary>����swap���м�ֵ</summary>
    GameObject temp;
    /// <summary>���ͼƬ����ʼλ��</summary>
    Vector3 leftPosition;
    /// <summary>�Ҳ�ͼƬ����ʼλ��</summary>
    Vector3 rightPosition;

    void Start()
    {
        leftPosition = front.transform.position;// ��¼��ʼ��
        rightPosition = back.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // ����ͼƬ��������
        front.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        back.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        float frontX = front.transform.position.x;
        float backX = back.transform.position.x;
        if (frontX <= leftPosition.x && backX <= leftPosition.x)// ���Ҳ�ͼƬ�������ͼƬ����ʼλ�ã�����ͼƬ����
        {
            if (backX < frontX)
            {
                front.transform.position = leftPosition;
                back.transform.position = rightPosition;
            }
            else
            {
                front.transform.position = rightPosition;
                back.transform.position = leftPosition;
            }
        }
    }
}
