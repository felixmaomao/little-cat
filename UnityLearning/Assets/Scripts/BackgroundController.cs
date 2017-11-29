using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour
{
	public  float speed = 0.1F;
	public Renderer rend;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
	}


	// Update is called once per frame
	void Update () {
		float x = Mathf.Repeat (Time.time*speed,10);
		rend.material.mainTextureOffset = new Vector2 (x,0);
	}
}

