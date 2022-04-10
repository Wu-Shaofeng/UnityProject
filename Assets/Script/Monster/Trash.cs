using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����αװ���ϵ����֣�ƽʱ���Լ�αװ�ɾ���ȴ�����ӽ�ʱ�Ÿ�������һ��
/// ע:С��������!!!
/// </summary>
public class Trash : MeleeMonster
{
    /// <summary>
    /// ��ȡ��ǰ���ŵĶ�����
    /// </summary>
    /// <returns>��ǰ���ŵĶ�����</returns>
    public string getCurrentClipInfo()
    {
        return animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
    }

    void Update()
    {
        /// �����ﴦ�����÷���״̬������Ѫ���������Ч��
        if ((getCurrentClipInfo() == "TrashIdle") && healthBar.gameObject.activeSelf)
            onDisableHealthBar();
        /// �����ﴦ��׷��״̬����ʾѪ��������ս��״̬
        else if (getCurrentClipInfo() == "TrashChase" && !healthBar.gameObject.activeSelf)
            onEnableHealthBar();
    }
}
