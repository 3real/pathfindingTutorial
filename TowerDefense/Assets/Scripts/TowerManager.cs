using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager> {

	private TowerBTN towerBTNPressed;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
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
		if(spriteRenderer.enabled){
				followMouse();
			}
	}

	public void placeTower(RaycastHit2D hit) {
		if(!EventSystem.current.IsPointerOverGameObject() && towerBTNPressed != null){
			GameObject newTower = Instantiate(towerBTNPressed.TowerObject);
			newTower.transform.position = hit.transform.position;
			disbleDragSprite();
		}
	}

	public void selectedTower (TowerBTN towerSelected) {
		towerBTNPressed = towerSelected;
		enableDragSprite(towerBTNPressed.DragSprite);
		//Debug.Log("Pressed! : " + towerBTNPressed.gameObject);
	}
	public void followMouse(){
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector2(transform.position.x, transform.position.y);
	}

	public void enableDragSprite (Sprite sprite) {
		spriteRenderer.enabled = true;
		spriteRenderer.sprite = sprite;
	}
	public void disbleDragSprite () {
		spriteRenderer.enabled = false;
	}
}
