using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]public class Pool
{
    public GameObject Prefab
    {
        get
        {
            return prefab;
        }
    }
    public GameObject prefab;
    public int size = 1;
    Queue<GameObject> queue;
    Transform parent;

    public void Initialize(Transform parent)
    {
        queue = new Queue<GameObject>();
        this.parent = parent;
        for (var i = 0; i < size; i++)
            queue.Enqueue(generate());
    }

    GameObject generate()
    {
        var temp = GameObject.Instantiate(prefab, parent);
        temp.SetActive(false);
        return temp;
    }

    GameObject AvailableObject()
    {
        GameObject availableObject = null;
        if(queue.Count>0 && !queue.Peek().activeSelf)
        {
            availableObject = queue.Dequeue();
        }
        else
        {
            availableObject = generate();
        }
        queue.Enqueue(availableObject);
        return availableObject;
    }

    public GameObject PreparedObject()
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        return preparedObject;
    }

    public GameObject PreparedObject(Vector3 position)
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        return preparedObject;
    }

    public GameObject PreparedObject(Vector3 position, Quaternion rotation)
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        return preparedObject;
    }

    public GameObject PreparedObject(Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        preparedObject.transform.localScale = localScale;
        preparedObject.SetActive(true);
        return preparedObject;
    }
}
