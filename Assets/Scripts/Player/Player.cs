using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity {

	public float moveSpeed;
	public bool paused;
	

	int points = 0;

	PlayerController controller;
	Camera viewCamera;

	public event System.Action OnPause;
	public event System.Action PointChange;

	// Use this for initialization
	public virtual void Start () {
		base.Start();
		controller = GetComponent<PlayerController>();
		viewCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!paused) {
			// Movement input
			Vector3 moveInput = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
			Vector3 moveVeloctiy = moveInput.normalized * moveSpeed;
			controller.Move (moveVeloctiy);
			
			// Look input
			Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
			Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
			float rayDistance;
			
			if (groundPlane.Raycast (ray, out rayDistance)) {
				Vector3 point = ray.GetPoint (rayDistance);
				//Debug.DrawLine(ray.origin, point, Color.red);
				controller.LookAt (point);
			}
		} 

		// pause controller
		if (Input.GetKeyDown(KeyCode.Escape)) {
			paused = !paused;
			if (OnPause != null) {
				OnPause();
			}
		}
	}

	public void AddPoints(int points) {
		this.points += points;
		PointChange();
	}

	public void RemovePoints(int points) {
		this.points -= points;
		PointChange();
	}

	public int GetPoints() {
		return points;
	}
}
