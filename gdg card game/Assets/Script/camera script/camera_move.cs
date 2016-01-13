using UnityEngine;
using System.Collections;

public class camera_move : MonoBehaviour {
	public float  speed;
	public int distance;
    public int minx;
    public int maxx;
    public int minz;
    public int maxz;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (!Input.GetKey("r")){
		    if (Input.mousePosition.x < distance && transform.position.x > minx) {
			    this.transform.Translate (-speed , 0 , 0, Space.World);
	    	}
		    if (Input.mousePosition.x > Screen.width-distance && transform.position.x < maxx) {
			    this.transform.Translate (speed , 0 , 0, Space.World);
		    }
	    	if (Input.mousePosition.y < distance && transform.position.z > minz) {
			this.transform.Translate (0 ,  0 , -speed , Space.World);
		    }
		    if (Input.mousePosition.y > Screen.height - distance&& transform.position.z<maxz) {
		    	this.transform.Translate (0 , 0,  speed , Space.World);
	    	}
		}
	
	}
}
