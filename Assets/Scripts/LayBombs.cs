using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LayBombs : MonoBehaviour
{

	public bool x;
	[HideInInspector]
    //public AudioClip bombsAway;       // Sound for when the player lays a bomb.
	public GameObject bomb;             // Prefab of the bomb.
	SpawnManager spawnManager;
	Player player; 

	float bombTimer;                    // Time between dropped bombs
    bool bombReady = true;

	void Start ()
	{
		spawnManager = FindObjectOfType<SpawnManager>();
		player = FindObjectOfType<Player>();
	}

	void Update ()
	{
        // Bomb place cooldown
        if (bombTimer > 0)
        {
            bombReady = false;
            bombTimer -= Time.deltaTime;
        }
        else
            bombReady = true;
	}

	public void OnClickThing()
	{
		if(bombReady && player.bombCount > 0)
		{
				LayBomb();
				--player.bombCount;
		}
	}
	

	public bool LayBomb()
	{
		if(bombReady /*&& !spawnManager.dayCycle*/)
		{
			// Create new bomb
			Instantiate(bomb, transform.position, transform.rotation);
			bombTimer = 3.0f;
            return true;
		}
        return false;
        
    }



}