using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BrickType
{
    Brick_1x1,
    Brick_2x1,
    Brick_2x2,
    Brick_2x4,
}

public struct BrickTransform
{
    public BrickSO BrickSO;
    public Transform Transform;
}

public class ReplaceBrick
{
    const float MAX_DISTANCE_NEIGHBORS = 1.0f;
    public BrickSO[] BricksSOs;
    public Transform[] TransformBricks;
    public List<Transform> TransformsRestants;
    public Transform Current;
    public Transform[] Pattern;

    public List<BrickTransform> Parse(Transform[] transformBricks)
    {
        Random.InitState(0);
        
        List<BrickTransform> Blocks = new();
        
        TransformsRestants = transformBricks.OrderBy(p => Random.value).ToList();

        transformBricks.ToList().ForEach(t => t.gameObject.SetActive(false));

        while(TransformsRestants.Count > 0)
        {
            var indexPattern = SelectRandomBlock();
            var pattern = BricksSOs[indexPattern];
            Current = TransformsRestants[0];

            Pattern = PatterAvailable(Current, TransformsRestants, pattern.Dimension);
            
            if(Pattern.Length == 0)
            {
                TransformsRestants.Remove(Current);
                
                Blocks.Add(new BrickTransform(){
                    Transform = Current,
                    BrickSO = BricksSOs[0]
                });
            }
            else
            {
                Pattern.ToList().ForEach(t => TransformsRestants.Remove(t));
                Blocks.Add(new BrickTransform(){
                    Transform = Current,
                    BrickSO = pattern
                });
            }
        }

        return Blocks;
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
        
        return neighbors.Count != pattern.x * pattern.y ? new Transform[0]{} : neighbors.ToArray();
    }

    public List<Transform> SearchNeighbors(Transform current) => new List<Transform>()
        .Where(t => t.position.y == current.position.y)
        .Where(t => Vector3.Distance(t.position, current.position) <= MAX_DISTANCE_NEIGHBORS)
        .ToList();

    public int SelectRandomBlock() => Random.Range(1, BricksSOs.Length);
}