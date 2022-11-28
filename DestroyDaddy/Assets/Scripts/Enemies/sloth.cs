using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sloth : MonoBehaviour
{
    private Transform target;
    private Animator animator;
    private float t;
    private int maxHealth = 2;
    private int currentHealth;
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        target = player.GetComponent<Transform>();
        t = Random.Range(0.001f, 0.01f);
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        Vector3 a = transform.position;
        Vector3 b = target.position;
        if (Vector3.Distance(a, b) < 2)
        {
            animator.Play("biteAnimation");
        }
        else
        {
            transform.position = Vector3.Lerp(a, b, 0.001f);
            transform.LookAt(target);
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
