using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	
	public int hp;
	public int maxhp;
	public int attack;
	public int cost;
	public int currentattack;
	public float critchance;
	public float attackrange;
	public float distance;
	public float attackerRange;
	public int finaldamage;
	public int armor;
	public int healval;
	public float randomroll;
	public bool isEnemy = false;
	public bool attackerIsEnemy = false;
	
	//set these to start at beginning of turn in TurnStart()
	public bool hasAttacked = false;
	public bool hasMoved = false;
	public bool turnEnded = false;
	
	
	
	public GameObject attacker;
	public GameObject charClass;
    public turns_manager tm;
	
	//testing tool for generating hits on character
	public bool debugattack = false;
	public bool debughit = false;
	public bool debugheal = false;

	public bool isSelected;

	void Start () {
		//Do we reduce the summon points here or in game controller?
		//Searches for the class of the new unit
		charClass = transform.parent.gameObject;
		StatBlock statScript = charClass.GetComponent<StatBlock> ();
		//can someone get all of these at once?
		hp= statScript.HP ();
		maxhp = statScript.maxHP ();
		attack = statScript.attack ();
		critchance = statScript.critchance ();
		attackrange = statScript.attackRange ();
		armor = statScript.armor();
		healval = statScript.heal ();
        tm = FindObjectOfType<turns_manager>();
	}
	
	void Update () {
		//Ends turn after attacking
		if (hasAttacked == true) {
			turnEnded = true;
		}
		//Calls function to grey out unit after movement
		if (turnEnded == true) {
			greyOut();
		}
		
		//testing tool for generating hits on character
		//this is for the one who is attacked
		if (debugattack == true) {
			isAttacked ();
			debugattack = false;
		}
		//this is for the attacker
		if (debughit == true) {
			hit ();
			debughit=false;
		}
		//this is for healing
		if (debugheal == true) {
			heal ();
			debugheal=false;
		}
		
	}
	
	void move(){
		//call move script? need more info
		hasMoved = true;
	}
	
	//attack function
	public void hit(){
		currentattack = attack;
		gameObject.tag = "Hitting";
		hasAttacked = true;
	}
	
	//heal function
	public void heal(){
		healval = 4;
		currentattack = -healval;
		gameObject.tag = "Hitting";
		hasAttacked = true;
	}
	
	//outputs current attack value
	public int attackvalue(){
		//checks for crit
		randomroll = Random.value;
		if(critchance>randomroll){
			finaldamage = 3*currentattack;
		}else{
			finaldamage = currentattack;
		}
		//check for armor goes here
		return finaldamage;
	}

	//function for when unit is attacked
	public bool isAttacked(){
		//finds attacker
		attacker = GameObject.FindWithTag ("Hitting");
		
		if (attacker == null) {
			Debug.Log ("Cannot find Attacker");
			resetAttacker ();
			return false;
		}
		
		//gets script from attacker
		Stats attackScript = attacker.GetComponent<Stats> ();
		
		//retrieves distance
		distance = Vector3.Distance (transform.position, attacker.transform.position);
		
		//gets attacker's range
		attackerRange = attackScript.attackingRange ();
		
		attackerIsEnemy = attackScript.enemyCheck ();
		
		Debug.Log ("attackerisEnemy=" + attackerIsEnemy);
		
		if (distance < attackerRange) {
			//retrieves currentattack
			currentattack = attackScript.attackvalue ();
			
			//checks if healing enemies or attacking allies
			if (attackerIsEnemy!=isEnemy){
				if (currentattack<0){
					undoSelection();
					Debug.Log ("Healing Enemies!");
					resetAttacker();
					return false;
				}
			}
			if (attackerIsEnemy==isEnemy){
				if (currentattack>=0){
					undoSelection();
					Debug.Log ("Attacking allies/self!");
					resetAttacker();
					return false;
				}
			}

			//reduces damage by armor
			if (currentattack >=0){
				currentattack-=armor;
			}
			
			//reduces health
			hp = hp - currentattack;
			
			//check for healing cases
			if (hp > maxhp) {
				hp = maxhp;
			}
			//kills character if neccesary
			if (hp <= 0) {
				death ();
			}
		} else {
			//Undo the attacking section so the unit can be selected again
			attackScript.undoSelection ();
			Debug.Log ("Too far. Attacker reset");
			resetAttacker();
			return false;
		}
		
		//reset attacker
		resetAttacker ();
		return true;
	}
	
	public float attackingRange(){
		return attackrange;
	}
	
	public bool enemyCheck(){
		return isEnemy;
	}
	
	void resetAttacker(){
		attacker.tag = "Untagged" ;
		attacker = null;
	}
	
	void undoSelection (){
		hasAttacked = false;
		turnEnded = false;
	}
	
	//function for showing character is unselectable
	void greyOut(){
		//change material here?
	}

	//reset function
	public void reset(){
		hasAttacked = false;
		hasMoved = false;
		turnEnded = false;
		isSelected = false;
	}
	
	//death function
	void death(){
        //play death animation?
        transform.parent = null;
        tm.characterCount();
        Destroy (this.gameObject);
        
    }
}
