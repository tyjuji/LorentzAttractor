using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct LorentzData : IComponentData
{
    public float a;
    public float b;
    public float c;
    public float t;
}
