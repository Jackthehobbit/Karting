using UnityEngine;
using System.Collections;

public class CamCollide : MonoBehaviour {

	float minDistance,maxDistance;
	GameObject Car;
	void Start()
	{
		Car= GameObject.Find ("Car");
	}
	void OnCollisionEnter()
	{
		print ("MOVE CAM Forward");
	}
	void OnCollisionExit()
	{
		print ("MOVE CAM");

	}
}
