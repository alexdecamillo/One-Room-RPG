using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Vector3 velocity;
    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

	void FixedUpdate() {
        myRigidbody.MovePosition(myRigidbody.position + velocity * Time.deltaTime);
        //transform.position = transform.position + velocity * Time.deltaTime;
	}

	public void Move(Vector3 _velocity) {
		velocity = _velocity;
	}

}
