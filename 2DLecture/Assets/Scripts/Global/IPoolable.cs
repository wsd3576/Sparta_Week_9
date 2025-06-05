using System;
using UnityEngine;

public interface IPoolable
{
    void Initialize(Action<GameObject> reaturnAction);

    void OnSpawn();

    void OnDespawn();
}

public interface IIndexable
{
    public int ObjectIndex { get; }
}