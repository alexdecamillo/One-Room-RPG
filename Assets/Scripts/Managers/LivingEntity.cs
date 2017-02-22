using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, iDamagable {

	public float startingHealth;
	public int value;
	Player player;

	public float health {get; protected set;}
	protected bool dead;

	public event System.Action OnDeath;
	public event System.Action TookDamage;

	protected virtual void Start() {
		health = startingHealth;
		player = FindObjectOfType<Player> ();
	}

	public void TakeDamage(float damage) {
		health -= damage;
		if (gameObject.name == "Player" && player !=null) {
			if (TookDamage != null) {
				TookDamage ();
			}
		}

		if (health <= 0 && !dead) {
			Die();
			FindObjectOfType<Player> ().AddPoints (value);
		}
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
