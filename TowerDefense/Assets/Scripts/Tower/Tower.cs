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

	private List<Enemy> GetEnemiesInRange(){
		List<Enemy> enemiesInRange = new List<Enemy>();
		foreach(Enemy enemy in GameManager.Instance.EnemyList){
			if (Vector2.Distance(transform.position, enemy.transform.position) <= attackRadius){
				enemiesInRange.Add(enemy);
			}

		}
		return enemiesInRange;
	}
	private Enemy GetNearestEnemyInRange(){
		Enemy nearestEnemy = null;
		float smallestDistance = float.PositiveInfinity;
		foreach(Enemy enemy in GetEnemiesInRange()){
			if(Vector2.Distance(transform.position, enemy.transform.position) <smallestDistance){
				smallestDistance = Vector2.Distance(transform.position, enemy.transform.position);
				nearestEnemy = enemy;
			}
		}
		return nearestEnemy;
	}
}
