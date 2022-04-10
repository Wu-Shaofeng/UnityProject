using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对象池管理器
/// </summary>
public class PoolManager : MonoBehaviour
{
    /// <summary>对象池数组</summary>
    public Pool[] ProjectilePools;
    /// <summary>对象池字典</summary>
    static Dictionary<GameObject, Pool> dictionary;

    void Start()
    {
        dictionary = new Dictionary<GameObject, Pool>();
        Initialize(ProjectilePools);
    }
    /// <summary>
    /// 初始化对象池
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
    /// 从对象池中释放对应的对象
    /// </summary>
    /// <param name="prefab">待释放对象</param>
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
    /// 在对应位置从对象池中释放对应的对象
    /// </summary>
    /// <param name="prefab">待释放对象</param>
    /// <param name="position">释放位置</param>
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
    /// 在对应位置,对应角度从对象池中释放对应的对象
    /// </summary>
    /// <param name="prefab">待释放对象</param>
    /// <param name="position">释放位置</param>
    /// <param name="rotation">释放角度</param>
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
    /// 在对应位置,对应角度,对应缩放从对象池中释放对应的对象
    /// </summary>
    /// <param name="prefab">待释放对象</param>
    /// <param name="position">释放位置</param>
    /// <param name="rotation">释放角度</param>
    /// <param name="localScale">释放缩放</param>
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
