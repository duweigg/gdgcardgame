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
	
	}
	
	// Update is called once per frame
	void Update () {

		placechar ();


	}
}
