using UnityEngine;
using System.Collections;

public class StatBlock : MonoBehaviour {
	
	public int hp;
	public int maxhp;
	public int attackval;
	public float critval;
	public float attackrange;
	public int armorval;
	public int healval;
	
	public int HP(){
		return hp;
	}
	
	public int maxHP (){
		return maxhp;
	}
	
	public int attack(){
		return attackval;
	}
	
	public float critchance(){
		return critval;
	}
	
	public float attackRange(){
		return attackrange;
	}
	
	public int armor(){
		return armorval;
	}

	public int heal(){
		return healval;
	}

}
