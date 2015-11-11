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

	public Stats[] StatsLists; 
	public Stats[] Stats2;
	public GameObject[] Classlist;
	public GameObject[] EnemyClasslist;
	public AI[] Enemylist;
	public int length;
	public int mlength;
	private int i;

	GameManager _manager;









	public	void endturn(){
		turn_count++;
	
		_manager.resetSelected ();



		startenemyturn ();
	}

	public void startturn(){
		_manager.resources = ++_manager.resourcesmax;
		_manager.drawcard ();


		//places all Stats in an array
		length = 0;
		StatsLists = new Stats[20];
		foreach (GameObject CharClass in Classlist) {
			Stats2 = CharClass.GetComponentsInChildren<Stats> ();
			Stats2.CopyTo (StatsLists, length);
			length += Stats2.Length;
		}
		Stats2=new Stats[0];
		//resets all booleans to intital
		foreach (Stats script in StatsLists) {
			if(script!=null){
				script.reset();
			}
		}

	}

	void startenemyturn(){
		//  AI proccess
		length = 0;
		Enemylist = new AI[20];
		foreach (GameObject EnemyClass in EnemyClasslist){
			(EnemyClass.GetComponentsInChildren<AI> ()).CopyTo (Enemylist, length);
			length+= (EnemyClass.GetComponentsInChildren<AI> ()).Length;
			mlength=length;
		}
		
		foreach (AI Enemy in Enemylist) {
			if (Enemy!=null){
			Enemy.AIMove();
			Enemy.AIAttack();
			}
		}

		endenemyturn ();
	}

	public void endenemyturn(){
		turn_count++;

		if (turn_count % 4 == 0&&mlength<19) {
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
