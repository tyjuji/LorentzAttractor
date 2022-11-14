using Unity.Entities;
using UnityEngine;

public class LorentzDataAuthoring : MonoBehaviour
{
    public float a;
    public float b;
    public float c;
    public float t;
}

public class LorentzDataBaker : Baker<LorentzDataAuthoring>
{
    public override void Bake(LorentzDataAuthoring authoring)
    {
        AddComponent(new LorentzData
        {
            a = authoring.a,
            b = authoring.b,
            c = authoring.c,
            t = authoring.t
        });
    }
}