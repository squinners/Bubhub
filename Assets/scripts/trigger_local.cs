using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_local : MonoBehaviour {

	public roombehavior_local room;

//	void Start()
//	{
//		room = this.transform.parent.GetComponent<roombehavior> ();
//	}

	void OnTriggerEnter(Collider other)
	{
		room.CreateRoom();
		room.RemoveTriggerPlane();
	}

}
