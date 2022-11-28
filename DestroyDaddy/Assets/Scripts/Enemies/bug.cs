using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bug : MonoBehaviour
{
    private Transform target;
    private float t;
    private Animator anim;
    private int maxHealth = 2;
    private int currentHealth;
    GameObject player;
    void Start()
    {
        //make every time a different speed for funzies
        t = Random.Range(0.001f, 0.01f);
        //initating target as the main character
        player = GameObject.FindWithTag("Player");
        target = player.GetComponent<Transform>();
        //getting animator
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        //getting position of character and bug
        Vector3 a = transform.position;
        Vector3 b = target.position;
        //going towards character
        
        if (Vector3.Distance(a, b) < 2)
        {
            anim.Play("stingAnimation");
            player.GetComponent<PlayerExperience>().TakeDamage(0.05f);
            
        }
        else {
            transform.position = Vector3.Lerp(a, b, t);
            transform.LookAt(target);
        }
    }
    public void HitByRay()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            anim.Play("dieAnimation");
            Destroy(gameObject, 1);
            player.GetComponent<XPBar>().GainExperienceFlatRate(5);
        }
    }
}
