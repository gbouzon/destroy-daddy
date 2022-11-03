using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant : MonoBehaviour
{
    public Transform target;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 a = transform.position;
        Vector3 b = target.position;
        if (Vector3.Distance(a, b) < 5)
        {
            animator.enabled = true;
            transform.position = Vector3.Lerp(a, b, 0.001f);
        }
        else
        {
            animator.enabled = false;
        }
    }
}
