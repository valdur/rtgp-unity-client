using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginView : MonoBehaviour {

    [SerializeField]
    InputField emailInput;

    [SerializeField]
    InputField passInput;

    [SerializeField]
    Button loginButton;

    [SerializeField]
    Text loginFailedLabel;

    public void Start() {
        loginFailedLabel.gameObject.SetActive(false);
        loginButton.onClick.AddListener(Login);
    }

    public void Login() {
        loginFailedLabel.gameObject.SetActive(false);
        MeteorDAO.instance.Login(emailInput.text, passInput.text, LoginSuccessfulHandler, LoginFailedHandler);
    }

    void LoginSuccessfulHandler() {
        gameObject.SetActive(false);
    }

    void LoginFailedHandler() {
        loginFailedLabel.gameObject.SetActive(true);
    }
}
