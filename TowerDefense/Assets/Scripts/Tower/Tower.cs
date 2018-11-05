using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

[SerializeField]
private float timeBetweenAttacks;

[SerializeField]
private float attackRadius;

private Projectile projectile;

private Enemy targetEnemy = null;

private float attackCounter;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//private Enemy GetNearestEnemyInRange(){

	//}
}
