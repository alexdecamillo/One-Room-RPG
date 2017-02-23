using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LayBombs : MonoBehaviour
{
	[HideInInspector]
	public bool bombLaid = false;       // Whether or not a bomb has currently been laid.
	public int bombCount = 0;           // How many bombs the player has.
	//public AudioClip bombsAway;       // Sound for when the player lays a bomb.
	public GameObject bomb;             // Prefab of the bomb.
	public float bombTimer = 0.0f; 						// Time between dropped bombs
	public float explodeBomb = 0.0f;
	public bool bombReady = true;
	bool attacking = false;
	public bool givingBombDamage = false;

	public float damage;
	public float swingTimer;
	public float swingCD;

	private GameObject instantiatedObj;

	SphereCollider hitBox;
	Vector3 velocity;
	Transform target;
	Enemy enemy;
	//Transform targetEn;

	public Text bombHUD;         // Heads up display of whether the player has a bomb or not.


	void Start ()
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		enemy = FindObjectOfType<Enemy>();
		hitBox = bomb.GetComponent<SphereCollider>();
		hitBox.enabled = false;
//		targetEn = GameObject.FindGameObjectWithTag("Enemy").transform;
		// Setting up the reference.
	}


	void Update ()
	{
		//Sets bomb to ready

		if (bombTimer > 0) {
			bombReady = false;
		} 
		if (bombTimer <= 0){
			bombReady = true;
		}
		if (bombTimer > 0 && bombReady == false) {
			
			bombTimer -= Time.deltaTime;
		}
		// If the bomb laying button is pressed, the bomb hasn't been laid and there's a bomb to lay...
		if(Input.GetButtonDown("Fire2") && bombCount > 0 && bombReady == true)
		{
			// Decrement the number of bombs.
			bombCount--;

			// Play the bomb laying sound.
			//AudioSource.PlayClipAtPoint(bombsAway,transform.position);

			// Instantiate the bomb prefab.
			instantiatedObj = Instantiate(bomb, target.transform.position, target.transform.rotation);

			bombTimer = 5.0f;
			bombLaid = true;
			explodeBomb = 3.0f;

		}

		if (bombLaid == true) {

			explodeBomb -= Time.deltaTime;

			if (explodeBomb <= 0) {
				BombExplosion ();
				bombLaid = false;
				Destroy(instantiatedObj, float.MinValue);
			}



		}


	

		// The bomb heads up display should be enabled if the player has bombs, other it should be disabled.
		bombHUD.text = bombCount + (" Bombs");
	}

	void BombExplosion()
	{
			hitBox.enabled = true;
			Debug.Log("Entered Function");
	}
		
	void OnTriggerEnter(Collider col) {
		if (col.tag == "Enemy") {
			col.SendMessage("TakeDamage", damage);
			givingBombDamage = true;
			Debug.Log("Enemy Took Damage");
		}
	}
}