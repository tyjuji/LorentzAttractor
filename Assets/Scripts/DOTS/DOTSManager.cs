using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditorInternal;
using UnityEngine;

public partial class DOTSManager : SystemBase
{
    bool created = false;

    protected override void OnCreate()
    {

    }

    protected override void OnUpdate()
    {
        if (!created)
        {
            var manData = GetSingleton<ManagerData>();
            var ecb =
                SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem
                .Singleton>().CreateCommandBuffer(World.Unmanaged);

            Unity.Mathematics.Random random = new Unity.Mathematics.Random();

            var min = new float3(0, 0, 0);
            var max = new float3(10, 10, 10);

            for (int i = 0; i < manData.spawnCount; i++)
            {
                Entity ent = ecb.Instantiate(manData.pointPrefab);
                var trans = GetComponent<LocalToWorldTransform>(ent);
                trans.Value.Position = random.NextFloat3(min, max);
            }

            created = true;
        }
    }
}