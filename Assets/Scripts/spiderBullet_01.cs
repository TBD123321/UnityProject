using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderBullet_01 : MonoBehaviour {

    public float bulletSpeed;
    public float damage = 2;

    [HideInInspector]
    public Vector2 bulletDir;

    private bool destroyed = false;
    private GameObject player;
    private Animator animPlayer;

    // Use this for initialization
    void Start () {
        bulletDir = new Vector2(0, bulletSpeed * Time.deltaTime);
    }
	
	// Update is called once per frame
	void Update () {
        if (!destroyed)
        {
            gameObject.transform.Translate(bulletDir);
            Invoke("Event_Destroy", 1);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            other.gameObject.SendMessage("Event_Damaged", damage);
        }
    }

}
