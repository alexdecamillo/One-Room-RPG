using UnityEngine;
using System.Collections;

public class Enemy : LivingEntity {

	public float speed;

	float timeToUpdate = 0;
	float updateTime = .1f;
	Transform target;

	public virtual void Start(){
		base.Start ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Update(){
		MoveToPlayer ();
	}

	void MoveToPlayer(){
		if(Time.time > timeToUpdate){
			transform.LookAt (target.transform);
			transform.Rotate (new Vector3 (0, -90, 0), Space.Self);
			timeToUpdate = Time.time + updateTime;
		}
		transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
	}
}
