using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sword : MonoBehaviour {

    public event System.Action swingUpdate;

	public bool attacking = false;

	public float damage;

	AudioSource hit;
	public float swingCD;
	float swingTimer;

	BoxCollider hitBox;

	void Start () {
		hitBox = GetComponent<BoxCollider>();
		hitBox.enabled = false;
		hit = GetComponent<AudioSource>();
	}

	void Update() {

		if (Input.GetButtonDown("Fire1") && !attacking) {
			attacking = true;
			swingTimer = swingCD;
			hitBox.enabled = true;
		}

		if (attacking) {
			if (swingTimer > 0) {
				swingTimer -= Time.deltaTime;
			}
			else {
				attacking = false;
				hitBox.enabled = false;
			}
		}

	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Enemy") {
			Debug.Log ("Attacking enemy");
			col.SendMessage ("TakeDamage", damage);
			col.SendMessage ("Knockback");
			hit.Play();
		}
	}

}

