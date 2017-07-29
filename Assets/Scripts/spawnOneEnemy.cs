using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnOneEnemy : MonoBehaviour {

    public GameObject enemy;
    public float spawnSpeed;

    private bool canSpawn = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (canSpawn)
        {
            Instantiate(enemy, gameObject.transform.position,gameObject.transform.rotation);
            StartCoroutine(CanSpawn());
        }
    }

    IEnumerator CanSpawn()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnSpeed);
        canSpawn = true;
    }
}
