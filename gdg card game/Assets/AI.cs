using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {
	public GameObject selected;
	public Vector3 relativePos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		relativePos = selected.transform.position - transform.position;
	}

	public void AIMove(){

	}



	public void AIAttack(){
	}
}
