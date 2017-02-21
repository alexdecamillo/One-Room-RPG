using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerController))]
public class Player : LivingEntity {

	bool attacking = false;
	public float swingTimer;
	public float swingCD;

	public float moveSpeed;
	public bool paused;
	

	int points = 0;
	int direction;

	PlayerController controller;
	Camera viewCamera;
	Animator anim;

	public event System.Action OnPause;
	public event System.Action PointChange;

	// Use this for initialization
	public virtual void Start () {
		base.Start();
		controller = GetComponent<PlayerController>();
		viewCamera = Camera.main;
		anim = GetComponent<Animator> ();
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
				//controller.LookAt (point);
			}

			if (Input.GetButtonDown("Fire1") && !attacking) {
				attacking = true;
				swingTimer = swingCD;
			}

			if (attacking) {
				if (swingTimer > 0) {
					swingTimer -= Time.deltaTime;
				}
				else {
					attacking = false;
				}
			}

			// handle animations
			if (moveInput.z < 0)
				direction = 0;
			else if (moveInput.z > 0)
				direction = 1;

			if (moveInput.x < 0)
				direction = 2;
			else if (moveInput.x > 0)
				direction = 3;

			anim.SetFloat ("speed", moveInput.magnitude);
			anim.SetInteger ("direction", direction);
			anim.SetBool ("attacking", attacking);
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
