using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActiveUserWidget : MonoBehaviour, IDropHandler {

    public static ActiveUserWidget instance;

    public Text nameLabel;
    public Button logOutButton;

    // Use this for initialization
    void Start() {
        instance = this;
        Load(null);
        logOutButton.onClick.AddListener(Logout);
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnDrop(PointerEventData eventData) {
        if (!eventData.pointerDrag)
            return;
        UserPlate upw = eventData.pointerDrag.GetComponent<UserPlate>();
        if (upw) {
            Load(upw.userData);
            return;
        }
    }

    private void Load(UserData userData) {
        logOutButton.gameObject.SetActive(userData != null);
        if (userData != null) {
            nameLabel.text = userData.username;
        } else {
            nameLabel.text = "Not logged in";
        }
    }

    public void Logout() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LogoutSuccessHandler() {

    }

    public void LogoutFailHandler() {

    }

}
