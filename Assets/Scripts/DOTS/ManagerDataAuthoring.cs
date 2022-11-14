using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ManagerDataAuthoring : MonoBehaviour
{
    public GameObject pointPrefab;
    public int spawnCount;
}

public class ManagerDataBaker : Baker<ManagerDataAuthoring>
{
    public override void Bake(ManagerDataAuthoring authoring)
    {
        AddComponent(new ManagerData
        {
            pointPrefab = GetEntity(authoring.pointPrefab),
            spawnCount = authoring.spawnCount
        });
    }
}
