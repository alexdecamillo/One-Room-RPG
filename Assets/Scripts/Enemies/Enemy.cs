using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : LivingEntity {

	public enum State {Chasing, Attacking};
	State currentState;
	public float speed;
	public Sword sword;

	float timeToUpdate = 0;
	float updateTime = .1f;
	float damage = 10;
	float attackDistanceThreshold = 1.0f;
	float chaseDistanceThreshold = 20.0f;
	float timeBetweenAttacks = 1;
	float nextAttackTime;
	public float knockPwr;

	bool stunned = false;
	float knockTime = .1f;
	float knockTimer;
	Player player;
	Transform target;
	LivingEntity targetEntity;

	public virtual void Start(){
		base.Start ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		targetEntity = target.GetComponent<LivingEntity>();
		player = FindObjectOfType<Player>();
		sword = FindObjectOfType<Sword>();
	}

	void Update(){
		if (player != null && !stunned) {
			MoveToPlayer ();
		}

		if(stunned){
			if (knockTimer > 0) {
				knockTimer -= Time.deltaTime;
			}
			else {
				stunned = false;
			}	
		}
	}

	void MoveToPlayer(){
		float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
		float chaseDist = Mathf.Pow (chaseDistanceThreshold, 2);
		float attDist = Mathf.Pow (attackDistanceThreshold, 2);

		if(attDist <= sqrDstToTarget && sqrDstToTarget <= chaseDist)
		{
			currentState = State.Chasing;
		}

		if (Time.time > nextAttackTime) 
		{

			if (sqrDstToTarget < attDist) 
			{
				currentState = State.Attacking;
				nextAttackTime = Time.time + timeBetweenAttacks;
				StartCoroutine (Attack ());
			}
		}

		if (currentState == State.Chasing) {
			if (Time.time > timeToUpdate) {
				transform.LookAt (target.transform);
				transform.Rotate (new Vector3 (0, -90, 0), Space.Self);
				timeToUpdate = Time.time + updateTime;
			}
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
		}
	}

	IEnumerator Attack() 
	{
		bool hasAppliedDamage = false;
		Vector3 originalPosition = transform.position;
		Vector3 attackPosition = target.position;

		float attackSpeed = 3;
		float percent = 0;

		while(percent <= 1)
		{
			if(percent >= .5f && !hasAppliedDamage)
			{
				hasAppliedDamage = true;
				targetEntity.TakeDamage(damage);
			}

			percent += Time.deltaTime * attackSpeed;
			float interpolation = (-Mathf.Pow (percent, 2) + percent) * 4;
			transform.position = Vector3.Lerp (originalPosition, attackPosition, interpolation);

			yield return null;
		}
	}

	public void Knockback(){
		StartCoroutine (Knockback(1f, knockPwr /*.02f*/));
		stunned = true;
		knockTimer = knockTime;
	}

	public IEnumerator Knockback(float knockDur, float knockbackPwr)
	{
		float timer = 0;

		while (knockDur > timer) 
		{
			timer += Time.deltaTime;
			transform.Translate (new Vector3 (-knockbackPwr, 0, 0));
		}
			
		yield return 0;
	}
	void OnTriggerEnter(Collider col) {
		if (col.tag == "Bomb") {
			SendMessage("TakeDamage", damage);
			//SendMessage ("Knockback");
		}
	}
}
