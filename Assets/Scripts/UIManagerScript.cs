using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManagerScript : MonoBehaviour {

	public Animator contentPanelUp;
	public Animator contentPanelDown;
	public Text nameText;

	void Start()
	{
		RectTransform transform = contentPanelUp.gameObject.transform as RectTransform;        
		Vector2 position = transform.anchoredPosition;
		position.y -= transform.rect.height;
		transform.anchoredPosition = position;

	    transform = contentPanelDown.gameObject.transform as RectTransform;        
		position = transform.anchoredPosition;
		position.y -= transform.rect.height;
		transform.anchoredPosition = position;
	}

	public void ToggleUpMenu()
	{
		contentPanelUp.enabled = true;

		bool isHidden = contentPanelUp.GetBool("isHidden");
		contentPanelUp.SetBool("isHidden", !isHidden);
	}

	public void ToggleDownMenu()
	{
		contentPanelDown.enabled = true;

		bool isHidden = contentPanelDown.GetBool("isHidden");
		contentPanelDown.SetBool("isHidden", !isHidden);
	}


	public void Event_StartGame()
	{
		SceneManager.LoadScene (1);
	}

	public void Event_ExitGame(){
		Application.Quit ();
	}
}
