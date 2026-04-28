using UnityEngine;

public class CameraSierp : MonoBehaviour
{
    public Transform target;

    float speed = 2f;

    // Update is called once per frame
    void Update()
    {
        Quaternion lookRot = Quaternion.LookRotation(target.position - transform.position);

        float t = 1f - Mathf.Exp(-speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, t);
    }

    Quaternion ManulSlerp(Quaternion from, Quaternion to, float t)
    {
        float dot = Quaternion.Dot(from, to);

        if(dot < 0f)
        {
            to = new Quaternion(-to.x, -to.y, -to.x, -to.w);
            dot = -dot;
        }

        if(1f - dot < 0.01f)
        {
            Quaternion lerp = new Quaternion(
                Mathf.Lerp(from.x, to.x, t),
                Mathf.Lerp(from.y, to.y, t),
                Mathf.Lerp(from.z, to.z, t),
                Mathf.Lerp(from.w, to.w, t)
                );
            return lerp.normalized;
        }

        float theta = Mathf.Acos(dot);
        float sintheta = Mathf.Sin(theta);

        float ratioA = Mathf.Sin((1f - t) * theta) / sintheta;
        float ratioB = Mathf.Sin(t * theta) / sintheta;

        Quaternion result = new Quaternion(
            ratioA * from.x + ratioB * to.x,
            ratioA * from.y + ratioB * to.y,
            ratioA * from.z + ratioB * to.z,
            ratioA * from.w + ratioB * to.w
            );
        return result.normalized;
    }
}
