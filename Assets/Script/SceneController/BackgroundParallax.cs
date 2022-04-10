using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 背景动态偏移类
/// </summary>
public class BackgroundParallax : MonoBehaviour
{
    /// <summary>各个背景的变换</summary>
    [SerializeField]private Transform[] backgrounds;
    /// <summary>背景偏移比</summary>
    [SerializeField]private float parallaxScale;
    /// <summary>背景偏移衰减因子</summary>
    [SerializeField]private float parallaxReductionFactor;
    /// <summary>偏移幅度</summary>
    [SerializeField]private float smooth;
    /// <summary>相机的变换</summary>
    [SerializeField]private Transform cam;
    /// <summary>相机上一帧所在的位置</summary>
    private Vector3 previousCamPos;

    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position;    
    }

    // Update is called once per frame
    void Update()
    {
        float parallax_x = (previousCamPos.x - cam.position.x) * parallaxScale;// 根据每一帧相机的位移计算x,y方向上的偏移量
        float parallax_y = (previousCamPos.y - cam.position.y) * parallaxScale;
        // 每一层的背景的偏移量不同,偏移量由远到近不断衰减,离相机最远的偏移最大,最近的偏移最小
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
