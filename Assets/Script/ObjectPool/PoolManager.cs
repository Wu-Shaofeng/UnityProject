using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����ع�����
/// </summary>
public class PoolManager : MonoBehaviour
{
    /// <summary>���������</summary>
    public Pool[] ProjectilePools;
    /// <summary>������ֵ�</summary>
    static Dictionary<GameObject, Pool> dictionary;

    void Start()
    {
        dictionary = new Dictionary<GameObject, Pool>();
        Initialize(ProjectilePools);
    }
    /// <summary>
    /// ��ʼ�������
    /// </summary>
    /// <param name="pools"></param>
    void Initialize(Pool[] pools)
    {
        foreach(var pool in pools)
        {
#if UNITY_EDITOR
            if(dictionary.ContainsKey(pool.Prefab))
            {
                Debug.Log("Same prefab has existed in pool! Prefab:" + pool.Prefab.name);
                continue;
            }
#endif
            dictionary.Add(pool.Prefab, pool);
            Transform poolParent = new GameObject("Pool: " + pool.Prefab.name).transform;
            poolParent.parent = transform;
            pool.Initialize(poolParent);
        }
    }
    /// <summary>
    /// �Ӷ�������ͷŶ�Ӧ�Ķ���
    /// </summary>
    /// <param name="prefab">���ͷŶ���</param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab)
    {
        if(dictionary.ContainsKey(prefab))
            return dictionary[prefab].PreparedObject();
        else
        {
#if UNITY_EDITOR
            Debug.LogError("Pool Manager could not find prefab: " + prefab.name);
#endif
            return null;
        }
    }
    /// <summary>
    /// �ڶ�Ӧλ�ôӶ�������ͷŶ�Ӧ�Ķ���
    /// </summary>
    /// <param name="prefab">���ͷŶ���</param>
    /// <param name="position">�ͷ�λ��</param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position)
    {
        if (dictionary.ContainsKey(prefab))
            return dictionary[prefab].PreparedObject(position);
        else
        {
#if UNITY_EDITOR
            Debug.LogError("Pool Manager could not find prefab: " + prefab.name);
#endif
            return null;
        }
    }
    /// <summary>
    /// �ڶ�Ӧλ��,��Ӧ�ǶȴӶ�������ͷŶ�Ӧ�Ķ���
    /// </summary>
    /// <param name="prefab">���ͷŶ���</param>
    /// <param name="position">�ͷ�λ��</param>
    /// <param name="rotation">�ͷŽǶ�</param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (dictionary.ContainsKey(prefab))
            return dictionary[prefab].PreparedObject(position, rotation);
        else
        {
#if UNITY_EDITOR
            Debug.LogError("Pool Manager could not find prefab: " + prefab.name);
#endif
            return null;
        }
    }
    /// <summary>
    /// �ڶ�Ӧλ��,��Ӧ�Ƕ�,��Ӧ���ŴӶ�������ͷŶ�Ӧ�Ķ���
    /// </summary>
    /// <param name="prefab">���ͷŶ���</param>
    /// <param name="position">�ͷ�λ��</param>
    /// <param name="rotation">�ͷŽǶ�</param>
    /// <param name="localScale">�ͷ�����</param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        if (dictionary.ContainsKey(prefab))
            return dictionary[prefab].PreparedObject(position, rotation, localScale);
        else
        {
#if UNITY_EDITOR
            Debug.LogError("Pool Manager could not find prefab: " + prefab.name);
#endif
            return null;
        }
    }
}
