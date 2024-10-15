using System;
using System.Linq;
using Unity.Entities.UI;
using UnityEngine;

public class DebugVisualTween : MonoBehaviour
{
    public Vector2[] linearEase = new Vector2[]{};

    [Range(0, 10)]
    public int CountEase = 1;

    [Range(0, 1.0f)]
    public float Dephasage;
    
    public float Speed;

    private void OnDrawGizmos()
    {
        DrawGridArea();
        DrawEase(linearEase);
        Gizmos.color = Color.gray;
        Gizmos.DrawLine(new Vector3(.5f, .0f), new Vector3(.5f, 1.0f));
        Gizmos.DrawLine(new Vector3(.0f, .5f), new Vector3(1.0f, .5f));
    }

    public void DrawEase(Vector2[] linearEase)
    {
        if(linearEase.Length == 0 ) return;

        Gizmos.color = Color.red;

        float spaceEvenly = 1.0f / (CountEase + 1);

        for (int i = 0; i < CountEase; i++)
        {
            // var p1 = new Vector3(spaceEvenly * (i+1), 0);
            // var p2 = new Vector3(spaceEvenly * (i+1), 1);
            var s = 1.0f / CountEase;

            var d = - Dephasage/CountEase * i;

            var p1 = new Vector3(Speed / (CountEase*2) + i * s + d, 0);
            var p2 = new Vector3(-Speed / (CountEase*2) + (i+1) * s + d, 1);
            
            var a = new Vector3(p1.x, p1.y);
            var b = new Vector3(p2.x , p2.y);

            Gizmos.DrawLine(a, b);
        }
    }

    public void DrawGridArea()
    {
        var points = new Vector2[]{new Vector2(0,0), new Vector2(1,0), new Vector2(1,1), new Vector2(0,1), new Vector2(0,0)};

        Gizmos.color = Color.gray;
        
        var previous = linearEase[0];

        foreach(var point in points)
        {
            var a = new Vector3(previous.x, previous.y);
            var b = new Vector3(point.x, point.y);

            Gizmos.DrawLine(a, b);

            previous = point;
        }
    }
}
