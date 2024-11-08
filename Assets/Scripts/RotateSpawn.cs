using UnityEngine;

public class RotateSpawn : MonoBehaviour
{
    public float SpeedRotate = 1.0f;
    public float SpeedScale = 1.0f;

    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * SpeedRotate);
        transform.localScale += Vector3.one * Time.deltaTime * SpeedScale;
    }
}
