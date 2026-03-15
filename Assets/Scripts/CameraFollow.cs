using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Drag your Player object here in the Inspector
    public Vector3 offset = new Vector3(0, 2, -10); // Adjust to position the camera

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}