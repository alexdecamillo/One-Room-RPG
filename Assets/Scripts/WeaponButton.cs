using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CompleteProject
{
	public class WeaponButton : MonoBehaviour {

		//public PlayerShooting playerShooting;
		public int weaponNumber;
		private int bombPrice = 50;

		LayBombs bombs;
		Player player;


		// Use this for initialization
		void Start () 
		{
			player = FindObjectOfType<Player>();
			bombs = FindObjectOfType<LayBombs>();
		}

		public void BuyBomb()
		{
			if (player.points >= bombPrice) {

				player.points -= bombPrice;
				++bombs.bombCount;
				Debug.Log("bought a bomb");
			} else 
			{
				//You dont have enough money
			}
		}

	}

}