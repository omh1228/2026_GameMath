using UnityEngine;
using UnityEngine.InputSystem;

public class TargetingSystem : MonoBehaviour
{
    public Transform currentTarget;
    public PredictionLineRenderer line;

    // 우클릭 입력 (Invoke Unity Events용)
    public void OnRightClick(InputValue value)
    {
        if (!value.isPressed) return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                currentTarget = hit.transform;

                // 조준선 연결
                if (line != null)
                {
                    line.startPos = transform;
                    line.endPos = currentTarget;
                }

                Debug.Log("타겟 설정: " + hit.collider.name);
            }
            else
            {
                ClearTarget();
            }
        }
        else
        {
            ClearTarget();
        }
    }

    void ClearTarget()
    {
        currentTarget = null;

        if (line != null)
        {
            line.endPos = null;
        }

        Debug.Log("타겟 해제");
    }

}