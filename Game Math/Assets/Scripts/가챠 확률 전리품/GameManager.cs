using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public int attack = 30;
    public float critRate = 0.3f;

    [Header("Enemy")]
    public int maxHP = 300;
    int currentHP;

    [Header("Critical System")]
    int totalHits = 0;
    int critHits = 0;

    [Header("Drop Rate")]
    float normal = 0.5f;
    float rare = 0.3f;
    float epic = 0.15f;
    float legendary = 0.05f;

    [Header("UI")]
    public TextMeshProUGUI logText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI dropRateText;

    void Start()
    {
        currentHP = maxHP;
        UpdateUI();
    }

   
    public void OnClickAttack()
    {
        bool isCrit = RollCrit();

        int damage = isCrit ? attack * 2 : attack;
        currentHP -= damage;

        string log = isCrit ? " 크리티컬!" : "일반 공격";
        log += $" → 데미지 {damage}";

        if (currentHP <= 0)
        {
            log += "\n 적 처치!";
            string item = DropItem();
            log += $"\n 획득: {item}";
            RespawnEnemy();
        }

        logText.text = log;
        UpdateUI();
    }

    
    bool RollCrit()
    {
        totalHits++;

        float currentRate = (totalHits > 0) ? (float)critHits / totalHits : 0f;

        if (currentRate < critRate && (float)(critHits + 1) / totalHits <= critRate)
        {
            critHits++;
            return true;
        }

        if (currentRate > critRate)
        {
            return false;
        }

        if (Random.value < critRate)
        {
            critHits++;
            return true;
        }

        return false;
    }

  
    string DropItem()
    {
        float r = Random.value;
        string result = "";

        if (r < normal) result = "일반";
        else if (r < normal + rare) result = "고급";
        else if (r < normal + rare + epic) result = "희귀";
        else result = "전설";

        
        if (result == "전설")
        {
            normal = 0.5f;
            rare = 0.3f;
            epic = 0.15f;
            legendary = 0.05f;
        }
        else
        {
            legendary += 0.015f;
            normal -= 0.005f;
            rare -= 0.005f;
            epic -= 0.005f;
        }

        return result;
    }

    
    void RespawnEnemy()
    {
        currentHP = maxHP;
    }

    
    void UpdateUI()
    {
        hpText.text = $"HP: {currentHP} / {maxHP}";

        float currentCritRate = (totalHits > 0) ? (float)critHits / totalHits : 0f;

        dropRateText.text =
            $"크리 확률: {currentCritRate:P1}\n" +
            $"일반: {normal:P1}\n고급: {rare:P1}\n희귀: {epic:P1}\n전설: {legendary:P1}";
    }
}