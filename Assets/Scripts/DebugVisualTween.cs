using System.Linq;
using UnityEngine;

public class DebugVisualTween : MonoBehaviour
{
    public Vector2[] linearEase = new Vector2[]{};

    [Range(0, 100)]
    public int CountEase = 1;

    private void OnDrawGizmos()
    {
        DrawGridArea();
        DrawEase(linearEase);
    }

    public void DrawEase(Vector2[] linearEase)
    {
        if(linearEase.Length == 0 ) return;

        Gizmos.color = Color.red;

        float spaceEvenly = 1.0f / (CountEase + 1);

        for (int i = 0; i < CountEase; i++)
        {
            linearEase[0] = new Vector3(spaceEvenly * (i+1), 0);
            linearEase[1] = new Vector3(spaceEvenly * (i+1), 1);
            var previous = linearEase[0];

            foreach (var point in linearEase.Skip(1))
            {
                var a = new Vector3(previous.x, previous.y);
                var b = new Vector3(point.x, point.y);

                Gizmos.DrawLine(a, b);

                previous = point;
            }
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
