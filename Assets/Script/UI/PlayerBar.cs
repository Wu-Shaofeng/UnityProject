using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 玩家血条类
/// 状态栏类的继承类
/// 新增残血时血条闪烁的功能
/// </summary>
public class PlayerBar : StatusBar
{
    [SerializeField] Text bloodText;
    public bool lowAmount;
    void setbloodText(float currentValue)
    {
        bloodText.text = Mathf.RoundToInt(currentValue).ToString();
    }

    public new void Initialize(float currentValue, float maxValue)
    {
        base.Initialize(currentValue, maxValue);
        setbloodText(currentValue);
        StartCoroutine(flashLight());
    }

    public new void UpdateStatus(float currentValue, float maxValue)
    {
        targetFillAmount = currentValue / maxValue;
        lowAmount = targetFillAmount < 0.25 ? true : false;
        if (coroutine != null)
            StopCoroutine(coroutine);
        if (currentFillAmount > targetFillAmount)
        {
            fillImageFront.fillAmount = targetFillAmount;
            coroutine = StartCoroutine(BufferedFilling(fillImageBack, currentValue));
        }
        if (currentFillAmount < targetFillAmount)
        {
            fillImageBack.fillAmount = targetFillAmount;
            coroutine = StartCoroutine(BufferedFilling(fillImageFront,currentValue));
        }
    }

    protected IEnumerator BufferedFilling(Image image, float currentValue)
    {
        setbloodText(currentValue);
        return base.BufferedFilling(image);
    }
    /// <summary>
    /// 当血量低于25%时,血条开始闪烁
    /// </summary>
    /// <returns></returns>
    IEnumerator flashLight()
    {
        Color originColor = fillImageFront.color;
        while(true)
        {
            if (lowAmount)
                fillImageFront.color = (fillImageFront.color == originColor) ? Color.white : originColor;
            else
                fillImageFront.color = originColor;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
