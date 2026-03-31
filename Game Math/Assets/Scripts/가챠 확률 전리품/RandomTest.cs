using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomTest : MonoBehaviour
{
    public TextMeshProUGUI[] labels = new TextMeshProUGUI[6];
    int[] counts = new int[6];

    public int trials = 100;

    // 버튼 클릭 시 실행할 함수
    public void RunTest()
    {
        // 카운트 초기화
        System.Array.Clear(counts, 0, counts.Length);

        // 랜덤 실행
        for (int i = 0; i < trials; i++)
        {
            int result = Random.Range(1, 7);
            counts[result - 1]++;
        }

        // 결과 출력
        for (int i = 0; i < counts.Length; i++)
        {
            float percent = (float)counts[i] / trials * 100f;
            string result = $"{i + 1}: {counts[i]}회 {percent:F2}%";
            labels[i].text = result;
        }
    }

    /*
    void Start()
    {
        float chance = Random.value;
        int dice = Random.Range(1, 7);

        System.Random sysRand = new System.Random();
        int number = sysRand.Next(1, 7);


        Debug.Log("Unity Random (Random.value): " + chance);
        Debug.Log("Unity Random (Random.Range): " + dice);
        Debug.Log("System Random (Next):" + number);
    }
    */
}

    

