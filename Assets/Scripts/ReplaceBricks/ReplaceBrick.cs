using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct BrickTransform
{
    public BrickSO BrickSO;
    public Transform Transform;
}

public class ReplaceBrick : MonoBehaviour
{
    const float MAX_DISTANCE_NEIGHBORS = 1.0f;
    public BrickSO[] BricksSOs;
    public Transform[] TransformBricks;
    public List<Transform> TransformsRestants;
    public Transform Current;
    public Transform[] Pattern;

    private void Start()
    {
        Random.InitState(0);
    }

    public List<BrickTransform> Parse(Transform[] transformBricks)
    {
        List<BrickTransform> Blocks = new();
        
        TransformsRestants = transformBricks.OrderBy(p => Random.value).ToList();

        transformBricks.ToList().ForEach(t => t.gameObject.SetActive(false));

        while(TransformsRestants.Count > 0)
        {
            var indexPattern = SelectRandomBlock();
            var pattern = BricksSOs[indexPattern];
            Current = TransformsRestants[0];

            Pattern = PatterAvailable(Current, TransformsRestants, pattern.Dimension);
            // indexPattern--;
            // while(Pattern.Length == 0 && indexPattern > 0)
            // {
            //     Pattern = PatterAvailable(Current, TransformsRestants, BricksSOs[indexPattern].Dimension);
            //     indexPattern--;
            // }

            GameObject instance;
            
            if(Pattern.Length == 0)
            {
                TransformsRestants.Remove(Current);
                instance = Instantiate(BricksSOs[0].Prefab);
            }
            else
            {
                Pattern.ToList().ForEach(t => TransformsRestants.Remove(t));
                instance = Instantiate(pattern.Prefab);
            }
            
            Blocks.Add(new BrickTransform(){
                Transform = instance.transform,
                BrickSO = pattern
            });

            instance.transform.position = Current.position;
            instance.transform.localScale = Vector3.one *.2f;
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