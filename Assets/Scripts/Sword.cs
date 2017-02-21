using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	bool attacking = false;
	public bool givingDamage = false;
	Vector3 velocity;

	public float damage;

	public float swingTimer;
	public float swingCD;

	Enemy enemy;
	Transform target;

	BoxCollider hitBox;

	void Start () {
		enemy = FindObjectOfType<Enemy>();
		hitBox = GetComponent<BoxCollider>();
		hitBox.enabled = false;
		target = GameObject.FindGameObjectWithTag("Enemy").transform;
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
				givingDamage = false;
			}
		}

	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Enemy") {
			col.SendMessage("TakeDamage", damage);
			givingDamage = true;
		}
	}

}

