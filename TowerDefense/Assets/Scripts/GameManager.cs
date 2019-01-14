﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum gameStatus {
	next, play, gameover, win
} 
public class GameManager : Singleton<GameManager> {
	[SerializeField]
	private int totalWaves = 10;
	[SerializeField]
	private Text totalMoneyLbl;
	[SerializeField]
	private Text currentWaveLbl;
	[SerializeField]
	private Text playBtnLbl;
	[SerializeField]
	private Button playBtn;
	[SerializeField]
	private Text totalEscapedLbl;
	[SerializeField]
	private GameObject spawnPoint;
	[SerializeField]
	private GameObject[] enemies;
	// maxEnemiesOnScreen is obsolette as we used a different method to iterate through waves.
	//[SerializeField]
	//private int maxEnemiesOnScreen;
	[SerializeField]
	private int totalEnemies = 3;
	[SerializeField]
	private int enemiesPerSpawn;
	[SerializeField]
	
	private int waveNumber = 0;

	private int totalMoney = 10;

	private int totalEscaped = 0;

	private int roundEscaped = 0;

	private int totalKilled = 0;

	private int whichEnemiesToSpawn = 0;

	private int tooManyEscapes = 10;

	private gameStatus currentState = gameStatus.play;

	public List<Enemy> EnemyList = new List<Enemy>();

	public int TotalMoney {
		get{
			return totalMoney;
		}
		set{
			totalMoney = value;
			totalMoneyLbl.text = totalMoney.ToString();
		}
	}

	public int TotalEscaped {
		get{
			return totalEscaped;
		}
		set{
			totalEscaped = value;
		}
	}

	public int RoundEscaped {
		get{
			return roundEscaped;
		}
		set{
			roundEscaped = value;
		}
	}

	public int TotalKilled {
		get{
			return totalKilled;
		}
		set{
			totalKilled = value;
		}
	}
	const float spawnDelay = 0.5f;



	// Use this for initialization
	void Start () {
		playBtn.gameObject.SetActive(false);
		showmenu();
		//spawn corutine commented out so spawn can be implemented as a button press.
		//StartCoroutine(Spawn());
	}
	
	void Update() {
		handleEscape();
	}

	// Update is called once per frame
	// void SpawnEnemy () {
	// 	if(enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies){
	// 		for(int i = 0; i < enemiesPerSpawn; i++){
	// 			if(enemiesOnScreen < maxEnemiesOnScreen){
	// 				GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
	// 				newEnemy.transform.position = spawnPoint.transform.position;
	// 				enemiesOnScreen += 1;
	// 			}
	// 		}
	// 	}

		
	// }
	IEnumerator Spawn () {
		Debug.Log ("spawn called");
		if(enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies){
			for(int i = 0; i < enemiesPerSpawn; i++){
				if(EnemyList.Count < totalEnemies){
					GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
					newEnemy.transform.position = spawnPoint.transform.position;
					
				}
			}
			yield return new WaitForSeconds (spawnDelay);
			StartCoroutine (Spawn());
		}

		
	}

	public void RegisterEnemy(Enemy enemy){
		EnemyList.Add(enemy);
	}

	public void UnregisterEnemy (Enemy enemy){
		EnemyList.Remove(enemy);
		Destroy(enemy.gameObject);
	}

	public void DestroyAllEnemies(){
		foreach(Enemy enemy in EnemyList){
			Destroy(enemy.gameObject);
		}

		EnemyList.Clear();
	}

	public void addMoney (int amount) {
		TotalMoney += amount;
	}

	public void subMoney (int amount) {
		TotalMoney -= amount;
	}

	public void isWaveOver() {
		totalEscapedLbl.text = "Escaped " + TotalEscaped + "/" + tooManyEscapes;
		if ((RoundEscaped + TotalKilled) == totalEnemies) {
			//space for when we actually have a method
			setCurrentGameSate();
			showmenu();
		}
	}

	public void setCurrentGameSate() {
		if (TotalEscaped >= tooManyEscapes){
			currentState = gameStatus.gameover;
		} else if (waveNumber == 0 && (TotalKilled + RoundEscaped) == 0){
			currentState = gameStatus.play;
		} else if (waveNumber >= totalWaves){
			currentState = gameStatus.win;
		} else {
			currentState = gameStatus.next;
		}	
	}
	public void showmenu() {
		switch (currentState) {
			case gameStatus.gameover:
				playBtnLbl.text = "Play Again!";
				//TODO add gameover sound.
				//TODO gameover banner.
				break;
			case gameStatus.next:
				playBtnLbl.text = "Next Wave!";
				break;
			case gameStatus.play:
				playBtnLbl.text = "Play";
				break;
			case gameStatus.win:
				playBtnLbl.text = "Play";
				//TODO add win sound
				//TODO add win banner
				break;
		}
		playBtn.gameObject.SetActive(true);
	}

	public void playBtnPressed() {
		switch (currentState) {
			case gameStatus.next:
				waveNumber += 1;
				totalEnemies += waveNumber;
				break;
			default:
				totalEnemies = 3;
				TotalEscaped = 0;
				TotalMoney = 10;
				TowerManager.Instance.DestroyAllTowers();
				TowerManager.Instance.RenameTagBuildSites();
				totalMoneyLbl.text = TotalMoney.ToString();
				totalEscapedLbl.text = "Escaped " + TotalEscaped + "/" + tooManyEscapes;
				break;

		}
		DestroyAllEnemies();
		TotalKilled = 0;
		RoundEscaped = 0;
		currentWaveLbl.text = "Wave " + (waveNumber + 1);
		StartCoroutine(Spawn());
		playBtn.gameObject.SetActive(false);
	}

	private void handleEscape() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			TowerManager.Instance.disbleDragSprite();
			TowerManager.Instance.towerBTNPressed = null;
		}
	}
}