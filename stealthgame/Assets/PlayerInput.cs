using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	Rigidbody r;
	RaycastHit hit;
	float acceleration;
	public Camera main;
	//public Camera back;
	Quaternion initrot;
	public float maxRot,deceleration,tempdecel;
	float angle,maxspeed;
	Vector2 movespeed;




	// Use this for initialization
	void Start () {
		r= GetComponent<Rigidbody>();
			acceleration = 15;
		initrot = transform.rotation;
		maxRot=45;
		maxspeed=25;

	
	}
	
	// Update is called once per frame
	void Update () {
	
		Physics.Raycast(transform.position,-Vector3.up,out hit,2f);
		ChangeCam();
		angle=Input.GetAxis("Horizontal")*maxRot;
		transform.rotation=Quaternion.Euler(0,angle,0);
		movespeed= new Vector2(r.velocity.x,r.velocity.z);


		if(GetAccel()==0&&Grounded())
		{
			r.AddRelativeForce(-r.velocity*deceleration);

		}
		if(Grounded())
		{
			if(movespeed.magnitude>maxspeed)
			{
				movespeed=movespeed.normalized;
				movespeed*=maxspeed;
				r.velocity=new Vector3(movespeed.x,0,movespeed.y);
				
			}
			r.AddRelativeForce(new Vector3(0,0,GetAccel()*acceleration));

		}
		if(Input.GetKeyDown(KeyCode.B))
		{

			StartCoroutine(Boost());


		}
	

	}
	int GetAccel() 
	{
		if(Input.GetKey(KeyCode.JoystickButton0))
			return 1;
		if(Input.GetKey(KeyCode.JoystickButton3))
			return -1;
	 else
			return 0;
	}
	void ChangeCam()
	{
		if(Input.GetKey(KeyCode.JoystickButton1))
		{
			main.enabled= false;
			//back.enabled=true;
		}
		else
		{
			main.enabled= true;
			//back.enabled=false;
			
		}

	}
	bool Grounded()
	{
		if(Physics.Raycast(transform.position,-Vector3.up,2f))
			return true;
		else{
			return false;
		}

	}
	 IEnumerator Boost()
	{
		float temp = maxspeed;
		maxspeed*=10000;
		r.AddForce(Vector3.forward*2000);

		yield return new WaitForSeconds(0.05f);

		maxspeed=temp;

		r.velocity=new Vector3(0,0,maxspeed);

	}

	
}
