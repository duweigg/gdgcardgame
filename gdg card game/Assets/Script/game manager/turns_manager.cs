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
    public int j;

    public bool gameisOver;
    public GameObject GameOverText;
    public Vector3 endPos;

    public float t;
    public bool endingturn;
    public bool bossdead;

    public GameObject player;

	GameManager _manager;

    GUIStyle guiStyle = new GUIStyle();







    public	void endturn(){
        if (turn_count % 2 == 0) {

            turn_count++;

            _manager.resetSelected();

            characterCount();

            StartCoroutine(startenemyturn());
        }
	}

	public void startturn(){
        if (_manager.resources<7) {
            _manager.resources = ++_manager.resourcesmax;
        }
		_manager.drawcard ();
		_manager.arrangeCards ();

        //places all Stats in an array
        characterCount();
		Stats2=new Stats[0];
		//resets all booleans to intital
		foreach (Stats script in StatsLists) {
			if(script!=null){
				script.reset();
			}
		}

	}

    public void characterCount() {
        length = 0;
        StatsLists = new Stats[20];
        foreach (GameObject CharClass in Classlist) {
            Stats2 = CharClass.GetComponentsInChildren<Stats>();
            Stats2.CopyTo(StatsLists, length);
            length += Stats2.Length;
        }
    }


	IEnumerator startenemyturn(){
        if (gameisOver == false) {
            //  AI process
            length = 0;
            Enemylist = new AI[20];
            foreach (GameObject EnemyClass in EnemyClasslist) {
                (EnemyClass.GetComponentsInChildren<AI>()).CopyTo(Enemylist, length);
                length += (EnemyClass.GetComponentsInChildren<AI>()).Length;
                mlength = length;
            }

            foreach (AI Enemy in Enemylist) {
                if (Enemy != null && StatsLists[0] != null) {
                    yield return StartCoroutine(Enemy.AIMove());
                    Enemy.AIAttack();
                    Debug.Log("Attack");
                }
            }
            endenemyturn();
        }
	}

	public void endenemyturn(){
        if (!gameisOver) {
            turn_count++;
        }

        Debug.Log("Test");

        j = 0;

        for (i = 0; i < 20 && j<1; i++) {
            if (StatsLists[i] != null) {
                j++;
            }
        }

        if (j!=1) {
            gameOver();
        }

		if (turn_count % 4 == 0&&mlength<19) {
			Vector2 randomVector = Random.insideUnitCircle*15;
			position = new Vector3 (randomVector.x ,0, randomVector.y);
			position = position + boss.transform.position;
			randomroll= Random.value;
			if (randomroll<=0.5f){
				GameObject monster = Instantiate (monster1, position, Quaternion.identity) as GameObject;
				monster.transform.LookAt (position - new Vector3 (0, 0, 1));
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


        t = Time.time;

		startturn ();
	}

    public void checkEnd() {
        if (!gameisOver) {
            i = 0;
            j = 0;
            foreach (Stats Stat in StatsLists) {
                if (Stat != null) {
                    i++;
                    if (Stat.turnEnded == true) {
                        j++;
                    }
                }
            }
            if (i == j) {
                endturn();
            }
        }
    }

	void drawcard (){



	}

    void gameOver() {
        Debug.Log("Game Over!");
        gameisOver = true;
    }


    void OnGUI() {
        guiStyle.fontSize = 32; //change the font size
        guiStyle.normal.textColor = Color.white;
        if (gameisOver) {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 -50 , 100, 50), "GAME OVER!",guiStyle);
        }
        if (endingturn&&!gameisOver)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 100, 50), "PLAYER TURN START", guiStyle);
        }
        if (bossdead)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 100, 50), "You Won!", guiStyle);
        }
    }



    // Use this for initialization
    void Start () {
		_manager = this.GetComponent<GameManager>();
        gameisOver = false;
        startturn ();
	}

    // Update is called once per frame
    void Update() {
        if (Time.time < t + 1)
        {
            endingturn = true;
        }
        else
        {
            endingturn = false;
        }

        if (player == null)
        {
            gameisOver = true;
        }

        if (boss == null)
        {
            bossdead = true;
        }

    }
}
