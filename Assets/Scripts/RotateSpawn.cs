using UnityEngine;

public class RotateSpawn : MonoBehaviour
{
    public float SpeedRotate = 1.0f;
    public float SpeedScale = 1.0f;

    public Camera Camera = Camera.main;

    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * SpeedRotate);
        transform.position += (Vector3.right + Vector3.forward) * SpeedScale;
        Camera.transform.Rotate(Vector3.up, Time.deltaTime*10);
    }
}
