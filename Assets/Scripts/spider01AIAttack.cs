﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider01AIAttack : MonoBehaviour {

    public GameObject spider;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            spider.SendMessage("DetectedPlayerForAttack");
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            spider.SendMessage("UndetectedPlayerForAttack");
        }
    }
}
