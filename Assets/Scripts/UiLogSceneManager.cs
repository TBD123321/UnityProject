using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiLogSceneManager : MonoBehaviour {

	public Transform target;
	public float speed;

	private InputField loginField;

	// Use this for initialization
	void Start () {
		loginField = GetComponentInChildren<InputField> ();
		var submitEvent = new InputField.SubmitEvent ();
		submitEvent.AddListener (Event_AcceptClick);
		loginField.onEndEdit = submitEvent;
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		gameObject.transform.position = Vector3.MoveTowards (transform.position,target.position,step);
	}

	public void Event_AcceptClick(string arg0){
		PlayerPrefs.SetString ("PlayerName",arg0);
		SceneManager.LoadScene ("MainMenu");
	}
}
