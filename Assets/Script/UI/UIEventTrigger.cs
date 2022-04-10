using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// UI�¼���������
/// ʵ��UI�������Ч�����¼�
/// </summary>
public class UIEventTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDragHandler
{
    /// <summary>������Ч</summary>
    [SerializeField] AudioClip enterSFX;
    /// <summary>�뿪��Ч</summary>
    [SerializeField] AudioClip exitSFX;
    /// <summary>�����Ч</summary>
    [SerializeField] AudioClip pressSFX;
    /// <summary>��ק��Ч</summary>
    [SerializeField] AudioClip dragSFX;
    /// <summary>
    /// �����������ʱ�������¼�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(enterSFX)
            AudioManager.instance.PlaySFX(enterSFX);
    }
    /// <summary>
    /// ������뿪���ʱ�������¼�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if(exitSFX)
            AudioManager.instance.PlaySFX(exitSFX);
    }
    /// <summary>
    /// ����갴�����ʱ�������¼�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if(pressSFX)
            AudioManager.instance.PlaySFX(pressSFX);
    }
    /// <summary>
    /// �������ק���ʱ�������¼�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (dragSFX)
            AudioManager.instance.PlaySFX(dragSFX);
    }
}
