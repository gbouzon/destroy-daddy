using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diatryma : MonoBehaviour
{
    private Transform target;
    private Animator animator;
    private int maxHealth = 2;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        target = player.GetComponent<Transform>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
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

    public void HitByRay()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            animator.Play("dieAnimation");
            Destroy(gameObject, 1);
        }
    }
}
