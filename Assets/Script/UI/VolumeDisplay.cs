using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 音量显示类
/// 实现音量滑动条旁的音量值文本显示
/// </summary>
public class VolumeDisplay : MonoBehaviour
{
    /// <summary>音量滑动条</summary>
    private Slider volumeSlider;
    /// <summary>音量显示文本</summary>
    [SerializeField] Text volumeText;

    // Start is called before the first frame update
    void Start()
    {
        // 获取音量滑动条
        volumeSlider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        volumeText.text = volumeSlider.value.ToString() + "%";// 更新文本
    }
}
