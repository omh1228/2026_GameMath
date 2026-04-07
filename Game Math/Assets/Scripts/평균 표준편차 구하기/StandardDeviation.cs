using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StandardDeviation : MonoBehaviour
{
    public int sampleCount = 1000;
    public float randomMin = 0;
    public float randomMax = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StandardDe();
    }

    // Update is called once per frame
    void StandardDe()
    {
        int n = sampleCount;
        float[] samples = new float[n];
        for (int i = 0; i < n; i++)
        {
            samples[i] = Random.Range(0f, 1f);
        }

        float mean = samples.Average();
        float sumofSquares = samples.Sum(x => Mathf.Pow(x - mean, 2));
        float stdDev = Mathf.Sqrt(sumofSquares / n);

        Debug.Log($"평균: {mean}, 표준편차: {stdDev}");
    }
}
