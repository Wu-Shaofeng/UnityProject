using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 状态栏类
/// </summary>
public class StatusBar : MonoBehaviour
{
    /// <summary>底层填充图片</summary>
    public Image fillImageBack;
    /// <summary>顶层填充图片</summary>
    public Image fillImageFront;
    /// <summary>填充速度</summary>
    public float fillSpeed = 0.1f;
    /// <summary>背景延迟</summary>
    public float fillDelay = 0.5f;
    /// <summary>是否处于延迟状态</summary>
    public bool delayFill = true;
    /// <summary>当前填充值</summary>
    protected float currentFillAmount;
    /// <summary>目标填充值</summary>
    protected float targetFillAmount;
    float t;
    /// <summary>存储协程的变量</summary>
    protected Coroutine coroutine;

    void Awake()
    {
        if (TryGetComponent<Canvas>(out Canvas canvas))
            canvas.worldCamera = Camera.main;
    }
    /// <summary>
    /// 状态栏初始化
    /// </summary>
    /// <param name="currentValue">当前值</param>
    /// <param name="maxValue">最大值</param>
    public void Initialize(float currentValue, float maxValue)
    {
        currentFillAmount = currentValue / maxValue;
        targetFillAmount = currentFillAmount;
        fillImageBack.fillAmount = currentFillAmount;
        fillImageFront.fillAmount = currentFillAmount;
    }
    /// <summary>
    /// 更新状态栏
    /// </summary>
    /// <param name="currentValue">当前值</param>
    /// <param name="maxValue">最大值</param>
    public void UpdateStatus(float currentValue, float maxValue)
    {
        targetFillAmount = currentValue / maxValue;
        if (coroutine != null)// 若之前的协程未结束,则终止该协程
            StopCoroutine(coroutine);
        if(currentFillAmount>targetFillAmount)// 扣除
        {
            fillImageFront.fillAmount = targetFillAmount;
            coroutine = StartCoroutine(BufferedFilling(fillImageBack));
        }
        if(currentFillAmount<targetFillAmount)// 填充
        {
            fillImageBack.fillAmount = targetFillAmount;
            coroutine = StartCoroutine(BufferedFilling(fillImageFront));
        }
    }
    /// <summary>
    /// 负责缓慢填充/扣除的协程
    /// </summary>
    /// <param name="image">目标图片</param>
    /// <returns></returns>
    protected IEnumerator BufferedFilling(Image image)
    {
        if (delayFill)
            yield return new WaitForSeconds(fillDelay);
        t = 0f;
        while(t<1f)
        {
            t += Time.deltaTime * fillSpeed;
            currentFillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, t);
            image.fillAmount = currentFillAmount;
            yield return null;
        }
        
    }
}
