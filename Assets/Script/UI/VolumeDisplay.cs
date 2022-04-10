using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ������ʾ��
/// ʵ�������������Ե�����ֵ�ı���ʾ
/// </summary>
public class VolumeDisplay : MonoBehaviour
{
    /// <summary>����������</summary>
    private Slider volumeSlider;
    /// <summary>������ʾ�ı�</summary>
    [SerializeField] Text volumeText;

    // Start is called before the first frame update
    void Start()
    {
        // ��ȡ����������
        volumeSlider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        volumeText.text = volumeSlider.value.ToString() + "%";// �����ı�
    }
}
