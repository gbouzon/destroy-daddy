using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class CurrencyBank : MonoBehaviour
{
    private float money;
    private float lerpTimer;

    public float maxMoney = 0f;
    public float chipSpeed = 2f;
    public Image frontMoneyBar;
    public Image backMoneyBar;

    public TextMeshProUGUI moneyText;
    // Start is called before the first frame update
    void Start()
    {
        money = maxMoney;
    }

    // Update is called once per frame
    void Update()
    {
        money = Mathf.Clamp(money, 0, maxMoney);
        UpdateMoneyUI();
        if (Input.GetKeyDown(KeyCode.U))
        {
            RemoveMoney(Random.Range(5,10));
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddMoney(Random.Range(5,10));
        }
    }

    public void UpdateMoneyUI()
    {
        float fillFrontMoney = frontMoneyBar.fillAmount;
        float fillBackMoney = backMoneyBar.fillAmount;
        float halfFraction = money / maxMoney;
        if (fillBackMoney > halfFraction)
        {
            frontMoneyBar.fillAmount = halfFraction;
            backMoneyBar.color = Color.white;
            lerpTimer += Time.deltaTime;
            float percentageComplete = lerpTimer / chipSpeed;
            percentageComplete = percentageComplete * percentageComplete;
            backMoneyBar.fillAmount = Mathf.Lerp(fillBackMoney, halfFraction, percentageComplete);
        }

        if (fillFrontMoney < halfFraction)
        {
            backMoneyBar.color = Color.green;
            backMoneyBar.fillAmount = halfFraction;
            lerpTimer += Time.deltaTime;
            float percentageComplete = lerpTimer / chipSpeed;
            percentageComplete = percentageComplete * percentageComplete;
            frontMoneyBar.fillAmount = Mathf.Lerp(fillFrontMoney, backMoneyBar.fillAmount, percentageComplete);
        }

        moneyText.text = Mathf.Round(money) + "/" + Mathf.Round(maxMoney);
    }

    public void RemoveMoney(float removeAmount)
    {
        money -= removeAmount;
        lerpTimer = 0f;
    }

    public void AddMoney(float addAmount)
    {
        money += addAmount;
        lerpTimer = 0f;
    }

    public void IncreaseCash(float cash)
    {
        maxMoney += (cash * 0.25f) *((1000- cash) * 0.25f);
        cash = maxMoney;
    }
}
