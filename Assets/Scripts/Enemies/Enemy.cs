using UnityEngine;
using System.Collections;

public class Enemy : LivingEntity {

	public float speed;
	public Sword sword;

	float timeToUpdate = 0;
	float updateTime = .1f;
	Transform target;

	public virtual void Start(){
		base.Start ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		sword = FindObjectOfType<Sword>();
	}

	void Update(){
		MoveToPlayer ();
		if (sword.givingDamage)
		{
			StartCoroutine (Knockback(0.02f, 5 ));
			
		}
	}

	void MoveToPlayer(){
		if(Time.time > timeToUpdate){
			transform.LookAt (target.transform);
			transform.Rotate (new Vector3 (0, -90, 0), Space.Self);
			timeToUpdate = Time.time + updateTime;
		}
		transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
	}

	public IEnumerator Knockback(float knockDur, float knockbackPwr)
	{
		float timer = 0;

		while (knockDur > timer) 
		{
			timer += Time.deltaTime;
		Debug.Log("Damage");
				transform.Translate (new Vector3 (-.05f, 0, 0));
			

			//player.velocity.y = knockbackDir.y + knockbackPwr;
			//player.velocity.z = target.transform.position.z;

		}

		yield return 0;
	}
}
