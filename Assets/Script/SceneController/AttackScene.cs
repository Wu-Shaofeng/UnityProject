using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 用于实现战斗场景中镜头晃动和顿帧的类
/// </summary>
public class AttackScene : MonoBehaviour
{
    /// <summary>单例模式</summary>
    private static AttackScene instance;
    /// <summary>获取唯一可用对象</summary>
    public static AttackScene Instance
    {
        get
        {
            if (instance == null)
                instance = Transform.FindObjectOfType<AttackScene>();
            return instance;
        }
    }
    /// <summary>判断当前镜头是否晃动</summary>
    private bool isShake;
    /// <summary>
    /// 顿帧,停顿帧数为duration个
    /// </summary>
    /// <param name="duration">停顿帧数</param>
    public void hitPause(int duration)
    {
        StartCoroutine(Pause(duration));
    }
    /// <summary>
    /// 镜头晃动,晃动时长为duration秒,晃动幅度为strength
    /// </summary>
    /// <param name="duration">晃动时长</param>
    /// <param name="strength">晃动幅度</param>
    public void hitShake(float duration, float strength)
    {
        // 镜头晃动不叠加
        if(!isShake)
            StartCoroutine(Shake(duration, strength));
    }
    /// <summary>
    /// 用于实现顿帧的协程，软顿帧
    /// </summary>
    /// <param name="duration">停顿帧数</param>
    /// <returns></returns>
    IEnumerator Pause(int duration)
    {
        float pauseTime = duration / 60f;
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1;
    }
    /// <summary>
    /// 用于实现晃动的协程,以相机初始点为圆心,strength为半径持续晃动duration秒
    /// </summary>
    /// <param name="duration">晃动时长</param>
    /// <param name="strength">晃动幅度</param>
    /// <returns></returns>
    IEnumerator Shake(float duration, float strength)
    {
        isShake = true;
        Transform camera = Camera.main.transform;
        Vector3 originPosition = camera.position;
        while(duration>0)
        {
            camera.position = Random.insideUnitSphere * strength + originPosition;
            duration -= Time.deltaTime;
            yield return null;
        }
        camera.position = originPosition;
        isShake = false;
    }
}
