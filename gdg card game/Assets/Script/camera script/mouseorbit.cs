using UnityEngine;
using System.Collections;

public class mouseorbit : MonoBehaviour
{
	public Transform Target;
	public float Distance = 5.0f;
	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;
	public float yMinLimit = -20.0f;
	public float yMaxLimit = 80.0f;
	public float zoomSensitivity= 15.0f;
	public float zoomSpeed= 5.0f;
	public float zoomMin= 5.0f;
	public float zoomMax= 80.0f;





	private float zoom;
	private float x;
	private float y;


	
	void Awake()
	{


		Vector3 angles = transform.eulerAngles;
		x = angles.x;
		y = angles.y;
		zoom = Distance;
		if(GetComponent<Rigidbody>() != null)
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}
	}

	void Update() {
		zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
		zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
	}

	void LateUpdate()
	{
		if(Target != null )
		{
			x += (float)(Input.GetAxis("Mouse X") * xSpeed * 0.02f);
			y -= (float)(Input.GetAxis("Mouse Y") * ySpeed * 0.02f);
			
			y = ClampAngle(y, yMinLimit, yMaxLimit);
			
			Quaternion rotation = Quaternion.Euler(y, x, 0);
			Vector3 position = rotation * (new Vector3(0.0f, 0.0f, -Distance)) + Target.position;
			
			transform.rotation = rotation;
			transform.position = position;
			Distance = Mathf.Lerp (Distance, zoom, Time.deltaTime * zoomSpeed);
		}
	}
	
	private float ClampAngle(float angle, float min, float max)
	{
		if(angle < -360)
		{
			angle += 360;
		}
		if(angle > 360)
		{
			angle -= 360;
		}
		return Mathf.Clamp (angle, min, max);
	}
}