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

	Player player;
	Transform target;
	LivingEntity targetEntity;
	public Slider healthBar;

	public virtual void Start(){
		base.Start ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		targetEntity = target.GetComponent<LivingEntity>();
		player = FindObjectOfType<Player>();

		sword = FindObjectOfType<Sword>();
	}

	void Update(){
		if (player != null) {
			MoveToPlayer ();
		}
/*		if (sword.givingDamage)
		{
			StartCoroutine (Knockback(0.02f, 5 ));
			
		}*/
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
				//healthBar.value = targetEntity.health;
			}

			percent += Time.deltaTime * attackSpeed;
			float interpolation = (-Mathf.Pow (percent, 2) + percent) * 4;
			transform.position = Vector3.Lerp (originalPosition, attackPosition, interpolation);

			yield return null;
		}
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
