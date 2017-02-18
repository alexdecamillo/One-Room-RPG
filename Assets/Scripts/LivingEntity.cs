using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, iDamagable {

	public float startingHealth;

	protected float health;
	protected bool dead;

	public event System.Action OnDeath;
	public event System.Action TookDamage;

	protected virtual void Start() {
		health = startingHealth;
	}

	public void TakeDamage(float damage) {
		health -= damage;
		if (gameObject.name == "Player") {
				TookDamage();
		}

		if (health <= 0 && !dead) {
			Die();
		}
	}

	public void TakeHit(float damage) {
		TakeDamage(damage);
	}

	[ContextMenu("Self Destuct")]
	protected void Die() {
		dead = true;
		if(OnDeath != null) {
			OnDeath();
		}
		GameObject.Destroy(gameObject);
	}
}
