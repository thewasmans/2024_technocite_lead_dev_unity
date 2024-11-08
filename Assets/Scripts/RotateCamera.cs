using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime*10);
    }
}
