using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GachaManager : MonoBehaviour
{
    public TextMeshProUGUI resultText; // 결과 출력 UI

    // 🎯 1회 뽑기 버튼
    public void OnClickSingle()
    {
        string result = Simulate();
        resultText.text = $"결과: {result}";
    }

    // 🎯 10회 뽑기 버튼
    public void OnClickTen()
    {
        List<string> results = new List<string>();

        // 9번 일반 뽑기
        for (int i = 0; i < 9; i++)
        {
            results.Add(Simulate());
        }

        // 🔥 마지막은 A 이상 확정 (A : S = 2 : 1)
        float r = Random.value;
        string last = (r < 2f / 3f) ? "A" : "S";
        results.Add(last);

        resultText.text = "10연차: " + string.Join(", ", results);
    }

    // 🎲 가챠 확률 로직
    string Simulate()
    {
        float r = Random.value;

        if (r < 0.4f) return "C";      // 40%
        else if (r < 0.7f) return "B"; // 30%
        else if (r < 0.9f) return "A"; // 20%
        else return "S";               // 10%
    }
}