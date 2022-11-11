using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sloth : MonoBehaviour
{
    private Transform target;
    private Animator animator;
    private float t;
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        target = player.GetComponent<Transform>();
        t = Random.Range(0.001f, 0.01f);
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        Vector3 a = transform.position;
        Vector3 b = target.position;
        if (Vector3.Distance(a, b) < 5)
        {
            animator.Play("groundsmackAnimation");
        }
        else
        {
            transform.position = Vector3.Lerp(a, b, 0.001f);
            transform.LookAt(target);
        }


    }
}
