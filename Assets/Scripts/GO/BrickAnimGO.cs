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
    [Range(0, 1.0f)]
    public float Dephasage = 1;
    public bool PlayDebug;
    public ReplaceBrick ReplaceBrick;
    public AnimationSO AnimationSO;

    void Start()
    {
        PlayDebug = true;

        List<Transform> transforms;
        
        if(ReplaceBrick)
            transforms = ReplaceBrick.Parse(ReplaceBrick.TransformBricks).Select(p => p.Transform).ToList();
        else
            transforms = Artwork.GetComponentsInChildren<Transform>().ToList();

        UnityEngine.Random.InitState(42);
        
        bricks = transforms
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
            if(CurrentTime >= Duration) PlayDebug = false;
        }
        
        AnimateSO();
    }

    public float EaseOutSine(float x) => Mathf.Sin((x * Mathf.PI) / 2);

    public void Animate()
    {
        var startPosition = StartAnimate.transform.position;

        var stepTime = 1.0f / bricks.Count;
        
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

    public void AnimateSO()
    {
         var startPosition = StartAnimate.transform.position;
        
        for (int i = 0; i < bricks.Count; i++)
        {
            var brick = bricks[i];

            var value = AnimationSO.Ease(i, CurrentTime, bricks.Count);
            
            var direction = brick.Pos - startPosition;

            brick.Transform.position = startPosition + direction * value;
            
            brick.Transform.localScale = brick.Scale * EaseOutSine(value);
            brick.Transform.rotation = Quaternion.Lerp(brick.RotStart, brick.RotEnd, value);
        }
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(50, 50, 100, 50), "Reload"))
        {
            PlayDebug = true;
            CurrentTime = 0;
        }

        GUI.Toggle(new Rect(165, 50, 100, 50), PlayDebug, PlayDebug ? "Play" : "Stop");
    }
}