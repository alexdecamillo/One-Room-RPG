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
	public float explosionTime = 0.2f;
	public bool bombReady = true;
	bool attacking = false;
	public bool givingBombDamage = false;

	public float damage;

	private GameObject instantiatedObj;

	SphereCollider hitBox;
	Vector3 velocity;
	Transform target;
	Enemy enemy;
	public SpawnManager spawnManager;
	//Transform targetEn;

	public Text bombHUD;         // Heads up display of whether the player has a bomb or not.


	void Start ()
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		enemy = FindObjectOfType<Enemy>();
		instantiatedObj.GetComponent<SphereCollider>().enabled = false;
		hitBox = instantiatedObj.GetComponent<SphereCollider>();
		spawnManager = FindObjectOfType<SpawnManager>();
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
		
		if(Input.GetButtonDown("Fire2"))
		{
			LayBomb();
		}

		if (bombLaid == true) {

			explodeBomb -= Time.deltaTime;

			if (explodeBomb <= 0.5f) {
				BombExplosion ();
				if (explodeBomb <= 0.0f){
				bombLaid = false;
				Destroy(instantiatedObj, float.MinValue);
			}
			}



		}

		// The bomb heads up display should be enabled if the player has bombs, other it should be disabled.
		bombHUD.text = bombCount + "";
	}

	void BombExplosion()
	{
			instantiatedObj.GetComponent<SphereCollider>().enabled = true;
			Debug.Log("Entered Function");
	}

	public void LayBomb()
	{
		if(bombCount > 0 && bombReady == true && spawnManager.dayCycle == false)
		{
			
			// Decrement the number of bombs.
			bombCount--;

			// Instantiate the bomb prefab.
			instantiatedObj = Instantiate(bomb, target.transform.position, target.transform.rotation);
	

			bombTimer = 5.0f;
			bombLaid = true;
			explodeBomb = 3.5f;

		}
		
	
}
}