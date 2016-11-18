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

    [SerializeField]
    Toggle rememberCredentialsToggle;

    const string rememberCredentialsKey = "LoginView.rememberCredentials";
    const string usernameKey = "LoginView.username";
    const string passwordKey = "LoginView.password";

    public void OnEnable() {
        RestoreCredentials();
    }

    private void RestoreCredentials() {
        bool remember = PlayerPrefs.GetInt(rememberCredentialsKey, 0) > 0;
        rememberCredentialsToggle.isOn = remember;
        if (remember) {
            emailInput.text = PlayerPrefs.GetString(usernameKey, string.Empty);
            passInput.text = PlayerPrefs.GetString(passwordKey, string.Empty);
        }
    }

    private void SaveCredentials() {
        bool remember = rememberCredentialsToggle.isOn;

        PlayerPrefs.SetInt(rememberCredentialsKey, remember ? 1 : 0);
        if (remember) {
            PlayerPrefs.SetString(usernameKey, emailInput.text);
            PlayerPrefs.SetString(passwordKey, passInput.text);
        } else {
            PlayerPrefs.DeleteKey(usernameKey);
            PlayerPrefs.DeleteKey(passwordKey);
        }
        PlayerPrefs.Save();
    }

    public void Start() {
        loginFailedLabel.gameObject.SetActive(false);
        loginButton.onClick.AddListener(Login);
    }

    public void Login() {
        loginFailedLabel.gameObject.SetActive(false);
        MeteorAccess.instance.Login(emailInput.text, passInput.text, LoginSuccessfulHandler, LoginFailedHandler);
        SaveCredentials();
    }

    void LoginSuccessfulHandler() {
        ViewsController.instance.ShowView<GameView>();
    }

    void LoginFailedHandler() {
        loginFailedLabel.gameObject.SetActive(true);
    }
}
