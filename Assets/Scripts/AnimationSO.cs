using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationSO", menuName = "AnimationSO", order = 0)]
public class AnimationSO : ScriptableObject
{
    public float Duration;
    public float Speed;
    public float Phase;
    public int Quantity;

    public float Ease(int index, float time)
    {
        var stepTime = 1.0f / Quantity;

        var d = Phase / Quantity * index;

        float src = index * stepTime - d;
        float dst = index * stepTime + stepTime - d;
        float value = math.clamp(time * (1 - Phase), src, dst);
        value = math.remap(src, dst, 0, 1, value);

        return value;
    }
}