using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public GameObject[] prefabs;

    private Dictionary<int, Queue<GameObject>> pools = new();

    public static ObjectPoolManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < prefabs.Length; i++)
        {
            Debug.Log($"{prefabs[i].name} 발견.");
            if (prefabs[i].TryGetComponent(out IIndexable indexable))
            {
                Debug.Log($"아이템 번호 {indexable.ObjectIndex} 확인");
                pools[indexable.ObjectIndex] = new Queue<GameObject>();
                continue;
            }
            else
            {
                Debug.Log($"아이템 번호 확인 불가");
            }
        }
    }

    public GameObject GetObject(int ObjectIndex, Vector3 position, Quaternion rotation)
    {
        if (!pools.ContainsKey(ObjectIndex))
        {
            return null;
        }

        GameObject obj;
        if (pools[ObjectIndex].Count > 0)
        {
            obj = pools[ObjectIndex].Dequeue();
        }
        else
        {
            GameObject matchedPrefab = null;
            
            foreach (GameObject prefab in prefabs)
            {
                if (prefab.TryGetComponent(out IIndexable indexable))
                {
                    if (indexable.ObjectIndex == ObjectIndex)
                    {
                        matchedPrefab = prefab;
                        break;
                    }
                }
                else
                {
                    return null;
                }
            }
            
            obj = Instantiate(matchedPrefab);
            obj.GetComponent<IPoolable>()?.Initialize(o => ReturnObject(ObjectIndex, o));
        }
        
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);
        obj.GetComponent<IPoolable>()?.OnSpawn();
        
        return obj;
    }

    public void ReturnObject(int prefabIndex, GameObject obj)
    {
        if (!pools.ContainsKey(prefabIndex))
        {
            Destroy(obj);
        }
        
        obj.SetActive(false);
        pools[prefabIndex].Enqueue(obj);
    }
}