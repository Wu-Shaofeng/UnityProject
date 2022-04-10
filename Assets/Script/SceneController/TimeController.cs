using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ʱ�������(����),����ʵ���ӵ�ʱ��
/// </summary>
public class TimeController : Singleton<TimeController>
{
    /// <summary>�ӵ�ʱ�������ٶ�</summary>
    float bulletTimeScale = 0.1f;
    /// <summary>Ĭ�Ϲ̶�֡ʱ��</summary>
    float defaultFixedDeltaTime;
    /// <summary>�ӵ�ʱ�����ʱ��</summary>
    float duration = 1.0f;
    /// <summary>�ж��Ƿ�������ͣ�˵�</summary>
    public bool isPause = false;

    float currentBulletTimeScale;

    protected override void Awake()
    {
        base.Awake();
        defaultFixedDeltaTime = Time.fixedDeltaTime;
    }
    /// <summary>
    /// ������ͣ�˵�,��¼��ǰ�ӵ�ʱ������
    /// </summary>
    public void Pause()
    {
        isPause = true;
        currentBulletTimeScale = Time.timeScale;
        Time.timeScale = 0f;
    }
    /// <summary>
    /// �����ͣ�˵�,�����ӵ�ʱ������
    /// </summary>
    public void Resume()
    {
        Time.timeScale = currentBulletTimeScale;
        isPause = false;
    }

    /// <summary>
    /// �����ӵ�ʱ��
    /// </summary>
    public void enableBulletTime()
    {
        Time.timeScale = bulletTimeScale;
        StartCoroutine(slowOutCoroutine(duration));
    }
    /// <summary>
    /// <para>ʵ���ӵ�ʱ���Э��</para>
    /// <para>ƽ���ָ�����ʱ�������ٶ�,�˳��ӵ�ʱ��</para>
    /// </summary>
    /// <param name="duration">�˳�����ʱ��</param>
    /// <returns></returns>
    IEnumerator slowOutCoroutine(float duration)
    {
        float t = 0f;
        while(t<1f)
        {
            if (!isPause)// ����ͣ�˵�����ʱ,ִֹͣ��
            {
                t += Time.deltaTime / duration;
                Time.timeScale = Mathf.Lerp(bulletTimeScale, 1, t);
                Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;         
            }
            yield return null;
        }
    }
}
