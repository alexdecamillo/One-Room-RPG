using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Enemy : LivingEntity {

	public enum State {Chasing, Attacking};
	State currentState;

    public float speed;
	public float damage;
	public float knockPwr;

	float timeToUpdate = 0;
	float updateTime = .1f;
    Vector3 velocity;

    float attackDistanceThreshold = 1f;
    float attDist;
	float timeBetweenAttacks = 1;
	float nextAttackTime;

    bool stunned = false;
	float knockTime = .1f;
	float knockTimer;

	LivingEntity targetEntity;
	Transform target;

    Animator anim;

	public virtual void Start(){
		base.Start ();
        targetEntity = FindObjectOfType<Player>();
        target = targetEntity.transform;
        anim = GetComponent<Animator>();
        attDist = Mathf.Pow (attackDistanceThreshold, 2);
        Physics.IgnoreCollision(FindObjectOfType<Player>().GetComponent<BoxCollider>(), GetComponent<BoxCollider>());
    }

	void Update(){
        // If enemy gets hit, wait knockTime until moving again
		if (targetEntity != null && !stunned)
			MoveToPlayer ();

		if(stunned)
        {
			if (knockTimer > 0)
				knockTimer -= Time.deltaTime;
			else
				stunned = false;
		}
	}

	void MoveToPlayer(){
        // Finds distance to the target
		float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;

        // Sets state based on attack threshold
        if (attDist <= sqrDstToTarget)
		{
			currentState = State.Chasing;
            anim.SetBool("attacking", false);
		}
        else
        {
            currentState = State.Attacking;
        }

        // Attacks or chases based on state
        if (currentState == State.Attacking)
        {
            if (Time.time > nextAttackTime)
            {
                anim.SetBool("attacking", true);
                nextAttackTime = Time.time + timeBetweenAttacks;
                targetEntity.TakeDamage(damage);
                //StartCoroutine(Attack()); 
            }
            else
                anim.SetBool("attacking", false);
        }
        else if (currentState == State.Chasing)
        {
            if (Time.time > timeToUpdate)
            {
                velocity = new Vector3((target.position.x - transform.position.x) * speed, 0, (target.position.z - transform.position.z) * speed);
                timeToUpdate = Time.time + updateTime;
			}
            GetComponent<Rigidbody>().velocity = velocity;
        }

        // Finds direction that the enemy is moving for animator
        int dir;
        if (Mathf.Abs(velocity.z) > Mathf.Abs(velocity.x))
        {
            if (velocity.z > 0)
                dir = 1;
            else
                dir = 0;
        }
        else
        {
            if (velocity.x > 0)
                dir = 3;
            else
                dir = 2;
        }
        anim.SetInteger("direction", dir);
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
    
}
