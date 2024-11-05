using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReplaceBrick : MonoBehaviour
{
    const float MAX_DISTANCE_NEIGHBORS = 1.0f;
    public BrickSO[] BricksSOs;

    public void Parse(List<Transform> transformBricks)
    {
        List<TransformBlock> blocks = new();
        
        List<Transform> transforms = transformBricks.ToList();
        
        List<Transform>.Enumerator enumerator = transforms.ToList().GetEnumerator();

        while(enumerator.MoveNext())
        {
            var pattern = SelectRandomBlock();
            var current = enumerator.Current;

            Transform[] transformPatterns = PatterAvailable(current, transforms, pattern.Dimension);
            
            if(transformPatterns.Length == 0)
            {
                transformBricks.Remove(current);
            }
            else
            {
                transformPatterns.Select(t => transformBricks.Remove(t));

                // blocks.Add(new TransformBlock()
                // {
                //     Position = current.position,
                //     Block = pattern.
                // });
            }
        }
    }

    public Transform[] PatterAvailable(Transform current, List<Transform> transforms, Vector2Int pattern)
    {
        var neighbors = new List<Transform>();
        transforms = transforms
            .Where(t => t.position.y == current.position.y)
            .ToList();

        
        
        for (int x = 0; x < pattern.x; x++)
        {
            for (int y = 0; y < pattern.y; y++)
            {
                Vector3 brickPosition = current.position + Vector3.right * x + Vector3.forward * y;

                var neighbor = transforms.Where(t => t.position == brickPosition).ToList();

                neighbors.AddRange(neighbor);
            }
        }

        return neighbors.ToArray();
    }

    public List<Transform> SearchNeighbors(Transform current, Transform[] transforms)
    {
        List<Transform> neighbors = new();

        return neighbors
        .Where(t => t.position.y == current.position.y)
        .Where(t => Vector3.Distance(t.position, current.position) <= MAX_DISTANCE_NEIGHBORS).ToList();
    }

    public BrickSO SelectRandomBlock()
    {
        return BricksSOs[Random.Range(1, BricksSOs.Length - 1)];
    }
}