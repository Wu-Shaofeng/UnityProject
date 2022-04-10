using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 提示UI类
/// 新手指引
/// </summary>
public class EnterDialog : MonoBehaviour
{
    /// <summary>用于显示提示的画布对象</summary>
    [SerializeField] GameObject dialogPannel;
    /// <summary>提示文本组件</summary>
    [SerializeField] Text dialogText;
    /// <summary>提示内容</summary>
    [SerializeField] string str;

    // 进入碰撞体内显示提示
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<CharacterController>(out CharacterController character))
        {
            dialogText.text = str;
            dialogPannel.SetActive(true);
        }
    }
    // 离开碰撞体后提示消失
    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogPannel.SetActive(false);
    }
}
