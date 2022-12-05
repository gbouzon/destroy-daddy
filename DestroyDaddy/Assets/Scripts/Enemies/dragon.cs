using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragon : MonoBehaviour
{
    private Transform target;
    private Animator animator;
    private int maxHealth = 2;
    private int currentHealth;
    GameObject player;
    public EnemyHealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        target = player.GetComponent<Transform>();
        animator = GetComponent<Animator>();
        healthBar.setMaxEnemyHealth(maxHealth);
        currentHealth = maxHealth;
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
        if (Vector3.Distance(a, b) < 2)
        {
            animator.Play("armSwipeAnimation");
            player.GetComponent<PlayerExperience>().TakeDamage(0.15f);
        }
        else
        {
            animator.Play("walkAnimation");
            transform.position = Vector3.Lerp(a, b, 0.01f);
            transform.LookAt(target);
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
            player.GetComponent<PlayerCurrency>().addMoney(5);
            player.GetComponent<XPBar>().GainExperienceFlatRate(15);
        }
    }
}
