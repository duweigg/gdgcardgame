using UnityEngine;
using System.Collections;

public class character_attributs : MonoBehaviour {
	//card attributes
	public int cost;

	//caharacter attributes
	public int att;
	public int speed;
	public int hp;
	public int hpmax;
	public int ap;
	public int apmax;
	character_attributs _target_attributes;

	void attack(int attack, GameObject _target){
		_target_attributes = _target.GetComponent<character_attributs>();

		//reserve for further modify on damage calculation.
		_target_attributes.hp -= attack;
	}

	void heal(int attack, GameObject _target){
		_target_attributes = _target.GetComponent<character_attributs>();
		
		//reserve for further modify on damage calculation.
		_target_attributes.hp += attack;
	}


	void onmove(){


	}


	// Use this for initialization
	void Start () {
		hp = hpmax;
		ap = apmax;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
