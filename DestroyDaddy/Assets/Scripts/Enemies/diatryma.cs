using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diatryma : MonoBehaviour
{
    private Transform target;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        target = player.GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 a = transform.position;
        Vector3 b = target.position;
        transform.LookAt(target);
        if (Vector3.Distance(a,b) < 5)
        {
            animator.Play("biteAnimation");
        }
        else
        {
            transform.position = Vector3.Lerp(a, b, 0.01f);
        }
    }
}
