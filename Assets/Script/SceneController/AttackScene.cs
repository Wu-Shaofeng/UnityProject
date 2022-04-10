using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����ʵ��ս�������о�ͷ�ζ��Ͷ�֡����
/// </summary>
public class AttackScene : MonoBehaviour
{
    /// <summary>����ģʽ</summary>
    private static AttackScene instance;
    /// <summary>��ȡΨһ���ö���</summary>
    public static AttackScene Instance
    {
        get
        {
            if (instance == null)
                instance = Transform.FindObjectOfType<AttackScene>();
            return instance;
        }
    }
    /// <summary>�жϵ�ǰ��ͷ�Ƿ�ζ�</summary>
    private bool isShake;
    /// <summary>
    /// ��֡,ͣ��֡��Ϊduration��
    /// </summary>
    /// <param name="duration">ͣ��֡��</param>
    public void hitPause(int duration)
    {
        StartCoroutine(Pause(duration));
    }
    /// <summary>
    /// ��ͷ�ζ�,�ζ�ʱ��Ϊduration��,�ζ�����Ϊstrength
    /// </summary>
    /// <param name="duration">�ζ�ʱ��</param>
    /// <param name="strength">�ζ�����</param>
    public void hitShake(float duration, float strength)
    {
        // ��ͷ�ζ�������
        if(!isShake)
            StartCoroutine(Shake(duration, strength));
    }
    /// <summary>
    /// ����ʵ�ֶ�֡��Э�̣����֡
    /// </summary>
    /// <param name="duration">ͣ��֡��</param>
    /// <returns></returns>
    IEnumerator Pause(int duration)
    {
        float pauseTime = duration / 60f;
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1;
    }
    /// <summary>
    /// ����ʵ�ֻζ���Э��,�������ʼ��ΪԲ��,strengthΪ�뾶�����ζ�duration��
    /// </summary>
    /// <param name="duration">�ζ�ʱ��</param>
    /// <param name="strength">�ζ�����</param>
    /// <returns></returns>
    IEnumerator Shake(float duration, float strength)
    {
        isShake = true;
        Transform camera = Camera.main.transform;
        Vector3 originPosition = camera.position;
        while(duration>0)
        {
            camera.position = Random.insideUnitSphere * strength + originPosition;
            duration -= Time.deltaTime;
            yield return null;
        }
        camera.position = originPosition;
        isShake = false;
    }
}
