using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassSelection : MonoBehaviour {

	public bool setKnight = false;
	public bool setRogue = false;
	public bool setTank = false;
	Player player;
	Sword sword;
	Enemy enemy;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player>();
		sword = FindObjectOfType<Sword>();	
		}
	

	public void SelectKnight()
	{
		player.maxHealth = 100;
		player.health = 100;
		player.moveSpeed = 2;
		sword.damage = 4;
		player.healthBar.maxValue = player.maxHealth;
		gameObject.SetActive(!gameObject.activeSelf);
	}
	public void SelectRogue()
	{

		player.maxHealth = 80;
		player.health = 80;
		player.moveSpeed = 4;
		sword.damage = 2;
		player.healthBar.maxValue = player.maxHealth;
		gameObject.SetActive(!gameObject.activeSelf);
	}
	public void SelectTank()
	{

		player.maxHealth = 150;
		player.health = 150;
		player.moveSpeed = 1;
		sword.damage = 6;
		player.healthBar.maxValue = player.maxHealth;
		gameObject.SetActive(!gameObject.activeSelf);
	}
}
