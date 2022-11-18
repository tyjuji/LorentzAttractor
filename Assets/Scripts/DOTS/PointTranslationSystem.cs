using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial struct PointTranslationSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
    }

    public void OnDestroy(ref SystemState state)
    {
    }

    public void OnUpdate(ref SystemState state)
    {
        var man = SystemAPI.GetSingleton<LorentzData>();
        var a = man.a;
        var b = man.b;
        var c = man.c;
        var t = man.t;

        //Debug.Log(a);

        foreach (var transform in SystemAPI.Query<TransformAspect>().WithAll<PointComponent>())
        {
            var x = transform.Position.x;
            var y = transform.Position.y;
            var z = transform.Position.z;

            //Debug.Log(x);

            var nextpos = new float3
            {
                x = x + t * a * (y - x),
                y = y + t * (x * (b - z) - y),
                z = z + t * (x * y - c * z)
            };

            transform.Position = nextpos;
            //Debug.Log(nextpos);
        }
    }
}
