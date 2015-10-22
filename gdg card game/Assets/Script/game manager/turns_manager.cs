using UnityEngine;
using System.Collections;

public class turns_manager : MonoBehaviour {
	public int turn_count;
	public float randomroll;
	public Vector3 position;
	public GameObject boss;
	public GameObject monster1;
	public GameObject monster2;
	public GameObject monster3;
	public GameObject monster4;
	public GameObject monster5;

	public GameObject Monster1;
	public GameObject Monster2;
	public GameObject Monster3;
	public GameObject Monster4;
	public GameObject Monster5;


	GameManager _manager;









	public	void endturn(){
		turn_count++;
	




		startenemyturn ();
	}

	public void startturn(){
		_manager.resources = ++_manager.resourcesmax;
		_manager.drawcard ();



	}

	public void startenemyturn(){
		//  AI proccess


		endenemyturn ();
	}

	public void endenemyturn(){
		turn_count++;

		if (turn_count % 4 == 0) {
			Vector2 randomVector = Random.insideUnitCircle*15;
			position = new Vector3 (randomVector.x,0,randomVector.y);
			position+= boss.transform.position;
			randomroll= Random.value;
			if (randomroll<=0.5f){
				GameObject monster = Instantiate (monster1, position, Quaternion.identity) as GameObject;
				monster.transform.parent = Monster1.transform;
			}
			if (randomroll>0.5f&&randomroll<=0.7f){
				GameObject monster = Instantiate (monster2, position, Quaternion.identity) as GameObject;
				monster.transform.parent = Monster2.transform;
			}
			if (randomroll>0.7f&&randomroll<=0.8f){
				GameObject monster = Instantiate (monster3, position, Quaternion.identity) as GameObject;
				monster.transform.parent = Monster3.transform;
			}
			if (randomroll>0.8f&&randomroll<=0.9f){
				GameObject monster = Instantiate (monster4, position, Quaternion.identity) as GameObject;
				monster.transform.parent = Monster4.transform;
			}
			if (randomroll>0.9f&&randomroll<=1.0f){
				GameObject monster = Instantiate (monster5, position, Quaternion.identity) as GameObject;
				monster.transform.parent = Monster5.transform;
			}



		}




		startturn ();
	}


	void drawcard (){



	}






	// Use this for initialization
	void Start () {
		_manager = this.GetComponent<GameManager>();
		startturn ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
