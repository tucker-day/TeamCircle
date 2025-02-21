using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeightedItem<T>
{
    [SerializeField] public T item;
    [SerializeField] public int weight = 1;
    [SerializeField] public int cost = 1;
}
