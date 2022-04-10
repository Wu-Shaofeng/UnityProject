using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ״̬����
/// </summary>
public class StatusBar : MonoBehaviour
{
    /// <summary>�ײ����ͼƬ</summary>
    public Image fillImageBack;
    /// <summary>�������ͼƬ</summary>
    public Image fillImageFront;
    /// <summary>����ٶ�</summary>
    public float fillSpeed = 0.1f;
    /// <summary>�����ӳ�</summary>
    public float fillDelay = 0.5f;
    /// <summary>�Ƿ����ӳ�״̬</summary>
    public bool delayFill = true;
    /// <summary>��ǰ���ֵ</summary>
    protected float currentFillAmount;
    /// <summary>Ŀ�����ֵ</summary>
    protected float targetFillAmount;
    float t;
    /// <summary>�洢Э�̵ı���</summary>
    protected Coroutine coroutine;

    void Awake()
    {
        if (TryGetComponent<Canvas>(out Canvas canvas))
            canvas.worldCamera = Camera.main;
    }
    /// <summary>
    /// ״̬����ʼ��
    /// </summary>
    /// <param name="currentValue">��ǰֵ</param>
    /// <param name="maxValue">���ֵ</param>
    public void Initialize(float currentValue, float maxValue)
    {
        currentFillAmount = currentValue / maxValue;
        targetFillAmount = currentFillAmount;
        fillImageBack.fillAmount = currentFillAmount;
        fillImageFront.fillAmount = currentFillAmount;
    }
    /// <summary>
    /// ����״̬��
    /// </summary>
    /// <param name="currentValue">��ǰֵ</param>
    /// <param name="maxValue">���ֵ</param>
    public void UpdateStatus(float currentValue, float maxValue)
    {
        targetFillAmount = currentValue / maxValue;
        if (coroutine != null)// ��֮ǰ��Э��δ����,����ֹ��Э��
            StopCoroutine(coroutine);
        if(currentFillAmount>targetFillAmount)// �۳�
        {
            fillImageFront.fillAmount = targetFillAmount;
            coroutine = StartCoroutine(BufferedFilling(fillImageBack));
        }
        if(currentFillAmount<targetFillAmount)// ���
        {
            fillImageBack.fillAmount = targetFillAmount;
            coroutine = StartCoroutine(BufferedFilling(fillImageFront));
        }
    }
    /// <summary>
    /// ���������/�۳���Э��
    /// </summary>
    /// <param name="image">Ŀ��ͼƬ</param>
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
