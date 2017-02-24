﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CompleteProject
{
	public class WeaponButton : MonoBehaviour {

		public int bombPrice = 50;
		public int statUpgradePrice = 0;
		public int potionPrice = 0;

		public Text warning;

		LayBombs bombs;
		Player player;
		Sword sword;


		// Use this for initialization
		void Start () 
		{
			player = FindObjectOfType<Player>();
			bombs = FindObjectOfType<LayBombs>();
			sword = FindObjectOfType<Sword>();
		}

		public void BuyBomb()
		{
			if (player.points >= bombPrice) {

				player.points -= bombPrice;
				++bombs.bombCount;
				Debug.Log("bought a bomb");
			} /*else 
			{
				//warning.text = ("Not enough points!");
			}*/
		}

		public void BuyPotion()
		{
			if (player.points >= potionPrice) {

				player.points -= potionPrice;
				++player.potionCount;
				//Debug.Log("bought a bomb");
			} /*else 
			{
				//warning.text = ("Not enough points!");
			}*/
		}

		public void BuyMaxHealth()
		{
			if (player.points >= statUpgradePrice) {

				player.points -= statUpgradePrice;
				player.maxHealth += 10;
			}/* else 
			{
				//warning.text = ("Not enough points!");
			}*/
		}

		public void BuyMoveSpeed()
		{
			if (player.points >= statUpgradePrice) {

				player.points -= statUpgradePrice;
				++player.moveSpeed;
			}/* else 
			{
				//warning.text = ("Not enough points!");
			}*/
		}

		public void BuyAttackPower()
		{
			if (player.points >= statUpgradePrice) {

				player.points -= statUpgradePrice;
				++sword.damage;
			}/* else 
			{
				//warning.text = ("Not enough points!");
			}*/
		}
		
	}

}