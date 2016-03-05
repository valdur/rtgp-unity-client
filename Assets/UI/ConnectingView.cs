using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConnectingView : MonoBehaviour {

    [SerializeField]
    Text failLabel;
    [SerializeField]
    Button retryButton;

    public void Start() {
        ConnectOrReconnect();
        retryButton.onClick.AddListener(ConnectOrReconnect);
    }

    public void ConnectOrReconnect() {
        failLabel.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        MeteorDAO.instance.Connect(ConnectSuccessful, ConnectFail);
    }

    public void ConnectSuccessful() {
        gameObject.SetActive(false);
    }

    public void ConnectFail() {
        failLabel.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
    }
}
