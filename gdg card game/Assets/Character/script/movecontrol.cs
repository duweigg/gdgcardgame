using UnityEngine;
using System.Collections;

public class movecontrol : MonoBehaviour {

	protected
		GameObject _camera;

		
	public
		int speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {






		//rotate so that it is always back facing the camera.
		_camera=GameObject.Find("Main Camera");
		this.transform.rotation = Quaternion.Euler(0,_camera.transform.rotation.eulerAngles.y,0);
		//move forward
		if (Input.GetKey("w")){
			transform.Translate(0,0,speed*Time.deltaTime);
		};
	
	}
}
