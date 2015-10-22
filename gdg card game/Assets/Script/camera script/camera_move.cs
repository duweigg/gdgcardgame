using UnityEngine;
using System.Collections;

public class camera_move : MonoBehaviour {
	public float  speed;
	public int distance;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.mousePosition.x < distance) {
			this.transform.Translate (-speed , 0 , 0, Space.World);
		}
		if (Input.mousePosition.x > Screen.width-distance) {
			this.transform.Translate (speed , 0 , 0, Space.World);
		}
		if (Input.mousePosition.y < distance) {
			this.transform.Translate (0 ,  0 , -speed , Space.World);
		}
		if (Input.mousePosition.y > Screen.height - distance) {
			this.transform.Translate (0 , 0,  speed , Space.World);
		}
	
	}
}
