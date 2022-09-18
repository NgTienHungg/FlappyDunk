using System;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectTag
{
    Hoop
}

[Serializable]
public class Pool
{
    public GameObject prefab;
    public ObjectTag tag;
    public int size;
    public bool expandable;

    [HideInInspector]
    public List<GameObject> objects;
}