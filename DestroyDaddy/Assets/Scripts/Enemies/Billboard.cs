using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private GameObject cam;
    private Transform camTrans;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        camTrans = cam.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + camTrans.forward);
    }
}
