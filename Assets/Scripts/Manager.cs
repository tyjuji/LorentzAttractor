using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    public float a = 10f;
    [SerializeField]
    public float b = 28.0f;
    [SerializeField]
    public float c = 8.0f;
    [SerializeField]
    public float t = 0.02f;
    [SerializeField]
    public int count = 100;
    [SerializeField]
    GameObject _prefab = null;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            Instantiate(_prefab, position, Quaternion.identity);
        }
    }
}
