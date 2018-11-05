using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager> {

	private TowerBTN towerBTNPressed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(worldPoint,Vector2.zero);
			if (hit.collider.tag == "buildSite"){
				hit.collider.tag = "buildSiteFull";
				placeTower(hit);
			}
		}
	}

	public void placeTower(RaycastHit2D hit) {
		if(!EventSystem.current.IsPointerOverGameObject() && towerBTNPressed != null){
			GameObject newTower = Instantiate(towerBTNPressed.TowerObject);
			newTower.transform.position = hit.transform.position;
		}
	}

	public void selectedTower (TowerBTN towerSelected) {
		towerBTNPressed = towerSelected;
		//Debug.Log("Pressed! : " + towerBTNPressed.gameObject);
	}
}
