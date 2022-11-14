using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PointAuthoring : MonoBehaviour
{
}

public class PointBaker : Baker<PointAuthoring>
{
    public override void Bake(PointAuthoring authoring)
    {
        AddComponent<PointComponent>();
    }
}