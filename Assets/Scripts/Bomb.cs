using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float damage;
    private float explodeBomb;
    public float explosionTime = 0.2f;

    Animator anim;

    void Start () {
        explodeBomb = 2.0f;
        anim = GetComponent<Animator>();
        Destroy(gameObject, explodeBomb);
	}
    
	void Update () {
        explodeBomb -= Time.deltaTime;
        // Animation for waiting to explode; flashing red or something
        if (explodeBomb <= 0.15f)
        {
            anim.SetBool("Explode", true);
            // Sound for exploding
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
