using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeightedItem<T>
{
    [SerializeField] T item;
    [SerializeField] int weight = 1;
    [SerializeField] int cost = 1;
}
