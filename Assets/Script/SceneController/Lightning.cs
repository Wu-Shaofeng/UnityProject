using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
/// <summary>
/// 闪电类，实现场景亮度的频闪
/// </summary>
public class Lightning : MonoBehaviour
{
    /// <summary>2D光源</summary>
    private Light2D light2D;
    /// <summary>闪烁时间</summary>
    [SerializeField] private float flashTime;
    /// <summary>昏暗亮度</summary>
    [SerializeField] private float dimLightness;
    /// <summary>最大亮度</summary>
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
    /// <para>实现雨夜闪电造成的亮度变化，每隔5到10秒触发闪电，场景亮度发生变化</para>
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
    /// 开始亮度闪烁，每次闪电闪烁1到3次
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
