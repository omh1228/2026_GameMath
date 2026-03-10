using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

    public class  ClickToMove: MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 mouseScreenPosition;
    private Vector3 targetposition;
    private bool isMoving = false;


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
                if(hit.collider.gameObject != gameObject)
                {

                    targetposition = hit.point;
                    targetposition.y = transform.position.y;
                    isMoving = true;

                    break;
                }
            }
        }
    }


    void Update()
    {
        if (isMoving)
        {
            // 1. 방향 벡터 계산
            Vector3 dir = targetposition - transform.position;

            // 2. 벡터 길이 계산 (Distance 직접 계산)
            float length = Mathf.Sqrt(dir.x * dir.x + dir.y * dir.y + dir.z * dir.z);

            // 3. 도착했는지 확인
            if (length < 0.01f)
            {
                transform.position = targetposition;
                isMoving = false;
                return;
            }

            // 4. 정규화 (normalized 직접 계산)
            Vector3 direction = new Vector3(dir.x / length, dir.y / length, dir.z / length);

            // 5. 이동 거리 계산
            float moveStep = moveSpeed * Time.deltaTime;

            // 6. MoveTowards 기능 직접 구현
            if (moveStep >= length)
            {
                transform.position = targetposition;
                isMoving = false;
            }
            else
            {
                transform.position += direction * moveStep;
            }
        }
    }

}
    
    