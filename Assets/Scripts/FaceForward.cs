using UnityEngine;

public class FaceForward : MonoBehaviour
{
    private Vector3 initialLocalScale;

    void Start()
    {
        initialLocalScale = transform.localScale;
    }

    void LateUpdate()
    {
        if (transform.parent == null) return;

        transform.rotation = Quaternion.identity;

        float parentDirection = Mathf.Sign(transform.parent.localScale.x);
        
        transform.localScale = new Vector3(
            initialLocalScale.x * parentDirection, 
            initialLocalScale.y, 
            initialLocalScale.z
        );
    }
}