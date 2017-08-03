using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carEnvironment : MonoBehaviour {

    public float hp = 150;

    private Animator anim;

	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(hp <= 0)
        {
            anim.SetTrigger("Destroy");
        }
	}

    private void Event_Damaged(int damage)
    {
        hp -= damage;
    }

}
