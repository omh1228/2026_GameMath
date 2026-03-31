using UnityEngine;
using UnityEngine.SceneManagement;

public class ChaserEnemy : MonoBehaviour
{
    public Transform Player;
    public float rotationSpeed = 50f;
    public float detectionRange = 8f;
    public float dashSpeed = 15f;
    public float StopDistance = 1.2f;
    public bool isDashing = false;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Player == null) return;

        if (!isDashing)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, Player.position);
            if (distance < detectionRange)
            {
                Debug.Log("발견 돌진 모드로 전환");
                isDashing = true;
            }
        }
        else
        {
            float distance = Vector3.Distance(transform.position, Player.position);
            if (distance > StopDistance)
            {
                Vector3 dir = (Player.position - transform.position).normalized;
                rb.MovePosition(transform.position + dir * dashSpeed * Time.deltaTime);
            }
            else
            {
                CheckParry();
                isDashing = false;
            }
        }
    }

    void CheckParry()
    {
        PlayerController pc = Player.GetComponent<PlayerController>();
        
        if (pc.isLeftParrying || pc.isRightParrying)
        {
            Debug.Log("패링 성공! 적 제거");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("패링 실패!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
