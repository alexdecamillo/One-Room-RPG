using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CompleteProject
{
	public class WeaponButton : MonoBehaviour {

		public int bombPrice;
		public int statUpgradePrice;
		public int potionPrice;

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
			if (player.points >= bombPrice && player.bombCount < 4) {
				if(player.bombCount <4)
				{
					player.points -= bombPrice;
					++player.bombCount;
					Debug.Log("bought a bomb");
				}
				else if(player.bombCount >= 4)
				{
					warning.text = ("You can't hold any more! Store some in your chest.");
				}

			} 
			else 
			{
				warning.text = ("Not enough points!");
			}
		}

		public void BuyPotion()
		{
			if (player.points >= potionPrice) 
			{

				if (player.points >= bombPrice && player.potionCount < 4) 
				{
					player.points -= potionPrice;
					++player.potionCount;
					//Debug.Log("bought a bomb");
				}
				else if(player.bombCount >= 4)
				{
					warning.text = ("You can't hold any more! Store some in your chest.");
				}
			} 
			else 
			{
				warning.text = ("Not enough points!");
			}
		}

		public void BuyMaxHealth()
		{
			if (player.points >= statUpgradePrice) {

				player.points -= statUpgradePrice;
				player.maxHealth += 10;
				player.healthBar.maxValue += 10;
			} 
			else 
			{
				warning.text = ("Not enough points!");
			}
		}

		public void BuyMoveSpeed()
		{
			if (player.points >= statUpgradePrice) {

				player.points -= statUpgradePrice;
				++player.moveSpeed;
			}
			 else 
			{
				warning.text = ("Not enough points!");
			}
		}

		public void BuyAttackPower()
		{
			if (player.points >= statUpgradePrice) {

				player.points -= statUpgradePrice;
				++sword.damage;
			}
			 else 
			{
				warning.text = ("Not enough points!");
			}
		}
		
	public void StoreBomb()
		{
			if (player.bombCount > 0) 
			{
					
					++player.chestBombCount;
					--player.bombCount;
			} 
			else 
			{
				warning.text = ("Not enough bombs!");
			}
		}

		public void TakeBomb()
		{
			if (player.bombCount < 3 && player.chestBombCount > 0) 
			{
					
					--player.chestBombCount;
					++player.bombCount;
			} 
			else if(player.bombCount == 3)
			{
				warning.text = ("You have too many bombs!");
			}
			else if (player.chestBombCount < 1 )
			{
				warning.text = ("No bombs in here!");
			}
		}

		public void StorePotion()
		{
			if (player.potionCount > 0) 
			{
					
					++player.chestPotionCount;
					--player.potionCount;
			} 
			else 
			{
				warning.text = ("Not enough potions!");
			}
		}

		public void TakePotion()
		{
			if (player.potionCount < 3 && player.chestPotionCount > 0) 
			{
					
					--player.chestPotionCount;
					++player.potionCount;
			} 
			else if (player.potionCount == 3)
			{
				warning.text = ("You have too many potions!");
			}
			else if (player.chestPotionCount < 1 )
			{
				warning.text = ("No potions in here!");
			}
		}
	

	}

}