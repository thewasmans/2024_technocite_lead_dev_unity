using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public struct BrickLego
{
    public Vector3 Pos;
    public Quaternion RotStart;
    public Quaternion RotEnd;
    public Vector3 Scale;
    public Transform Transform;
}

public class BrickAnimGO : MonoBehaviour
{
    public Transform StartAnimate;
    public GameObject Artwork;
    public List<BrickLego> bricks = new();
    public float Duration = 0;
    public float CurrentTime = 0;
    public float Speed = 1;
    /// <summary>
    /// Influence the density brick interpolates in //. With 0 each block will animate one by one, with 1 all blocks will animate
    /// </summary>
    [Range(0, 1.0f)]
    public float Dephasage = 1;
    public bool PlayDebug;

    void Start()
    {
        PlayDebug = true;
        var transformBricks = Artwork.GetComponentsInChildren<Transform>();

        UnityEngine.Random.InitState(42);
        
        bricks = transformBricks
        .Where(b => b.name != "Bricks")
        .OrderBy(b => b.transform.position.y)
        .Select(b => new BrickLego(){
            Pos = b.position,
            RotEnd = b.rotation,
            RotStart = b.rotation * UnityEngine.Random.rotation,
            Scale = b.localScale,
            Transform = b.transform
        }).ToList();
    }

    void Update()
    {
        if(PlayDebug)
        {
            CurrentTime += Time.deltaTime * Speed;
            CurrentTime = Mathf.Lerp(0, Duration, CurrentTime);
            if(CurrentTime >= Duration) PlayDebug = false;
        }
        
        Animate();
    }

    public float EaseOutSine(float x)
    {
        return Mathf.Sin((x * Mathf.PI) / 2);
    }


    public void Animate()
    {
        var startPosition = StartAnimate.transform.position;

        var stepTime = Duration / bricks.Count;
        
        for (int i = 0; i < bricks.Count; i++)
        {
            var brick = bricks[i];

            var d = Dephasage / bricks.Count * i;

            float src = i * stepTime - d;
            float dst = i * stepTime + stepTime - d;
            float value = math.clamp(CurrentTime * (1 - Dephasage), src, dst);
            value = math.remap(src, dst, 0, 1, value);
            
            var direction = brick.Pos - startPosition;

            brick.Transform.position = startPosition + direction * value;
            
            brick.Transform.localScale = brick.Scale * EaseOutSine(value);
            brick.Transform.rotation = Quaternion.Lerp(brick.RotStart, brick.RotEnd, value);
        }
    }
}