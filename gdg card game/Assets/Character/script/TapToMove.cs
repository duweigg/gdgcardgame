using UnityEngine;
using System.Collections;

public class TapToMove : MonoBehaviour
{
    public bool ready_to_move = false;
    //flag to check if the user has tapped / clicked. 
    //Set to true on click. Reset to false on reaching destination
    public bool flag = false;
    //destination point
    public Vector3 check;
    public Vector3 endPoint;
    //alter this to change the speed of the movement of player / gameobject
    public float duration = 50.0f;
    //vertical position of the gameobject
    private float yAxis;
	public bool isMoving=false;
	Animator _char_animator;
    public RaycastHit hit;
    public LayerMask layers;

    void Start()
    {
		//get the animator of the char edited by duwei
		_char_animator = gameObject.GetComponent<Animator> ();
        //save the y axis value of gameobject
        yAxis = gameObject.transform.position.y;
        layers = 1 <<10;
    }

    // Update is called once per frame
    void Update()
    {
        check = gameObject.transform.position;

        //check if the screen is touched / clicked   
        if ((ready_to_move && 
            (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0))))
        {
            //declare a variable of RaycastHit struct
            //Create a Ray on the tapped / clicked position
            Ray ray;
            //for unity editor
            #if UNITY_EDITOR
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //for touch device
            #elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            #endif

            //Check if the ray hits any collider
            if (!isMoving && Physics.Raycast(ray, out hit,300, layers))
            {
                //set a flag to indicate to move the gameobject
                flag = true;
                //save the click / tap position
                endPoint = hit.point;
                //as we do not want to change the y axis value based on touch position, reset it to original y axis value
                endPoint.y = yAxis;               
            }

        }
        //check if the flag for movement is true and the current gameobject position is not same as the clicked / tapped position
        if (ready_to_move && flag && !Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude))
        { //&& !(V3Equal(transform.position, endPoint))){
          //move the gameobject to the desired position
			transform.LookAt(endPoint);

            if (_char_animator != null) {
                _char_animator.SetInteger("IsMoving", 1);
            }

			gameObject.transform.position =
                Vector3.Lerp(gameObject.transform.position, endPoint, 1 /
                (duration * (Vector3.Distance(gameObject.transform.position, endPoint))));
			isMoving=true;
        }
        //set the movement indicator flag to false if the endPoint and current gameobject position are equal
        else if (flag && Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude))
        {
            flag = false;
            Debug.Log("I am here");
            ready_to_move = false;
			isMoving=false;
            if (_char_animator != null) {
                _char_animator.SetInteger("IsMoving", 0);
            }
            
        }

    }
}