using UnityEngine;

[CreateAssetMenu(fileName = "BrickSO", menuName = "BrickSO", order = 0)]

public class BrickSO : ScriptableObject
{
    public GameObject Prefab;
    public BrickType Type;
    public Vector2Int Dimension;
}