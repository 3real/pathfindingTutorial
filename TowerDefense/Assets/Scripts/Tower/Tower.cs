using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

[SerializeField]
private float timeBetweenAttacks;

[SerializeField]
private float attackRadius;
[SerializeField]
private Projectile projectile;

private Enemy targetEnemy = null;

private float attackCounter;

private bool isAttacking = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		attackCounter -= Time.deltaTime;

		if (targetEnemy == null || targetEnemy.IsDead) {
			Enemy nearestEnemy = GetNearestEnemyInRange();
			if (nearestEnemy != null && Vector2.Distance(transform.localPosition, nearestEnemy.transform.localPosition) <= attackRadius){
				targetEnemy = nearestEnemy;
			} 
		}
		else {
			if(attackCounter <= 0){
				isAttacking = true;
				// reset attackCounter.
				attackCounter = timeBetweenAttacks;
			}
			else{
				isAttacking = false;
			}
			if (Vector2.Distance(transform.localPosition, targetEnemy.transform.localPosition) > attackRadius){
			targetEnemy = null;
		}
		}

		
	}

	void FixedUpdate(){
		if (isAttacking){
			Attack();
		}
	}

	public void Attack(){
		isAttacking = false;
		Projectile newProjectile = Instantiate(projectile) as Projectile;
		newProjectile.transform.localPosition = transform.localPosition;
		if (targetEnemy == null){
			Destroy(newProjectile);
		}
		else{
			//move projectile to enemy.
			StartCoroutine(MoveProjectile(newProjectile));
		}
	}

	IEnumerator MoveProjectile(Projectile projectile){
		while (getTargetDistance(targetEnemy) > 0.2f && projectile != null  && targetEnemy != null) {
			var dir = targetEnemy.transform.localPosition - transform.localPosition;
			var angleDirection = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
			projectile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
			projectile.transform.localPosition = Vector2.MoveTowards(projectile.transform.localPosition, targetEnemy.transform.localPosition, 5f * Time.deltaTime);
			yield return null;
		}
		if (projectile != null || targetEnemy == null){
			Destroy(projectile);
		}
	}

	private float getTargetDistance(Enemy thisEnemy){
		if (thisEnemy == null){
			thisEnemy = GetNearestEnemyInRange();
			if (thisEnemy == null){
				return 0f;
			}
		}
		return Mathf.Abs(Vector2.Distance(transform.localPosition, thisEnemy.transform.localPosition));
	}

	private List<Enemy> GetEnemiesInRange(){
		List<Enemy> enemiesInRange = new List<Enemy>();
		foreach(Enemy enemy in GameManager.Instance.EnemyList){
			//3reals code added, use the IsDead to discount dead enemies from the list of targets.
			if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= attackRadius && enemy.IsDead == false){
				enemiesInRange.Add(enemy);
			}

		}
		return enemiesInRange;
	}
	private Enemy GetNearestEnemyInRange(){
		Enemy nearestEnemy = null;
		float smallestDistance = float.PositiveInfinity;
		foreach(Enemy enemy in GetEnemiesInRange()){
			if(Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <smallestDistance){
				smallestDistance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
				nearestEnemy = enemy;
			}
		}
		return nearestEnemy;
	}
}
