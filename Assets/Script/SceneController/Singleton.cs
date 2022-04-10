using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 范式单例基类
/// </summary>
/// <typeparam name="T">类类型</typeparam>
public class Singleton<T> : MonoBehaviour where T : Component
{
    /// <summary>单例模式</summary>
    public static T instance { get; private set; }
    /// <summary>获取唯一可用对象</summary>
    protected virtual void Awake()
    {
        if (instance == null)
            instance = this as T;
        else if (instance != null)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
