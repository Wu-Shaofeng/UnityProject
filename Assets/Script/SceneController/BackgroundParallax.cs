using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������̬ƫ����
/// </summary>
public class BackgroundParallax : MonoBehaviour
{
    /// <summary>���������ı任</summary>
    [SerializeField]private Transform[] backgrounds;
    /// <summary>����ƫ�Ʊ�</summary>
    [SerializeField]private float parallaxScale;
    /// <summary>����ƫ��˥������</summary>
    [SerializeField]private float parallaxReductionFactor;
    /// <summary>ƫ�Ʒ���</summary>
    [SerializeField]private float smooth;
    /// <summary>����ı任</summary>
    [SerializeField]private Transform cam;
    /// <summary>�����һ֡���ڵ�λ��</summary>
    private Vector3 previousCamPos;

    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position;    
    }

    // Update is called once per frame
    void Update()
    {
        float parallax_x = (previousCamPos.x - cam.position.x) * parallaxScale;// ����ÿһ֡�����λ�Ƽ���x,y�����ϵ�ƫ����
        float parallax_y = (previousCamPos.y - cam.position.y) * parallaxScale;
        // ÿһ��ı�����ƫ������ͬ,ƫ������Զ��������˥��,�������Զ��ƫ�����,�����ƫ����С
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float backgroundTargetPosX = backgrounds[i].position.x + parallax_x * (i * parallaxReductionFactor + 1);
            float backgroundTargetPosY = backgrounds[i].position.y + parallax_y * (i * parallaxReductionFactor + 1);
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smooth);
        }
        previousCamPos = cam.position;
    }
}
