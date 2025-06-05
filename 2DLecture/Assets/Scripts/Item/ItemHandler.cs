using System;
using UnityEngine;

public class ItemHandler : MonoBehaviour, IPoolable, IIndexable
{
    [SerializeField] private ItemData itemData;
    [Range(100, 199)] 
    public int itemIndex = 100;
    public int ObjectIndex => itemIndex;
    
    public ItemData ItemData => itemData;

    private Action<GameObject> returnToPool;

    public void Initialize(Action<GameObject> reaturnAction)
    {
        returnToPool = reaturnAction;
    }

    public void OnSpawn()
    {
    }

    public void OnDespawn()
    {
        returnToPool?.Invoke(gameObject);
    }
}