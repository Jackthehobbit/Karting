using UnityEngine;
using System.Collections;

public class SetCam : MonoBehaviour {
	float angle;
	GameObject camPos,Car;
	void Start() {
		camPos=GameObject.Find("CamPos");
		Car=GameObject.Find ("Car");
	}

	void Update () 
	{

		transform.position = camPos.transform.position;
	}
}
