using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class framebehavior_local : MonoBehaviour {

	private int index;
	private string url;
	private double width;
	private double height;
	void Start () {

	}

	IEnumerator render () {
		index = this.transform.GetComponentInParent<roombehavior_local> ().renderindex;
		this.transform.GetComponentInParent<roombehavior_local> ().renderindex++;
		url = this.transform.GetComponentInParent<roombehavior_local> ().images [index].url;
		width = this.transform.GetComponentInParent<roombehavior_local> ().images [index].width;
		height = this.transform.GetComponentInParent<roombehavior_local> ().images [index].height;
		Debug.Log("adding " + url + " to picture of size " + width + " by " + height);
		WWW www = new WWW (url); 
		yield return www;
		Vector3 scale = transform.localScale;
		scale.x = (float) width / 1000;
		scale.z = (float) height / 1000;
		transform.localScale = scale;
		Renderer renderer = GetComponent<Renderer> ();
		renderer.material.mainTexture = www.texture;
	}
}
