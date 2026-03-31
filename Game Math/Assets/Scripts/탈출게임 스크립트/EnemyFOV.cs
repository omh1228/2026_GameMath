using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    public Transform player;

    public float viewAngle = 60f;   // 시야각
    public float viewDistance = 5f; // 최대 거리

    void Update()
    {
        // 🔹 방향 벡터 (적 → 플레이어)
        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0f;

        // 🔹 거리 계산
        float sqrDist = toPlayer.x * toPlayer.x + toPlayer.y * toPlayer.y + toPlayer.z * toPlayer.z;
        float dist = Mathf.Sqrt(sqrDist);

        // 🔹 거리 체크 (먼저 거르기)
        if (dist > viewDistance)
        {
            transform.localScale = Vector3.one;
            return;
        }

        // 🔹 정면 방향
        Vector3 forward = transform.forward;
        forward.y = 0f;

        // 🔹 정규화 (직접 계산)
        if (dist > 0.0001f)
        {
            float invDist = 1f / dist;

            Vector3 dir = new Vector3(
                toPlayer.x * invDist,
                toPlayer.y * invDist,
                toPlayer.z * invDist
            );

            float forwardMag = Mathf.Sqrt(forward.x * forward.x + forward.z * forward.z);
            float invForwardMag = 1f / forwardMag;

            Vector3 forwardNorm = new Vector3(
                forward.x * invForwardMag,
                0,
                forward.z * invForwardMag
            );

            // 🔹 내적
            float dot = forwardNorm.x * dir.x + forwardNorm.z * dir.z;

            // 🔹 각도 판정 (cos 값 비교)
            float cosHalfFOV = Mathf.Cos(viewAngle * 0.5f * Mathf.Deg2Rad);

            if (dot >= cosHalfFOV)
            {
                // 👀 시야 안
                transform.localScale = Vector3.one * 2;
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }
    }
}
