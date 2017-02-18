using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	Vector3 velocity;
	Rigidbody myRigidbody;

	void Start () {
		myRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		myRigidbody.MovePosition(myRigidbody.position + velocity * Time.deltaTime);
	}

	public void Move(Vector3 _velocity) {
		velocity = _velocity;
	}

	public void LookAt(Vector3 lookPoint) {
		Vector3 correctedPoint = new Vector3(lookPoint.x - .8f, transform.position.y, lookPoint.z);
		transform.LookAt(correctedPoint);
	}
}
