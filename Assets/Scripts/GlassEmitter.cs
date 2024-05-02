using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassEmitter : MonoBehaviour
{
    private ParticleSystem ps;
    public float duration;

    IEnumerator PlayPerticleSys()
    {
        ps.Play();
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        duration = ps.main.duration + ps.main.startLifetime.constantMax;
        StartCoroutine(PlayPerticleSys());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
