using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponObject : ScriptableObject {

	public string weaponName = "Weapon Name Here";
	public int cost = 50;
	public string description;

	public float dropRate = .5f;
	public int damage = 10;
	public float range = 100;
}
