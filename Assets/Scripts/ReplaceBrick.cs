using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReplaceBrick : MonoBehaviour
{
    const float MAX_DISTANCE_NEIGHBORS = 1.0f;
    public BrickSO[] BricksSOs;
    public Transform[] TransformBricks;
    public List<Transform> TransformsRestants;
    public Transform Current;
    public Transform[] Pattern;

    public float WaitValue;

    private void Start()
    {
        Random.InitState(0);
        Parse(TransformBricks);
    }

    public void Parse(Transform[] transformBricks)
    {
        List<TransformBlock> blocks = new();
        
        TransformsRestants = transformBricks.OrderBy(p => Random.value).ToList();

        transformBricks.ToList().ForEach(t => t.gameObject.SetActive(false));

        while(TransformsRestants.Count > 0)
        {
            var pattern = SelectRandomBlock();
            Current = TransformsRestants[0];

            Transform[] transformPatterns = PatterAvailable(Current, TransformsRestants, pattern.Dimension);
            Pattern = transformPatterns;
            GameObject instance;
            if(transformPatterns.Length == 0)
            {
                TransformsRestants.Remove(Current);
                instance = Instantiate(BricksSOs[0].Prefab);
            }
            else
            {
                transformPatterns.ToList().ForEach(t => TransformsRestants.Remove(t));
                instance = Instantiate(pattern.Prefab);
            }
            instance.transform.position = Current.position;
            instance.transform.localScale = Vector3.one;
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
        
        return neighbors.Count != pattern.x * pattern.y ? new Transform[0]{} : neighbors.ToArray();
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
        return BricksSOs[Random.Range(1, BricksSOs.Length)];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Pattern.ToList().ForEach(t => Gizmos.DrawSphere(t.transform.position, .1f));

        Gizmos.color = Color.red;
        if(Current)
            Gizmos.DrawSphere(Current.transform.position, .1f);
    }
}