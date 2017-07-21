using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterBullet_01 : MonoBehaviour {
     
	public float bulletSpeed;

	[HideInInspector]
	public Vector2 bulletDir;

	private bool destroyed = false;
	private GameObject player;
	private Animator animPlayer;

	// Use this for initialization
	void Start () {
	bulletDir = new Vector2 (0,bulletSpeed*Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () {
		if (!destroyed) {
			gameObject.transform.Translate (bulletDir);
			Invoke ("Event_Destroy",1);
		}
	}

	private void Event_Destroy(){
		Destroy (gameObject);
	}
		
}
