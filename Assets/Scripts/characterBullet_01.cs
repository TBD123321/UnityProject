using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterBullet_01 : MonoBehaviour {
     
	public float bulletSpeed;
    public float damage = 2;

	[HideInInspector]
	public Vector2 bulletDir;

	private bool destroyed = false;
	private GameObject player;
	private Animator anim;

	// Use this for initialization
	void Start () {
	    bulletDir = new Vector2 (0,bulletSpeed*Time.deltaTime);
        anim = GetComponent<Animator>();
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

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            destroyed = true;
            gameObject.transform.Translate(new Vector3(0, 0, 0));
            anim.SetTrigger("Destroy");
            other.gameObject.SendMessage("Event_GetDamage",damage);
        }
    }

}
