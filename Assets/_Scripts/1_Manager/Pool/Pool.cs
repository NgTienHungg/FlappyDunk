using System;
using UnityEngine;
using System.Collections.Generic;

public enum ObjectTag
{
    Hoop,
    SmokeEffect
}

[Serializable]
public class Pool
{
    public GameObject prefab;
    public ObjectTag tag;
    public int size;
    public bool expandable;

    //[HideInInspector]
    public List<GameObject> objects;
}