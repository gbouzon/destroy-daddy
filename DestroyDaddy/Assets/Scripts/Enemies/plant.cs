using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant : MonoBehaviour
{
    private Transform target;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //initating target as the main character
        GameObject player = GameObject.FindWithTag("Player");
        target = player.GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 a = transform.position;
        Vector3 b = target.position;
        //if player comes near plant it gets activated and starts attacking
        if (Vector3.Distance(a, b) < 10)
        {
            animator.enabled = true;
            transform.position = Vector3.Lerp(a, b, 0.001f);
        }
        else
        {
            animator.enabled = false;
        }
        //attack
        if (Vector3.Distance(a, b) < 2)
        {
            animator.Play("biteAnimation");
        }
    }

}
