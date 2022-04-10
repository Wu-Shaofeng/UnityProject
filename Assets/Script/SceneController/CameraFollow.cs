using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 镜头跟随类
/// </summary>
public class CameraFollow : MonoBehaviour
{
    /// <summary>镜头跟随对象</summary>
    [SerializeField] private Transform target;
    /// <summary>镜头移动平滑值</summary>
    [SerializeField] private float smooth;
    /// <summary>镜头的y轴偏移</summary>
    [SerializeField] private float yoffset;

    /// <summary>当人物移动至场景边缘时，镜头不再水平移动</summary>
    bool keepXStatic;
    /// <summary>当人物跌落至场景外时，镜头不再竖直移动</summary>
    bool keepYStatic;
    /// <summary>是否通知人物控制类,人物已调出场景</summary>
    bool hasInformed;

    void Start()
    {

    }

    void Update()
    {
        if (target != null)
        {
            if (target.position.x <= -53.35 || target.position.x >= 139)
                keepXStatic = true;
            else
                keepXStatic = false;
            if (target.position.y <= -10)
                keepYStatic = true;
            else
                keepYStatic = false;
            if (target.position.y <= -15 && !hasInformed)// 让人物完全掉出场景外
                DropOutOfTheScene();
        }
    }

    void LateUpdate()
    {
        if(target!=null)// 当目标不为空时，镜头跟随对象移动
        {
            Vector3 position;
            if (keepXStatic)
                position = new Vector3(transform.position.x, target.position.y + yoffset, transform.position.z);
            else
                position = new Vector3(target.position.x, target.position.y + yoffset, transform.position.z);
            if (keepYStatic)
                position.y = transform.position.y;
            if (transform.position != position) 
                transform.position = Vector3.Lerp(transform.position, position, smooth);
        }
    }
    /// <summary>
    /// 掉出场景外，角色死亡
    /// </summary>
    void DropOutOfTheScene()
    {
        hasInformed = true;
        StartCoroutine(SustainedInjury());
    }
    /// <summary>
    /// 掉出场景后,人物持续扣血
    /// </summary>
    /// <returns></returns>
    IEnumerator SustainedInjury()
    {
        int count = 10;// 获取玩家的血量的开销不如直接扣100生命值的开销小
        while (count > 0)
        {
            target.gameObject.GetComponent<CharacterController>().getDamage(10);
            yield return new WaitForSeconds(0.2f);
            count--;
        }
    }
}
