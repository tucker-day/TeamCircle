using System;
using UnityEngine;

[Serializable]
public class WeightedItem<T>
{
    [SerializeField] public T item;
    [SerializeField] public int weight = 1;
    [SerializeField] public int cost = 1;
}
