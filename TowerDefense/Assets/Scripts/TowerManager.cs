using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager> {
	public TowerBTN towerBTNPressed{get; set;}
	private SpriteRenderer spriteRenderer;
	private Collider2D buildTile;
	public List<Tower> TowerList = new List<Tower>();
	private List<Collider2D> BuildList = new List <Collider2D>();


	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		buildTile = GetComponent<Collider2D>();
		spriteRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(worldPoint,Vector2.zero);
			if (hit.collider.tag == "buildSite"){
				buildTile = hit.collider;
				buildTile.tag = "buildSiteFull";
				RegisterBuildSite(buildTile);
				placeTower(hit);
			}
			
		}
		if(spriteRenderer.enabled){
				followMouse();
			}
	}

	public void RegisterBuildSite(Collider2D buildTag) {
		BuildList.Add(buildTag);
	}

	public void RegisterTower(Tower tower){
		TowerList.Add(tower);
	}

	public void RenameTagBuildSites(){
		foreach(Collider2D buildTag in BuildList) {
			buildTag.tag = "buildSite";
		}
		BuildList.Clear();
	}

	public void DestroyAllTowers(){
		foreach(Tower tower in TowerList){
			Destroy(tower.gameObject);
		}
		TowerList.Clear();
	}
	public void placeTower(RaycastHit2D hit) {
		if(!EventSystem.current.IsPointerOverGameObject() && towerBTNPressed != null){
			Tower newTower = Instantiate(towerBTNPressed.TowerObject).GetComponent<Tower>();
			newTower.transform.position = hit.transform.position;
			buyTower(towerBTNPressed.TowerPrice);
			RegisterTower(newTower);
			disbleDragSprite();
		}
	}

	public void buyTower (int price) {
		GameManager.Instance.subMoney(price);
	}

	public void selectedTower (TowerBTN towerSelected) {
		if (towerSelected.TowerPrice <= GameManager.Instance.TotalMoney) {
			towerBTNPressed = towerSelected;
			enableDragSprite(towerBTNPressed.DragSprite);
			//Debug.Log("Pressed! : " + towerBTNPressed.gameObject);
		}
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
