using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBehaviour : MonoBehaviour
{
    private BoxCollider2D bc;
    private SpriteRenderer sr;
    private AudioSource ShatterAudio;

    // Start is called before the first frame update
    void Start() {
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        ShatterAudio = GetComponent<AudioSource>();
    }

    //// Update is called once per frame
    //void Update() {}

    IEnumerator DestroyGlass()
    {
        ShatterAudio.Play();
        yield return new WaitForSeconds(0.5f);
        bc.enabled = false;
        sr.enabled = false;
        yield return new WaitForSeconds(5.0f);
        bc.enabled = true;
        sr.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(DestroyGlass());
        }
    }
}
