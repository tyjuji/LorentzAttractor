using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private Manager _manager = null;
    private TrailRenderer _trailRenderer = null;
    private float a;
    private float b;
    private float c;
    private float t;

    // Start is called before the first frame update
    void Start()
    {
        _manager = FindObjectOfType<Manager>();
        _trailRenderer = GetComponent<TrailRenderer>();
        SetABC();
        _trailRenderer.startColor = Random.ColorHSV(0f, 1f, 0f, 1f, 0.5f, 0.9f, 1f, 1f);
        _trailRenderer.endColor = Random.ColorHSV(0f, 1f, 0f, 1f, 0.5f, 0.9f, 1f, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //SetABC();
        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;
        float z = gameObject.transform.position.z;
        float xt = x + t * a * (y - x);
        float yt = y + t * (x * (b - z) - y);
        float zt = z + t * (x * y - c * z);

        Vector3 newPosition = new Vector3(xt, yt, zt);

        gameObject.transform.position = newPosition;
    }

    void SetABC()
    {
        a = _manager.a;
        b = _manager.b;
        c = _manager.c;
        t = _manager.t;
    }
}
