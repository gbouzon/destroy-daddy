using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExperience : MonoBehaviour
{
    public static float health = float.MaxValue;
    private float lerpTimer; // use to animate the health bar 
    public float maxHealth = 100;
    public float chipSpped = 2f; // control the delayed bar take to catch up to the other
    public static int startCount = 0;
    public Image frontHealthBar; // the health bar that will be animated in front
    public Image backHealthBar; // the health bar that will be animated in back
    public TextMeshProUGUI healthText;
    Transform target;
    GameObject enemy;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject enemy = GameObject.FindWithTag("enemyPrefab");
        if (startCount == 0)
        {
            health = maxHealth;
            startCount++;
        }
        target = enemy.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // enemy = GameObject.FindWithTag("enemyPrefab");
        // target = enemy.GetComponent<Transform>();
        // //character and enemy position
        // Vector3 a = transform.position;
        // Vector3 b = target.position;
        // Debug.Log(Vector3.Distance(a,b));
        // if (Vector3.Distance(a, b) < 1)
        // {
        //     //TakeDamage(1);
        //     UpdateHealthUI();
        //     health = Mathf.Clamp(health, 0, maxHealth);
        // }
         // clamp health so its never below 0 
        UpdateHealthUI();
        health = Mathf.Clamp(health, 0, maxHealth);
        

    }

    public void UpdateHealthUI()
    {
        Debug.Log(health);
        float fillFront = frontHealthBar.fillAmount; // back health bar 
        float fillBack = backHealthBar.fillAmount; // front health 
        float hFraction = health / maxHealth; // fraction the value between 0 and 1
        if (fillBack > hFraction) // check to see if damage was taken on the bar
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime; // increment lerptimer
            float percentComplete = lerpTimer / chipSpped; // track the completion of the lerp
            percentComplete = percentComplete * percentComplete; // animation look better
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, hFraction, percentComplete); // bar chasing the front bar
        }

        if (fillFront < hFraction) // check to see that the player gets new health
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpped;
            percentComplete = percentComplete * percentComplete; // animation look better
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentComplete);
        }

        healthText.text = Mathf.Round(health) + "/" + Mathf.Round(maxHealth); // update the text on the health bar
    }

    /*
     * Take damage method
     * remove from player health
     */
    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }
    /*
     * Restore health method
     * add health to the player
     */
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
    /*
     * increase player health based on their level
     */
    public void IncreaseHealth(int level)
    {
        // increase health based on level
        maxHealth += (health * 0.01f) * ((100 - level) * 0.1f);
        // refill health bar
        health = maxHealth;
    }
}
