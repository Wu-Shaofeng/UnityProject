using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// UI事件触发器类
/// 实现UI组件的音效播放事件
/// </summary>
public class UIEventTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDragHandler
{
    /// <summary>进入音效</summary>
    [SerializeField] AudioClip enterSFX;
    /// <summary>离开音效</summary>
    [SerializeField] AudioClip exitSFX;
    /// <summary>点击音效</summary>
    [SerializeField] AudioClip pressSFX;
    /// <summary>拖拽音效</summary>
    [SerializeField] AudioClip dragSFX;
    /// <summary>
    /// 当鼠标进入组件时触发的事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(enterSFX)
            AudioManager.instance.PlaySFX(enterSFX);
    }
    /// <summary>
    /// 当鼠标离开组件时触发的事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if(exitSFX)
            AudioManager.instance.PlaySFX(exitSFX);
    }
    /// <summary>
    /// 当鼠标按下组件时触发的事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if(pressSFX)
            AudioManager.instance.PlaySFX(pressSFX);
    }
    /// <summary>
    /// 当鼠标拖拽组件时触发的事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (dragSFX)
            AudioManager.instance.PlaySFX(dragSFX);
    }
}
