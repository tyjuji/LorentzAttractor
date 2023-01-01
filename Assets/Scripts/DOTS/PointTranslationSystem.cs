using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Burst;

[BurstCompile]
[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial struct PointTranslationSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<LorentzData>();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var man = SystemAPI.GetSingleton<LorentzData>();

        // Create the job.
        var job = new PointJobEntity
        {
            Lorentz = man
        };

        // Schedule the job.
        // Source generation creates and passes the query implicitly.
        state.Dependency = job.ScheduleParallel(state.Dependency);
    }
}

[WithAll(typeof(PointComponent))]
[BurstCompile]
public partial struct PointJobEntity : IJobEntity
{
    // Thanks to source generation, an IJobEntity gets the type handles
    // it needs automatically, so we do not include them manually.
    public LorentzData Lorentz;

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

        var nextpos = new float3
        {
            x = x + t * a * (y - x),
            y = y + t * (x * (b - z) - y),
            z = z + t * (x * y - c * z)
        };

        trans.Position = nextpos;
    }
}
