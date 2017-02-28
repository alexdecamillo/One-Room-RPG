using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bomb : MonoBehaviour {

    public float damage;
    public float explodeBomb;
    public float explosionTime = 0.2f;

    public AudioClip clip;
    AudioSource bombSound;
    Animator anim;

    void Start () {
        explodeBomb = 2.0f;
        anim = GetComponent<Animator>();
        Destroy(gameObject, explodeBomb);
        bombSound = GetComponent<AudioSource>();
	}
    
	void Update () {
        explodeBomb -= Time.deltaTime;
        // Animation for waiting to explode; flashing red or something
        if (explodeBomb <= 0.15f)
        {
            anim.SetBool("Explode", true);
            if(!bombSound.isPlaying)
 {
            bombSound.PlayOneShot(clip, .7f);
        }
            GetComponent<SphereCollider>().enabled = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.SendMessage("TakeDamage", damage);
        }
    }
}
