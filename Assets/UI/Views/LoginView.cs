using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginView : View {

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

        MeteorAccess.instance.Login("valdur", "fdsafdsa", LoginSuccessfulHandler, LoginFailedHandler);
    }

    public void Login() {
        loginFailedLabel.gameObject.SetActive(false);
        MeteorAccess.instance.Login(emailInput.text, passInput.text, LoginSuccessfulHandler, LoginFailedHandler);
    }

    void LoginSuccessfulHandler() {
        ViewsController.instance.ShowView<GameView>();
    }

    void LoginFailedHandler() {
        loginFailedLabel.gameObject.SetActive(true);
    }
}
