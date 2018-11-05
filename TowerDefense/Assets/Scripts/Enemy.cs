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

	private int target = 0;
	private Transform enemy;
	private float navigationTime = 0;

	void Start () {
		enemy = GetComponent<Transform>(); 
	}
	
	// Update is called once per frame
	void Update () {
		if (wayPoints!= null){
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
			GameManager.Instance.RemoveEnemyFromScreen();
			Destroy (gameObject);
		}
	}
}

