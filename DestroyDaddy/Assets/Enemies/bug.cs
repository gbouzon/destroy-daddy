using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bug : MonoBehaviour
{
    public Transform target;
    private float t;
    void Start()
    {
        t = Random.Range(0.001f, 0.01f);
    }
    void Update()
    {
        Vector3 a = transform.position;
        Vector3 b = target.position;
        transform.position = Vector3.Lerp(a, b, t);
        transform.LookAt(target);
    }
}
