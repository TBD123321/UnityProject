using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider02Movement : MonoBehaviour {

    public float health = 50;
	public float speed;
	public int damageFromExplosion;

	private GameObject player;
	private Animator anim;

	private bool Detected = false;
	private bool found = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");	
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
        if(health <= 0)
        {
            Event_Destroyed();
        }
		if (Detected && !found) {
			anim.SetTrigger ("Move");
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, step);
		}
	}

	private void DetectedPlayer()
	{
		Detected = true;
	}
	private void UndetectedPlayer()
	{
		Detected = false;
		anim.ResetTrigger ("Move");
	}

	private void Event_Destroyed()
	{
		Destroy (gameObject);
	}

    private void Event_GetDamage(int damage)
    {
        health -= damage;
    }

	void OnCollisionEnter2D(Collision2D other) {
		
		if (other.gameObject.CompareTag("Player")) 
		{
			found = true;
			anim.ResetTrigger ("Move");
			player.SendMessage ("Event_Damaged",damageFromExplosion);
			anim.SetTrigger ("Explosion");
		}
	}
}
