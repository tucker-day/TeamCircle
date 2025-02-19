using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public sealed class DroppableRegistry //make singleton
{
    private static readonly DroppableRegistry instance = new DroppableRegistry();

    static DroppableRegistry()
    {

    }

    private DroppableRegistry()
    {

    }

    public static DroppableRegistry Instance
    {
        get
        {
            return instance;
        }
    }

    //create register function

    //create deregister function

    //weapons, accessories, gold list
}
