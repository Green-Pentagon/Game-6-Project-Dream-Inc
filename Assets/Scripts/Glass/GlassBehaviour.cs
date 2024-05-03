using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBehaviour : MonoBehaviour
{
    private const int REPAIR_PENALTY = 2000;
    private GlobalPingSystem GlobalPingSys;

    public GameObject EmitterPrefab;
    private BoxCollider2D bc;
    private SpriteRenderer sr;
    private AudioSource ShatterAudio;

    // Start is called before the first frame update
    void Start() {
        GlobalPingSys = GameObject.FindGameObjectWithTag("GlobalPing").GetComponent<GlobalPingSystem>();

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
        GlobalPingSys.WindowBroken(REPAIR_PENALTY);
        GameObject temp;
        temp = Instantiate(EmitterPrefab, transform);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(temp.GetComponent<GlassEmitter>().duration);
        bc.enabled = true;
        sr.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //add a minimum cost required which is taken off the player upon breaking a window
        //make sure that the player cannot end their game by breaking windows!
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerBehaviourScript>().GetCurrentCash() > REPAIR_PENALTY)
        {
            StartCoroutine(DestroyGlass());
        }
    }
}
