using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	//player attributes
	public int resourcesmax;
	public int resources;
	character_attributs _attributes;
	public GameObject _terrain;


	//card array;
	public GameObject[] _cards;
	public GameObject[] _handcards;
	public GameObject temp;
	public int index;

	//for raycast place character
	public Camera cam;
	RaycastHit hitt=new RaycastHit(); //to get the point of mouse in 3D world
	public GameObject _char=null;
	int layermask = 1 << 10;   //layer 10 is the teerain


	//for selection
	RaycastHit hit;
	public GameObject selected;
	public LayerMask layer;
	public int turn_count;
	turns_manager _manager;
	public Stats selectedScript;
	public TapToMove moveScript;
	public int t;
	public Stats targetScript;
	public bool isAttacking;

	//attacking/healing
	GameObject Attacker;
	GameObject Target;


	//assign character to variable _char from the card.
	public void _getchar(GameObject character){
		_char = character;
		if (_char != null)
			_attributes = _char.GetComponent<character_attributs> ();
	}

	void placechar(){
		//character follow the mouse
		if (_char != null && _attributes.cost < resources) {
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);
			Physics.Raycast (ray, out hitt, 100, layermask);
			_char.transform.position = hitt.point;

			//if left mouse is clicked;
			if (Input.GetMouseButton (0)) {
				//to place the character
				_char = null;
				_attributes = null;
			}
		} else if (_attributes != null && _attributes.cost > resources) {
			//show warning and play sound effect
			Debug.Log("not enough resource");
		}
	}

	void selectChar(){
		isAttacking = false;

		//selects character
		if (selected==null && Input.GetMouseButtonDown (0)) {
			Ray ray1 = cam.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray1.origin, ray1.direction,out hit, 300, layer.value)){
				Debug.Log ("Hit!");
				selected= hit.collider.gameObject;
				selectedScript = selected.GetComponent<Stats>();
				selectedScript.isSelected = true;
				t=Time.frameCount;
			}
		}
		//deselects character
		if (Input.GetMouseButtonDown (1)) {
			Ray ray2 = cam.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray2.origin, ray2.direction,out hit, 300, layermask)){
				Debug.Log ("Reset!");
				selectedScript.isSelected = false;

			}
		}

		attack ();

		if (!isAttacking) {
			move ();
		}

}
	
	void move(){
		if (selectedScript != null) {
			if (selectedScript.isSelected == true&&t!=Time.frameCount) {
				if (Input.GetMouseButtonDown(0)&&selectedScript.hasMoved==false){
					Ray ray1 = cam.ScreenPointToRay (Input.mousePosition);
					if (Physics.Raycast (ray1.origin, ray1.direction,out hit, 300, layermask)){
						Debug.Log ("Moving!");
						selectedScript.hasMoved= true;
						moveScript = selected.GetComponent<TapToMove>();
						moveScript.ready_to_move=true;
					}
				}
			}
		}
	}

	
	void attack(){
		if (selectedScript != null&&selected!=null) {
			if (Input.GetMouseButtonDown(0)&&selectedScript.hasAttacked==false&&t!=Time.frameCount){
				Ray ray2 = cam.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray2.origin, ray2.direction, out hit, 300, layer.value)){
					Debug.Log ("Attacking!");
					isAttacking=true;
					Target = hit.collider.gameObject;
					targetScript=Target.GetComponent<Stats>();
					selectedScript.hit ();
					targetScript.debugattack = true;
				}
			}
		}

	}

	void shaffle_card(){
		for (int i=0; i<20; i++) {
			index = Random.Range (i,20);
			temp = _cards[index];
			_cards[index] = _cards[i];
			_cards[i] = temp;
		}
	}

	public void drawcard(){
		
	}

	// Use this for initialization
	void Start () {
		shaffle_card ();
		//draw 5 cards
		drawcard ();
		drawcard ();
		drawcard ();
		drawcard ();
		drawcard ();

		_manager = this.GetComponent<turns_manager>();

	}
	
	// Update is called once per frame
	void Update () {

		placechar ();

		turn_count=_manager.turn_count;
		if (turn_count % 2 == 0) {
			selectChar ();
		}



	}
}
