using UnityEngine;
using System.Collections;

public class GameManager_old : MonoBehaviour {
	public CursorLockMode Cmode;

	private mouseorbit _mo;
	private GameObject _player;

	//cursor_control;
	void _cursor(){
		Cursor.lockState = Cmode;

		if(Input.GetKeyUp(KeyCode.LeftAlt)){
			Cursor.visible=!Cursor.visible;
			if(Cmode == CursorLockMode.None){
				Cmode = CursorLockMode.Locked;
				_mo.enabled = true;
			}else if(Cmode == CursorLockMode.Locked){
				Cmode = CursorLockMode.None;
				_mo.enabled = false;
			}
		}	
	}




	// Use this for initialization
	void Start () {
		_mo= GameObject.Find("MainCamera").GetComponent<mouseorbit>();
		_player = GameObject.Find ("player");
		Cursor.visible = false;
		Cmode = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		_cursor ();
	}
}
