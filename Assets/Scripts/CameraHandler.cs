using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    public Transform target;             // The object to follow
    public Vector3 offset; // Offset from the target

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            // Do NOT modify rotation — it will stay fixed
        }
    }
}
