using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Burst;

public partial struct PointTranslationSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<LorentzData>();
    }

    public void OnDestroy(ref SystemState state)
    {
    }

    public void OnUpdate(ref SystemState state)
    {
        //var man = SystemAPI.GetSingleton<LorentzData>();
        //var a = man.a;
        //var b = man.b;
        //var c = man.c;
        //var t = man.t;

        ////Debug.Log(a);

        //foreach (var transform in SystemAPI.Query<TransformAspect>().WithAll<PointComponent>())
        //{
        //    var x = transform.Position.x;
        //    var y = transform.Position.y;
        //    var z = transform.Position.z;

        //    //Debug.Log(x);

        //    var nextpos = new float3
        //    {
        //        x = x + t * a * (y - x),
        //        y = y + t * (x * (b - z) - y),
        //        z = z + t * (x * y - c * z)
        //    };

        //    transform.Position = nextpos;
        //    //Debug.Log(nextpos);
        //}

        // Get an EntityCommandBuffer from the BeginSimulationEntityCommandBufferSystem.
        var ecbSingleton = SystemAPI.GetSingleton<
                BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        // Create the job.
        var job = new PointJobEntity
        {
            Ecb = ecb.AsParallelWriter()
        };

        // Schedule the job. Source generation creates and passes the query implicitly.
        state.Dependency = job.Schedule(state.Dependency);
    }
}


// Only entities having the Apple component type will match the implicit query
// even though we do not access the Apple component values.
[WithAll(typeof(PointComponent))]
// Only entities NOT having the Banana component type will match the implicit query.
//[WithNone(typeof(Banana))]
[BurstCompile]
public partial struct PointJobEntity : IJobEntity
{
    // Thanks to source generation, an IJobEntity gets the type handles
    // it needs automatically, so we do not include them manually.

    // EntityCommandBuffers and other fields still must
    // be included manually.
    public EntityCommandBuffer.ParallelWriter Ecb;
    public LorentzData Lorentz;

    // Source generation will create an EntityQuery based on the
    // parameters of Execute(). In this case, the generated query will
    // match all entities having a Foo and Bar component.
    //   - When this job runs, Execute() will be called once
    //     for each entity matching the query.
    //   - Any entity with a disabled Foo or Bar will be skipped.
    //   - 'ref' param components are read-write
    //   - 'in' param components are read-only
    //   - We need to pass the chunk index as a sortKey to methods of
    //     the EntityCommandBuffer.ParallelWriter, so we include an
    //     int parameter with the [ChunkIndexInQuery] attribute.
    [BurstCompile]
    public void Execute(ref TransformAspect trans)
    {
        var a = Lorentz.a;
        var b = Lorentz.b;
        var c = Lorentz.c;
        var t = Lorentz.t;
        var x = trans.Position.x;
        var y = trans.Position.y;
        var z = trans.Position.z;

        //Debug.Log(x);

        var nextpos = new float3
        {
            x = x + t * a * (y - x),
            y = y + t * (x * (b - z) - y),
            z = z + t * (x * y - c * z)
        };

        trans.Position = nextpos;
    }
}