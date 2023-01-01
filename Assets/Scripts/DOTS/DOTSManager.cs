using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditorInternal;
//using UnityEngine;
using Unity.Collections;
//using System;
using static UnityEngine.Random;
using System;
using UnityEngine;
using Unity.Rendering;

[CreateAfter(typeof(InitializationSystemGroup))]
public partial class DOTSManager : SystemBase
{
    uint updatecounter;
    bool spawned = false;

    protected override void OnCreate()
    {
        RequireForUpdate<ManagerData>();
    }

    protected override void OnUpdate()
    {
        if (!spawned) 
        {
            // Only run once.
            spawned = true;
            var manData = GetSingleton<ManagerData>();
            var prefab = SystemAPI.GetSingleton<ManagerData>().pointPrefab;

            // Instantiating an entity creates copy entities
            // with the same component types and values.
            var instances = EntityManager.Instantiate(prefab, 
                manData.spawnCount, Allocator.Temp);

            var random = Unity.Mathematics.Random.CreateFromIndex(updatecounter++);

            foreach (var entity in instances)
            {
                var position = (random.NextFloat3() - new float3(0.5f, 0, 0.5f)) * 20;

                // Get a TransformAspect instance wrapping the entity.
                var transform = SystemAPI.GetAspectRW<TransformAspect>(entity);
                transform.LocalPosition = position;

                // Set random color
                EntityManager.AddComponentData(entity, 
                    new URPMaterialPropertyBaseColor { Value = random.NextFloat4() });
            }
        }
    }
}
