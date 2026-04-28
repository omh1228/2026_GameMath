using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public Vector3 moveDir = Vector3.right;
    public float distance = 3f;
    public float speed = 2f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = startPos + moveDir * t * distance;
    }
}