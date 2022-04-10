using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
/// <summary>
/// �����࣬ʵ�ֳ������ȵ�Ƶ��
/// </summary>
public class Lightning : MonoBehaviour
{
    /// <summary>2D��Դ</summary>
    private Light2D light2D;
    /// <summary>��˸ʱ��</summary>
    [SerializeField] private float flashTime;
    /// <summary>�谵����</summary>
    [SerializeField] private float dimLightness;
    /// <summary>�������</summary>
    [SerializeField] private float maxLightness;
    void Start()
    {
        light2D = GetComponent<Light2D>();
        light2D.intensity = dimLightness;
        StartCoroutine(rainyNight());
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// <para>ʵ����ҹ������ɵ����ȱ仯��ÿ��5��10�봥�����磬�������ȷ����仯</para>
    /// </summary>
    /// <returns></returns>
    IEnumerator rainyNight()
    {
        while (true)
        {
            float interval = Random.Range(5, 10);
            yield return new WaitForSeconds(interval);
            StartCoroutine(thuder());
        }
    }
    /// <summary>
    /// ��ʼ������˸��ÿ��������˸1��3��
    /// </summary>
    /// <returns></returns>
    IEnumerator thuder()
    {
        int flashCount = Random.Range(1, 3);
        for (int i = 0; i < flashCount; i++)
        {
            light2D.intensity = maxLightness;
            yield return new WaitForSeconds(flashTime);
            light2D.intensity = dimLightness;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
