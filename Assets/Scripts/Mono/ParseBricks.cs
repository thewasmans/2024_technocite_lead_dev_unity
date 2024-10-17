using System.Collections.Generic;
using UnityEngine;

public struct BrickTypeTransform
{

}

public enum BrickType
{
    Brick_1x1,
    Brick_2x2,
    Brick_2x4,
}

public class ParseBricks
{
    public float Distance = 1.0f;

    public void Parse(List<Transform> transforms)
    {
        var currentBrick = transforms[0];

        foreach (var brick in transforms.GetRange(1, transforms.Count))
        {
            float v = Vector3.Distance(currentBrick.position, brick.position);
        }
    }
}