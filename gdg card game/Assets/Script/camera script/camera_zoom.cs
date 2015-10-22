using UnityEngine;
using System.Collections;

public class camera_zoom : MonoBehaviour {
//	public float zoomspeed;
	public float zoomsenstivity;
//	public float zoommin;
//	public float zoommax;
	private float zoom;


	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		zoom = -Input.GetAxis ("Mouse ScrollWheel") * zoomsenstivity;
	
		Camera.main.transform.Translate(  0, zoom , 0 , relativeTo: Space.World);
	
	}
}
