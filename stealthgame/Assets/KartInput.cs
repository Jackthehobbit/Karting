using UnityEngine;
using System.Collections;

/// 
/// Class For Dealing With Player Input and Controlling The Car/// 
public class KartInput: MonoBehaviour {
	Rigidbody r;
	float acceleration,deceleration,maxspeed,angle,maxRot,xval,zval,minRot,sensitivity,brake;
	public GameObject groundcheck,rotatepoint;
	public Camera main,back;
	RaycastHit hit;

	// Use this for initialization
	void Start () 
	{
		r = GetComponent<Rigidbody>();
		acceleration = 20;//Acceleration Value
		deceleration=1.5f;//Value to Slow down Quicker
		brake = 3;
		maxspeed = 45;//MaxSpeed of the car
		//maxRot
		sensitivity =45;


	}
	
	// Update is called once per frame
	void Update () {

		angle +=Input.GetAxis("Horizontal")*sensitivity;
		if(Input.GetAxis ("Horizontal")==0)
		{
			angle = 0;
		}
		//angle= Mathf.Clamp(angle,-,45);

		if(Physics.Raycast(groundcheck.transform.position,-Vector3.up,out hit,2f))
		{ 

			Debug.DrawRay(transform.position,transform.forward*100,Color.cyan);
			transform.rotation = Quaternion.FromToRotation(Vector3.up,hit.normal);

		}
		else{
			xval=transform.rotation.x;
			zval=transform.rotation.z;
			transform.rotation=Quaternion.Euler(xval,0,zval);}
		print (angle);
		transform.Rotate (Vector3.up,angle);



		GetAccel();//Gets Value for input of -1 to 1
		Grounded();//Checks if the player is on the ground
		ChangeCam();//Method to change camera to look behind the player

	
	}
	void FixedUpdate (){
		if(GetAccel()==0&&Grounded())//if there is no accelerate button pressed
		{
			r.AddForce(-r.velocity*deceleration);//slowdown
			
		}
		if(Input.GetKey(KeyCode.JoystickButton2)&&Grounded())//if there is no accelerate button pressed
		{
			r.AddForce(-r.velocity*brake);//slowdown
			
		}
		r.AddRelativeForce(new Vector3(0,0,GetAccel()*acceleration));//Accelerates either forwards or backwards depending on input
		r.velocity= Vector3.ClampMagnitude(r.velocity,maxspeed);//Gives the car a top speed
		
	}
	int GetAccel()//Method to get input value
	{
		if(Input.GetKey(KeyCode.JoystickButton0))//returns 1 if A button on xbox controller is pressed
			return 1;
		if(Input.GetKey(KeyCode.JoystickButton3))//retunrs -1 if Y button on xbox controller is presed
			return -1;
		else//returns 0 if neither of these buttons are pressed
			return 0;
	}



	bool Grounded()//method to find out if grounded
	{
		if(Physics.Raycast(groundcheck.transform.position,-Vector3.up,2f))//sends a ray of 2units down from the groundcheck position 
		{
			return true;//if it collides it is grounded
		}
		else
		{
			return false;//if no collision it isnt.
		}
	
		
	}
	void ChangeCam()//Function to change the camera when the player wants to look backwards
	{
		if(Input.GetKey(KeyCode.JoystickButton1))
		{
			main.enabled= false;
			back.enabled=true;
		}
		else
		{
			main.enabled= true;
			back.enabled=false;
			
		}
		
	}
	public void StartBoost()
	{
		StartCoroutine(Boost());
	}
	 IEnumerator Boost()//Function to boost the car when going over a boost section on the map
	{
		float temp = maxspeed;
		maxspeed*=10000;
		print ("Boost");
		r.AddForce(Vector3.forward*2000);
		
		yield return new WaitForSeconds(0.5f);
		
		maxspeed=temp;
		
		r.velocity=new Vector3(0,0,maxspeed);
		
	}

}