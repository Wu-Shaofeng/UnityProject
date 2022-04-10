using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ͷ������
/// </summary>
public class CameraFollow : MonoBehaviour
{
    /// <summary>��ͷ�������</summary>
    [SerializeField] private Transform target;
    /// <summary>��ͷ�ƶ�ƽ��ֵ</summary>
    [SerializeField] private float smooth;
    /// <summary>��ͷ��y��ƫ��</summary>
    [SerializeField] private float yoffset;

    /// <summary>�������ƶ���������Եʱ����ͷ����ˮƽ�ƶ�</summary>
    bool keepXStatic;
    /// <summary>�����������������ʱ����ͷ������ֱ�ƶ�</summary>
    bool keepYStatic;
    /// <summary>�Ƿ�֪ͨ���������,�����ѵ�������</summary>
    bool hasInformed;

    void Start()
    {

    }

    void Update()
    {
        if (target != null)
        {
            if (target.position.x <= -53.35 || target.position.x >= 139)
                keepXStatic = true;
            else
                keepXStatic = false;
            if (target.position.y <= -10)
                keepYStatic = true;
            else
                keepYStatic = false;
            if (target.position.y <= -15 && !hasInformed)// ��������ȫ����������
                DropOutOfTheScene();
        }
    }

    void LateUpdate()
    {
        if(target!=null)// ��Ŀ�겻Ϊ��ʱ����ͷ��������ƶ�
        {
            Vector3 position;
            if (keepXStatic)
                position = new Vector3(transform.position.x, target.position.y + yoffset, transform.position.z);
            else
                position = new Vector3(target.position.x, target.position.y + yoffset, transform.position.z);
            if (keepYStatic)
                position.y = transform.position.y;
            if (transform.position != position) 
                transform.position = Vector3.Lerp(transform.position, position, smooth);
        }
    }
    /// <summary>
    /// ���������⣬��ɫ����
    /// </summary>
    void DropOutOfTheScene()
    {
        hasInformed = true;
        StartCoroutine(SustainedInjury());
    }
    /// <summary>
    /// ����������,���������Ѫ
    /// </summary>
    /// <returns></returns>
    IEnumerator SustainedInjury()
    {
        int count = 10;// ��ȡ��ҵ�Ѫ���Ŀ�������ֱ�ӿ�100����ֵ�Ŀ���С
        while (count > 0)
        {
            target.gameObject.GetComponent<CharacterController>().getDamage(10);
            yield return new WaitForSeconds(0.2f);
            count--;
        }
    }
}
