using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerController))]
public class Player : LivingEntity {

	bool attacking = false;
	public float swingTimer;
	public float swingCD;
	public Text activeBed;

	public float potionStrength = 10f;
	public float moveSpeed;
	public bool paused;
	bool crossBoundary = false;
	bool crossShopBoundary = false;
	bool inShop = false;
	public int points = 0;
	int direction = 0;
	public int potionCount = 0;

	Sword sword;
	PlayerController controller;
	Player player;	
	Animator anim;
	SpawnManager spawner;

	public Light dayLight;
	public Canvas Shop;
	public GameObject plane;
	public GameObject cycle;
	public Text healthHUD; 
	public Slider healthBar;

	public event System.Action OnPause;
	public event System.Action PointChange;

	// Use this for initialization
	public virtual void Start () {
		base.Start();
		cycle.SetActive (false);
		controller = GetComponent<PlayerController>();
		anim = GetComponent<Animator> ();
		spawner = FindObjectOfType<SpawnManager>();
		dayLight = FindObjectOfType<Light>();
		sword = GetComponentInChildren <Sword> ();
		Shop.enabled = false;
        sword.swingUpdate += Sword_swing;
	}

    private void Sword_swing()
    {
        
    }

    // Update is called once per frame
    void Update () {
        if (!paused) {
            // Movement input
            Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            Vector3 moveVeloctiy = moveInput.normalized * moveSpeed;
            controller.Move(moveVeloctiy);

            if (crossBoundary == true && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E pressed");
                spawner.dayCycle = false;
                cycle.SetActive(true);
            }

            if (inShop == true && crossShopBoundary == false)
            {
                Shop.enabled = false;
            }

            if (crossShopBoundary == true && inShop == false && Input.GetKeyDown(KeyCode.E)) {
                inShop = true;
                Shop.enabled = true;
                Debug.Log("Shop pressed");
            }
            else if (inShop == true && Input.GetKeyDown(KeyCode.E))
            {
                Shop.enabled = false;
                inShop = false;
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
            {
				OnPause();
			}
		}

		healthHUD.text = potionCount + "";
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

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Bed" && spawner.dayCycle == true)
        {
			activeBed.text = ("Press E to go to sleep");
			crossBoundary = true;
		}
		if (col.tag == "Shop" && spawner.dayCycle == true && inShop == false)
        {
			activeBed.text = ("Press E to shop");
			crossShopBoundary = true;
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
	}

	public void Potion()
	{
        if (potionCount > 0 && health != maxHealth)
        {
            health += potionStrength;
            --potionCount;
        }
    }

}