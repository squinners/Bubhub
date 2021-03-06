using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class playerController : MonoBehaviour {

	public float speed;
	private Rigidbody rb;
	public Text uitext;
	string url = "http://api.tumblr.com/v2/blog/squinners.tumblr.com/likes?api_key=x3pIKyCJKLugGNcHQp8beCB7aGMFPyLdg9fRajsU9n4hbhiLc0";
	List<string> images = new List<string>();
	public GameObject plane;
	public int postoffset;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		uitext.text = "loaded reference into playerController";
		postoffset = 0;
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);
	}

	public void getgot()
	{
		Vector3 jump = new Vector3 (0.0f, 500, 0.0f);
		rb.AddForce (jump);
		uitext.text = "starting boyo";
		StartCoroutine(boyo());
	}

	IEnumerator boyo()
	{
		uitext.text = "sending request";
		WWW www = new WWW (url); //www object contains only raw json
		uitext.text = "request sent";
		yield return www; //waits for response before proceeding
		uitext.text = "www returned";
		JsonData data = JsonMapper.ToObject (www.text); //maps json to nested array object. posts are stored in ["response"]["liked_posts"][i]
		JsonData posts = data["response"]["liked_posts"]; //new nested array of only the posts, now stored in [i]. Makes iteration simpler to code. Must cast types when using.  

		//iterates through posts and adds 400x600 image url to "images" list if post is of type "photo" (should add "answer" type later)
		//use .Count for length of JsonData object or lists
		for (int i = 0; i < posts.Count; i++) 
		{
			if ((string) posts[i]["type"] == "photo") 
			{
				images.Add((string) posts[i]["photos"][0]["alt_sizes"][2]["url"]);
			}
		}
		uitext.text = images [0];

		//ideally will go in each plane later - downloads image and sets as texture. herein sets it on a given plane for now.
		WWW www1 = new WWW (images[0]); 
		yield return www1;
		Renderer renderer = plane.GetComponent<Renderer> ();
		renderer.material.mainTexture = www1.texture;
		//uitext.text = images.Count.ToString(); 
		//string message = (string) posts [0]["type"];
		//uitext.text = message;
	}
}