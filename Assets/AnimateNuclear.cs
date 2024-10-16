using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AnimateNuclear : MonoBehaviour
{

    public float time = 0.0f;
    public Material material;
    public Vector3 Position;
    public Volume Volume;
    public Bloom Bloom;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        Position = transform.position;
        Volume.profile.TryGet(out Bloom);
    }

    void Update()
    {
        time += Time.deltaTime;

        material.SetFloat("_Speed", time * time);

        var t = Vector3.right * time * time * Random.Range(-.3f, 3.0f) * .0005f;
        t += Vector3.forward * time * time * Random.Range(-.3f, 3.0f) * .0005f;
        
        if(time > 5.0f)
        {
            t += Vector3.up * (time-5.0f) * (time-5.0f) *.5f;

            Bloom.intensity.value += (time-5.0f) *.5f;
        }

        transform.position = Position + t;
    }
}
