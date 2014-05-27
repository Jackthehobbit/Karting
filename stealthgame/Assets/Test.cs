using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	RaycastHit hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnCollisionEnter(Collision col)
	{
		if(col.collider.tag == "Driver")
		{
			col.gameObject.GetComponent<NewBehaviourScript>().StartBoost();
		}
	}
}
