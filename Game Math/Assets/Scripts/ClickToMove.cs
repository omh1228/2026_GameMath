using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    [Header("Move Settings")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;

    private bool isMoving = false;
    private bool isSprinting = false;
    private Vector2 mouseScreenPosition;
    private Vector3 targetPosition;

    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    private float dashTimer = 0f;
    private bool isDashing = false;
    private Vector3 lastDirection;

    public void OnDash(InputValue value)
    {
        if (value.isPressed && !isDashing)
        {
            isDashing = true;
            dashTimer = dashDuration;

            isMoving = false;

            if (lastDirection == Vector3.zero)
                lastDirection = transform.forward;
        }
    }

    public void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed;
    }

    public void OnPoint(InputValue value)
    {
        mouseScreenPosition = value.Get<Vector2>();
    }

    public void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject != gameObject)
                {
                    targetPosition = hit.point;
                    targetPosition.y = transform.position.y;

                    isMoving = true;
                    break;
                }
            }
        }
    }

    void Update()
    {
        if (isDashing)
        {
            transform.Translate(lastDirection * dashSpeed * Time.deltaTime);

            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
            {
                isDashing = false;
            }
        }
        else if (isMoving)
        {
            Vector3 offset = targetPosition - transform.position;

            float sqrDist = offset.x * offset.x + offset.y * offset.y + offset.z * offset.z;
            float dist = Mathf.Sqrt(sqrDist);

            float currentSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;

            // 🔥 0 나누기 방지
            if (dist > 0.0001f)
            {
                transform.Translate(offset / dist * currentSpeed * Time.deltaTime);
                lastDirection = offset / dist;
            }

            if (dist < 0.1f)
            {
                isMoving = false;
            }
        }
    }
}