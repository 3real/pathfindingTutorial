using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	
	[SerializeField]
	private Transform exitPoint;
	[SerializeField]
	private Transform[] wayPoints;
	[SerializeField]
	private float navigationUpdate;

	[SerializeField]
	private int healthPoints;

	private int target = 0;
	private Transform enemy;
	private Collider2D enemyCollider;
	private Animator anim;
	private float navigationTime = 0;

	private bool isDead = false;

	public bool IsDead{
		get{
			return isDead;
		}
	}

	void Start () {
		enemy = GetComponent<Transform>(); 
		enemyCollider = GetComponent<Collider2D>();
		anim = GetComponent<Animator>();
		GameManager.Instance.RegisterEnemy(this);
	}
	
	// Update is called once per frame
	void Update () {
		if (wayPoints!= null && !isDead){
			navigationTime += Time.deltaTime;
			if (navigationTime > navigationUpdate){
				if (target < wayPoints.Length){
					enemy.position = Vector2.MoveTowards (enemy.position, wayPoints[target].position, navigationTime);}
				else{
					enemy.position = Vector2.MoveTowards (enemy.position, exitPoint.position, navigationTime);
				}

				navigationTime = 0;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Checkpoint")
			target += 1;
		else if (other.tag == "Finish") {
			GameManager.Instance.UnregisterEnemy(this);
		}
		else if (other.tag == "projectile"){
			Projectile newP = other.gameObject.GetComponent<Projectile>();
			enemyHit(newP.AttackStrength);
			Destroy(other.gameObject);
		}
	}
	public void enemyHit(int hitPoints){
		if (healthPoints - hitPoints >0){
		healthPoints -= hitPoints;
		anim.Play("hurt");
		}
		else{
			anim.SetTrigger("didDie");
			die();
		}
	}

	public void die(){
		isDead = true;
		enemyCollider.enabled = false;
	}
}

