using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    public Transform sun;
    public Transform mercury;
    public Transform venus;
    public Transform earth;
    public Transform moon;
    public Transform mars;
    public Transform jupiter;

    // 공전 반지름
    public float mercuryDist = 2f;
    public float venusDist = 3f;
    public float earthDist = 4f;
    public float moonDist = 1f;
    public float marsDist = 5f;
    public float jupiterDist = 7f;

    // 공전 속도
    public float mercurySpeed = 2f;
    public float venusSpeed = 1.5f;
    public float earthSpeed = 1f;
    public float moonSpeed = 3f;
    public float marsSpeed = 0.8f;
    public float jupiterSpeed = 0.5f;

    // 각도 저장
    float mercuryAngle;
    float venusAngle;
    float earthAngle;
    float moonAngle;
    float marsAngle;
    float jupiterAngle;

    void Update()
    {
        // 각도 증가 (Time.deltaTime 사용)
        mercuryAngle += mercurySpeed * Time.deltaTime;
        venusAngle += venusSpeed * Time.deltaTime;
        earthAngle += earthSpeed * Time.deltaTime;
        moonAngle += moonSpeed * Time.deltaTime;
        marsAngle += marsSpeed * Time.deltaTime;
        jupiterAngle += jupiterSpeed * Time.deltaTime;

        // 🌍 태양 기준 공전
        mercury.position = sun.position + new Vector3(
            Mathf.Cos(mercuryAngle) * mercuryDist,
            0,
            Mathf.Sin(mercuryAngle) * mercuryDist
        );

        venus.position = sun.position + new Vector3(
            Mathf.Cos(venusAngle) * venusDist,
            0,
            Mathf.Sin(venusAngle) * venusDist
        );

        earth.position = sun.position + new Vector3(
            Mathf.Cos(earthAngle) * earthDist,
            0,
            Mathf.Sin(earthAngle) * earthDist
        );

        mars.position = sun.position + new Vector3(
            Mathf.Cos(marsAngle) * marsDist,
            0,
            Mathf.Sin(marsAngle) * marsDist
        );

        jupiter.position = sun.position + new Vector3(
            Mathf.Cos(jupiterAngle) * jupiterDist,
            0,
            Mathf.Sin(jupiterAngle) * jupiterDist
        );

        // 🌙 달은 지구 기준으로 공전
        moon.position = earth.position + new Vector3(
            Mathf.Cos(moonAngle) * moonDist,
            0,
            Mathf.Sin(moonAngle) * moonDist
        );
    }
}