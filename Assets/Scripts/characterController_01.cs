using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterController_01 : MonoBehaviour {

	public int maxHealth = 100;
	public int maxBullets = 9;
    public float moveSpeed;
	public float attackSpeed;
	public float timeBetweenBullets;
	public GameObject firePos;
	public characterBullet_01 bullet;
	public Slider healthBar;

    private Text healthText;
	private int currentHealth;
    private Animator anim;
	private bool canShoot = true;
	private Light firePosLight;
	private int bulletCount;
    private Vector2 lastMove;
    private Rigidbody2D playerRigidbody;

    // Use this for initialization
    void Start()
    {
		currentHealth = maxHealth;
		bulletCount = maxBullets;

        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
		firePosLight = GetComponentInChildren<Light> ();

		healthBar.maxValue = maxHealth;
		healthText = healthBar.GetComponentInChildren<Text>();
		healthBar.value = CalculateHealth ();
    }

    // Update is called once per frame
    void Update()
    {
        anim.ResetTrigger("01_characterMoving");

		PlayerMovement();

        if (Input.GetKey (KeyCode.Space) && canShoot) 
		{
			
			firePosLight.enabled = !firePosLight.enabled;
			Invoke ("light_enabled", 0.1f);
	
			StartCoroutine (Shot ());
		
			StartCoroutine (CanShoot ());
		}

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
	
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}

		if (currentHealth <= 0) {
			healthBar.value = CalculateHealth ();
			Event_Die ();
		}
    }

    private void light_enabled(){
		firePosLight.enabled = !firePosLight.enabled;
	}

	IEnumerator CanShoot(){
		canShoot = false;
		yield return new WaitForSeconds (attackSpeed);
		canShoot = true;
	}
		
	IEnumerator Shot(){
		do {
			yield return new WaitForSeconds (timeBetweenBullets);
			firePos.transform.rotation = Quaternion.LookRotation (transform.forward, lastMove);
			Instantiate (bullet, firePos.transform.position, firePos.transform.rotation);
			maxBullets--;
		} while(maxBullets != 0);
		maxBullets = bulletCount;
	}

	private float CalculateHealth(){
		return currentHealth;
	}

	private void Event_Die(){
		healthText.text = ("  " + currentHealth + "/" + maxHealth);
		Destroy (gameObject);
	}

	private void Event_Damaged(int damage){
		currentHealth -= damage;
		healthText.text = ("  " + currentHealth + "/" + maxHealth);
		healthBar.value = CalculateHealth ();
	}

	private void PlayerMovement(){
		
		if (Input.GetAxisRaw("Horizontal") > 0f ) {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            anim.SetTrigger("01_characterMoving");
			lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
			return;
		}
		else if(Input.GetAxisRaw("Horizontal") < 0f) {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            anim.SetTrigger("01_characterMoving");
			lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
			return;
		}
		 else if (Input.GetAxisRaw("Vertical") > 0f)
		{
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            anim.SetTrigger("01_characterMoving");
			lastMove = new Vector2(0, Input.GetAxisRaw("Vertical"));
			return;
		}
		else if(Input.GetAxisRaw("Vertical") < 0f){
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            anim.SetTrigger("01_characterMoving");
			lastMove = new Vector2(0, Input.GetAxisRaw("Vertical"));
			return;
		}

	}
}
