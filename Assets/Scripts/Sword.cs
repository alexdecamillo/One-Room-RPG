﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	bool attacking = false;

	public float damage;

	public float swingTimer;
	public float swingCD;

	BoxCollider hitBox;

	void Start () {
		hitBox = GetComponent<BoxCollider>();
		hitBox.enabled = false;
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
			col.SendMessage("TakeDamage", damage);
		}
	}
}