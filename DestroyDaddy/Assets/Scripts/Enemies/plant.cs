using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant : MonoBehaviour
{
    private Transform target;
    private Animator animator;
    private int maxHealth = 4;
    private int currentHealth;
    GameObject player;
    public EnemyHealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        target = player.GetComponent<Transform>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.setMaxEnemyHealth(maxHealth);
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        Vector3 a = transform.position;
        Vector3 b = target.position;
        if (Vector3.Distance(a, b) < 10)
        {
            animator.enabled = true;
            
        }
        if(animator.enabled == true)
        {
            transform.position = Vector3.Lerp(a, b, 0.01f);
            animator.Play("walkAnimation");
        }
        if (Vector3.Distance(a, b) < 2)
        {
            animator.Play("biteAnimation");
            player.GetComponent<PlayerExperience>().TakeDamage(0.1f);
        }
    }
    public void HitByRay()
    {
        currentHealth--;
        healthBar.setEnemyHealth(currentHealth);
        if (currentHealth <= 0)
        {
            animator.Play("dieAnimation");
            Destroy(gameObject, 1);
            //GameObject.Find("GoldCoin").GetComponentInChildren<Currency>().SpawnCurreny(transform, 5);
            player.GetComponent<XPBar>().GainExperienceFlatRate(10);
        }
    }
}
