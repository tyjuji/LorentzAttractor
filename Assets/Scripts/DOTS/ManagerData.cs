using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ManagerData : IComponentData
{
    public Entity pointPrefab;
    public int spawnCount;
}
