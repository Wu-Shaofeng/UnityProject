using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 主菜单背景滚动类(粗暴)
/// 由两张图片交替移动实现
/// </summary>
public class BackgroundScroll : MonoBehaviour
{
    /// <summary>左侧图片</summary>
    [SerializeField] GameObject front;
    /// <summary>右侧图片</summary>
    [SerializeField] GameObject back;
    /// <summary>滚动速度</summary>
    [SerializeField] float moveSpeed;
    /// <summary>用于swap的中间值</summary>
    GameObject temp;
    /// <summary>左侧图片的起始位置</summary>
    Vector3 leftPosition;
    /// <summary>右侧图片的起始位置</summary>
    Vector3 rightPosition;

    void Start()
    {
        leftPosition = front.transform.position;// 记录起始点
        rightPosition = back.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 左右图片匀速左移
        front.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        back.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        float frontX = front.transform.position.x;
        float backX = back.transform.position.x;
        if (frontX <= leftPosition.x && backX <= leftPosition.x)// 若右侧图片到达左侧图片的起始位置，左右图片交换
        {
            if (backX < frontX)
            {
                front.transform.position = leftPosition;
                back.transform.position = rightPosition;
            }
            else
            {
                front.transform.position = rightPosition;
                back.transform.position = leftPosition;
            }
        }
    }
}
