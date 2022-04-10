using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ʽ��������
/// </summary>
/// <typeparam name="T">������</typeparam>
public class Singleton<T> : MonoBehaviour where T : Component
{
    /// <summary>����ģʽ</summary>
    public static T instance { get; private set; }
    /// <summary>��ȡΨһ���ö���</summary>
    protected virtual void Awake()
    {
        if (instance == null)
            instance = this as T;
        else if (instance != null)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
