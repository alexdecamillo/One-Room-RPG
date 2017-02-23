using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerController))]
public class Player : LivingEntity {

	bool attacking = false;
	public float swingTimer;
	public float swingCD;
	public Text activeBed;

	public float moveSpeed;
	public bool paused;
	bool crossBoundary = false;
	int points = 0;
	int direction = 0;

	Sword sword;
	PlayerController controller;
	Player player;	
	Camera viewCamera;
	Animator anim;
	SpawnManager spawner;
	public Light dayLight;

	public event System.Action OnPause;
	public event System.Action PointChange;

	// Use this for initialization
	public virtual void Start () {
		base.Start();
		controller = GetComponent<PlayerController>();
		player = FindObjectOfType<Player>();
		viewCamera = Camera.main;
		anim = GetComponent<Animator> ();
		spawner = FindObjectOfType<SpawnManager>();
		dayLight = FindObjectOfType<Light>();
		sword = GetComponentInChildren <Sword> ();
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

			/*
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
			*/

			if (crossBoundary == true && Input.GetKeyDown (KeyCode.E)) {
				Debug.Log ("E pressed");
				spawner.dayCycle = false;
				dayLight.enabled = false;

			}

			// handle animations
			if (moveInput.z < 0)
				direction = 0;	// foward
			else if (moveInput.z > 0)
				direction = 1;	// back

			if (moveInput.x < 0)
				direction = 2;	// left
			else if (moveInput.x > 0)
				direction = 3;	// right

			Debug.Log (direction);
			anim.SetFloat ("speed", moveInput.magnitude);
			anim.SetInteger ("direction", direction);
			anim.SetBool ("attacking", sword.attacking);
			sword.transform.localPosition.Set (0, 0, 0);
			/*
			if (direction == 0){
				sword.transform.position.Set (0,-.7f,0);
				sword.transform.localRotation.Set (0,0,0,0);
			} else if (direction == 1){
				sword.transform.position.Set (0,.7f,0);
				sword.transform.localRotation.Set (0,0,0,0);
			} else if (direction == 2){
				sword.transform.position.Set (-.7f,0,0);
				sword.transform.localRotation.Set (0,0,90,0);
			} else {
				sword.transform.position.Set (.7f,0,0);
				sword.transform.localRotation.Set (0,0,90,0);
			}
			*/
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

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Bed" && spawner.dayCycle == true) {
			activeBed.text = ("Press E to go to sleep");
			crossBoundary = true;
		}
		if (col.tag == "Shop" && spawner.dayCycle == true) {
			activeBed.text = ("Press E to shop");
			//crossBoundary = true;
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.tag == "Bed") {
			activeBed.text = ("");
			crossBoundary = false;
		}
		if (col.tag == "Shop") {
			activeBed.text = ("");
			//crossBoundary = false;
		}
	}
}