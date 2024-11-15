using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Artwork", menuName = "Artwork", order = 0)]
public class ArtworkSO : ScriptableObject
{
    public GameObject MeshArtwork;
    public BrickSO[] PrefabsBricks;
    public AnimationSO Animation;
    public float ScaleBrick;
    
    public Transform[] TransformBricks
    {
        get
        {
            var trsChild = new List<Transform>();
            
            foreach (Transform trs in MeshArtwork.transform) trsChild.Add(trs);

            return trsChild.ToArray();
        }
    }
}