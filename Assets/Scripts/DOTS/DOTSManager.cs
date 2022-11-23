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
    //EntityQuery inscene;

    uint updatecounter;

    bool spawned = false;

    protected override void OnCreate()
    {
        RequireForUpdate<ManagerData>();


        //var queryBuilder = new EntityQueryBuilder(Allocator.Temp);
        //queryBuilder.WithAll<PointComponent>();
        //inscene = GetEntityQuery(queryBuilder);


        //var prefab = SystemAPI.GetSingleton<ManagerData>().pointPrefab;

        //// Instantiating an entity creates copy entities with the same component types and values.
        //var instances = EntityManager.Instantiate(prefab, 500, Allocator.Temp);

        //// Unlike new Random(), CreateFromIndex() hashes the random seed
        //// so that similar seeds don't produce similar results.
        //var random = Unity.Mathematics.Random.CreateFromIndex(updatecounter++);

        //foreach (var entity in instances)
        //{
        //    var position = (random.NextFloat3() - new float3(0.5f, 0, 0.5f)) * 20;

        //    // Get a TransformAspect instance wrapping the entity.
        //    var transform = SystemAPI.GetAspectRW<TransformAspect>(entity);
        //    transform.LocalPosition = position;
        //}
    }

    protected override void OnStartRunning()
    {
        var manData = GetSingleton<ManagerData>();
        //var ecb = new EntityCommandBuffer(Allocator.Temp);

        //Unity.Mathematics.Random random = new Unity.Mathematics.Random((uint)DateTime.Now.Ticks);

        //var min = new float3(0, 0, 0);
        //var max = new float3(10, 10, 10);

        //Debug.Log(manData.spawnCount);

        //var asd = EntityManager.Instantiate(manData.pointPrefab, 200, Allocator.Temp);

        //foreach (var item in asd)
        //{
        //    var trans = new UniformScaleTransform { Position = random.NextFloat3(min, max) };
        //    var l2wtrans = new LocalToWorldTransform { Value = trans };
        //    SetComponent<LocalToWorldTransform>(item, l2wtrans);
        //}

        //for (int i = 0; i < manData.spawnCount; i++)
        //{
        //    Entity ent = ecb.Instantiate(manData.pointPrefab);
        //    var trans = new UniformScaleTransform { Position = random.NextFloat3(min, max) };
        //    var l2wtrans = new LocalToWorldTransform { Value = trans };
        //    ecb.SetComponent<LocalToWorldTransform>(ent, l2wtrans);
        //}

        //ecb.Playback(EntityManager.World.EntityManager);
    }

    protected override void OnUpdate()
    {
        var manData = GetSingleton<ManagerData>();
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        //Unity.Mathematics.Random random = new Unity.Mathematics.Random((uint)DateTime.Now.Ticks);

        var min = new float3(0, 0, 0);
        var max = new float3(10, 10, 10);

        if (!spawned) 
        {
            spawned = true;
            var prefab = SystemAPI.GetSingleton<ManagerData>().pointPrefab;

            // Instantiating an entity creates copy entities with the same component types and values.
            var instances = EntityManager.Instantiate(prefab, manData.spawnCount, Allocator.Temp);

            // Unlike new Random(), CreateFromIndex() hashes the random seed
            // so that similar seeds don't produce similar results.
            var random = Unity.Mathematics.Random.CreateFromIndex(updatecounter++);

            foreach (var entity in instances)
            {
                var position = (random.NextFloat3() - new float3(0.5f, 0, 0.5f)) * 20;

                // Get a TransformAspect instance wrapping the entity.
                var transform = SystemAPI.GetAspectRW<TransformAspect>(entity);
                transform.LocalPosition = position;


                // Set random color
                EntityManager.AddComponentData(entity, new URPMaterialPropertyBaseColor { Value = random.NextFloat4() });
            }

            //for (int i = 0; i < manData.spawnCount; i++)
            //{
            //    //Entity ent = ecb.Instantiate(manData.pointPrefab);
            //    Entity ent = ecb.Instantiate(SystemAPI.GetSingleton<ManagerData>().pointPrefab);
            //    //var trans = new UniformScaleTransform { Position = random.NextFloat3(min, max) };
            //    //var l2wtrans = new LocalToWorldTransform { Value = trans };
            //    //ecb.SetComponent<LocalToWorldTransform>(ent, l2wtrans);
            //}

            //ecb.Playback(EntityManager.WorldUnmanaged.EntityManager);
        }
    }
}
