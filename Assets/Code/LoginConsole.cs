using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginConsole : MonoBehaviour {

	public InputField emailInput;
	public InputField passInput;
	public Button loginButton;

	public void Start() {
		loginButton.onClick.AddListener(Login);
	}

	public void Login() {
		//loginButton.interactable = false;
		NetworkingManager.instance.Login(emailInput.text, passInput.text);
	}

	public void LoginSuccessful() {
		gameObject.SetActive(false);
	}
}
