using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(AudioSource))]
public class Player : LivingEntity {

	bool attacking = false;
	public float swingTimer;
	public float swingCD;
	public Text activeBed;
	public Text healthHUD;
	public Text bombHud;
	public Text chestInfo;

	public float potionStrength = 10f;
	public float moveSpeed;
	public bool paused;
	bool crossBoundary = false;
	bool crossShopBoundary = false;
	bool crossChestBoundary = false;
	bool inShop = false;
	bool inChest = false;

	public int points = 0;
	int direction = 0;
	public int potionCount = 0;
    public int bombCount = 0;
    public int chestPotionCount = 0;
    public int chestBombCount = 0;

	Sword sword;
	PlayerController controller;
	Player player;	
	Animator anim;
	SpawnManager spawner;
	AudioSource potionDrink;

	public Canvas Shop;
	public Canvas Chest;
	public GameObject plane;
	public GameObject cycle;
	 
	public Slider healthBar;

	public event System.Action OnPause;
	public event System.Action PointChange;

	// Use this for initialization
	public virtual void Start () {
		base.Start();
		Shop.enabled = false;
		Chest.enabled = false;
		controller = GetComponent<PlayerController>();
		anim = GetComponent<Animator> ();
		spawner = FindObjectOfType<SpawnManager>();
		sword = GetComponentInChildren <Sword> ();
		potionDrink = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update () {
        if (!paused) {
            // Movement input
            Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            Vector3 moveVeloctiy = moveInput.normalized * moveSpeed;
            controller.Move(moveVeloctiy);

            // Bomb laying
            if (Input.GetButtonDown("Fire2"))
            {
                // Check if there is a bomb and one can be laid
                if ((bombCount > 0) && GetComponentInChildren<LayBombs>().LayBomb())
                    bombCount--;
                else
                { }// Clunk sound??
            }

            if (crossBoundary && Input.GetKeyDown(KeyCode.E))
            {
                spawner.dayCycle = false;
                FindObjectOfType<DayCycle>().SetNight();
            }

            if (inShop && !crossShopBoundary)
            {
                Shop.enabled = false;
            }

            if (crossShopBoundary && !inShop && Input.GetKeyDown(KeyCode.E)) {
                inShop = true;
                Shop.enabled = true;
                Debug.Log("Shop pressed");
            }
            else if (inShop && Input.GetKeyDown(KeyCode.E))
            {
                Shop.enabled = false;
                inShop = false;
            }

            if (inChest && !crossChestBoundary)
            {
                Chest.enabled = false;
            }

            if (crossChestBoundary && !inChest && Input.GetKeyDown(KeyCode.E)) {
                inChest = true;
                Chest.enabled = true;
                Time.timeScale = 0;
                Debug.Log("Shop pressed");
            }
            else if (inChest && Input.GetKeyDown(KeyCode.E))
            {
                Chest.enabled = false;
                inChest = false;
                Time.timeScale = 1;
            }

            // handle sword and player direction
            if (moveInput.z < 0)        // foward
            {
                direction = 0;
                sword.transform.localPosition = new Vector3(0, -0.6f, 0);
            }
            else if (moveInput.z > 0)   // back
            { 
                direction = 1;
                sword.transform.localPosition = new Vector3(0, 0.6f, 0);
            }

            if (moveInput.x < 0)        // left
            {
                direction = 2;
                sword.transform.localPosition = new Vector3(-0.6f, 0, 0);
            }
            else if (moveInput.x > 0)   // right
            {
                direction = 3;
                sword.transform.localPosition = new Vector3(0.6f, 0, 0);
            }

            // animations
            anim.SetFloat ("speed", moveInput.magnitude);
			anim.SetInteger ("direction", direction);
            anim.SetBool("attacking", sword.attacking);
        }

		if(Input.GetKeyDown(KeyCode.Alpha1))
            Potion();

		if (health > maxHealth)
            health = maxHealth;

		// pause controller
		if (Input.GetKeyDown(KeyCode.Escape))
        {
			paused = !paused;
			if (OnPause != null)
				OnPause();
		}

		chestInfo.text = "Bombs: " + chestBombCount + " Potions: " + chestPotionCount;
		healthHUD.text = potionCount + "";
		bombHud.text = bombCount + "";
		healthBar.maxValue = maxHealth;
		healthBar.value = health;
	}

	public void AddPoints(int points) {
		this.points += points;
		PointChange();
	}

	public void RemovePoints(int points) {
		this.points -= points;
		PointChange();
	}

	public int GetPoints() {
		return points;
	}

	public void Potion()
	{
        if ((potionCount > 0) && (health != maxHealth))
        {
            health += potionStrength;
            --potionCount;
            potionDrink.Play();
        }
    }

	void OnTriggerEnter(Collider col) {
		if ((col.tag == "Bed") && spawner.dayCycle)
        {
			activeBed.text = ("Press E to go to sleep.");
			crossBoundary = true;
		}
		if ((col.tag == "Shop") && spawner.dayCycle && !inShop)
        {
			activeBed.text = ("Press E to shop.");
			crossShopBoundary = true;
		}
		if ((col.tag == "Chest") && !inChest)
        {
			activeBed.text = ("Press E to view chest.");
			crossChestBoundary = true;
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.tag == "Bed")
        {
			activeBed.text = ("");
			crossBoundary = false;
		}
		if (col.tag == "Shop")
        {
			activeBed.text = ("");
			crossShopBoundary = false;
		}
		if (col.tag == "Chest")
        {
			activeBed.text = ("");
			crossChestBoundary = false;
		}
	}

}