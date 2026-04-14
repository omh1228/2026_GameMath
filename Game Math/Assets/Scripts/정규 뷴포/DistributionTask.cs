using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class DistributionTask : MonoBehaviour
{
    [Header("UI 연결 (2개로 분리)")]
    public TMP_Text battleResultText; // 상단용
    public TMP_Text itemInventoryText; // 하단용

    [Header("시뮬레이션 설정")]
    [SerializeField] float critChance = 0.2f;
    [SerializeField] float meanDamage = 20f;
    [SerializeField] float stdDevDamage = 5f;
    [SerializeField] float enemyHP = 100f;
    [SerializeField] float poissonLambda = 2f;
    [SerializeField] float hitRate = 0.6f;
    [SerializeField] float critDamageRate = 2f;
    [SerializeField] int maxHitsPerTurn = 5;

    // --- 통계용 누적 변수들을 여기에 선언해야 오류가 사라집니다 ---
    int totalTurns, totalEnemies, killedEnemies, totalHits, totalAttempts, totalCrits;
    float maxDmg, minDmg;
    int potion, gold, weaponNormal, weaponRare, armorNormal, armorRare;
    string[] rewards = { "Gold", "Weapon", "Armor", "Potion" };

    public void StartSimulation()
    {
        ResetStats();
        bool rareItemObtained = false;
        float currentRareChance = 0.05f;

        while (!rareItemObtained)
        {
            totalTurns++;
            int enemyCount = SamplePoisson(poissonLambda);
            totalEnemies += enemyCount;

            for (int i = 0; i < enemyCount; i++)
            {
                int hits = SampleBinomial(maxHitsPerTurn, hitRate);
                totalAttempts += maxHitsPerTurn;
                totalHits += hits;

                float turnDmg = 0;
                for (int j = 0; j < hits; j++)
                {
                    float dmg = SampleNormal(meanDamage, stdDevDamage);
                    if (Random.value < critChance) { dmg *= critDamageRate; totalCrits++; }

                    turnDmg += dmg;
                    if (dmg > maxDmg) maxDmg = dmg;
                    if (dmg < minDmg) minDmg = dmg;
                }

                if (turnDmg >= enemyHP)
                {
                    killedEnemies++;
                    string r = rewards[Random.Range(0, rewards.Length)];
                    if (r == "Potion") potion++;
                    else if (r == "Gold") gold++;
                    else if (r == "Weapon")
                    {
                        if (Random.value < currentRareChance) { weaponRare++; rareItemObtained = true; }
                        else weaponNormal++;
                    }
                    else if (r == "Armor")
                    {
                        if (Random.value < currentRareChance) { armorRare++; rareItemObtained = true; }
                        else armorNormal++;
                    }
                }
            }
            if (rareItemObtained) break;
            currentRareChance += 0.05f;
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        // 1. 전투 결과 (상단)
        StringBuilder sbBattle = new StringBuilder();
        sbBattle.AppendLine("<size=120%><color=#FFCC00>전투 결과</color></size>");
        sbBattle.AppendLine($"총 진행 턴 수 : {totalTurns}");
        sbBattle.AppendLine($"발생한 적 : {totalEnemies}");
        sbBattle.AppendLine($"처치한 적 : {killedEnemies}");
        sbBattle.AppendLine($"공격 명중 결과 : {(totalAttempts > 0 ? (float)totalHits / totalAttempts * 100 : 0):F2}%");
        sbBattle.AppendLine($"발생한 치명타 결과 : {(totalHits > 0 ? (float)totalCrits / totalHits * 100 : 0):F2}%");
        sbBattle.AppendLine($"최대 데미지 : {maxDmg:F2} / 최소 : {minDmg:F2}");

        battleResultText.text = sbBattle.ToString();

        // 2. 획득 아이템 (하단)
        StringBuilder sbItems = new StringBuilder();
        sbItems.AppendLine("<size=120%><color=#FFCC00>획득한 아이템</color></size>");
        sbItems.AppendLine($"포션 : {potion} / 골드 : {gold}");
        sbItems.AppendLine($"무기(일반/레어) : {weaponNormal} / <color=orange>{weaponRare}</color>");
        sbItems.AppendLine($"방어구(일반/레어) : {armorNormal} / <color=orange>{armorRare}</color>");

        itemInventoryText.text = sbItems.ToString();
    }

    void ResetStats()
    {
        totalTurns = totalEnemies = killedEnemies = totalHits = totalAttempts = totalCrits = 0;
        maxDmg = 0; minDmg = 999f;
        potion = gold = weaponNormal = weaponRare = armorNormal = armorRare = 0;
    }

    // --- 분포 함수들 ---
    int SamplePoisson(float lambda)
    {
        int k = 0; float p = 1f; float L = Mathf.Exp(-lambda);
        while (p > L) { k++; p *= Random.value; }
        return k - 1;
    }
    int SampleBinomial(int n, float p)
    {
        int s = 0; for (int i = 0; i < n; i++) if (Random.value < p) s++;
        return s;
    }
    float SampleNormal(float mean, float stdDev)
    {
        float u1 = Random.value; float u2 = Random.value;
        float z = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Cos(2.0f * Mathf.PI * u2);
        return mean + stdDev * z;
    }
}