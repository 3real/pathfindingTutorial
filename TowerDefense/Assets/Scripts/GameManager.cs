using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public GameObject spawnPoint;
	public GameObject[] enemies;
	public int maxEnemiesOnScreen;
	public int totalEnemies;
	public int enemiesPerSpawn;

	private int enemiesOnScreen = 0;


	void Awake () {
		if (instance == null)
		instance = this;
		else if (instance != this)
		Destroy (gameObject);

		DontDestroyOnLoad(gameObject);
	}
	// Use this for initialization
	void Start () {
		SpawnEnemy();
	}
	
	// Update is called once per frame
	void SpawnEnemy () {
		if(enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies){
			for(int i = 0; i < enemiesPerSpawn; i++){
				if(enemiesOnScreen < maxEnemiesOnScreen){
					GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
					newEnemy.transform.position = spawnPoint.transform.position;
					enemiesOnScreen += 1;
				}
			}
		}

		
	}

}