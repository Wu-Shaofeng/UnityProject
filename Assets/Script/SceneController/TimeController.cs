using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 时间控制器(单例),用于实现子弹时间
/// </summary>
public class TimeController : Singleton<TimeController>
{
    /// <summary>子弹时间流逝速度</summary>
    float bulletTimeScale = 0.1f;
    /// <summary>默认固定帧时间</summary>
    float defaultFixedDeltaTime;
    /// <summary>子弹时间持续时间</summary>
    float duration = 1.0f;
    /// <summary>判断是否启动暂停菜单</summary>
    public bool isPause = false;

    float currentBulletTimeScale;

    protected override void Awake()
    {
        base.Awake();
        defaultFixedDeltaTime = Time.fixedDeltaTime;
    }
    /// <summary>
    /// 唤出暂停菜单,记录当前子弹时间流速
    /// </summary>
    public void Pause()
    {
        isPause = true;
        currentBulletTimeScale = Time.timeScale;
        Time.timeScale = 0f;
    }
    /// <summary>
    /// 解除暂停菜单,返还子弹时间流速
    /// </summary>
    public void Resume()
    {
        Time.timeScale = currentBulletTimeScale;
        isPause = false;
    }

    /// <summary>
    /// 启动子弹时间
    /// </summary>
    public void enableBulletTime()
    {
        Time.timeScale = bulletTimeScale;
        StartCoroutine(slowOutCoroutine(duration));
    }
    /// <summary>
    /// <para>实现子弹时间的协程</para>
    /// <para>平滑恢复正常时间流逝速度,退出子弹时间</para>
    /// </summary>
    /// <param name="duration">退出持续时间</param>
    /// <returns></returns>
    IEnumerator slowOutCoroutine(float duration)
    {
        float t = 0f;
        while(t<1f)
        {
            if (!isPause)// 当暂停菜单唤出时,停止执行
            {
                t += Time.deltaTime / duration;
                Time.timeScale = Mathf.Lerp(bulletTimeScale, 1, t);
                Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;         
            }
            yield return null;
        }
    }
}
