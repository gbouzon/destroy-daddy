using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant : MonoBehaviour
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
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 a = transform.position;
        Vector3 b = target.position;
        if (Vector3.Distance(a, b) < 10)
        {
            animator.enabled = true;
            
        }
        if(animator.enabled == true)
        {
            transform.position = Vector3.Lerp(a, b, 0.01f);
        }
        if (Vector3.Distance(a, b) < 2)
        {
            animator.Play("biteAnimation");

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
