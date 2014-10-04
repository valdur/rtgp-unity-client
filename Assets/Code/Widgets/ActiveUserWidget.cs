using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
		UserPlateWidget upw = eventData.pointerDrag.GetComponent<UserPlateWidget>();
		if (upw) {
			Load(upw.userData);
			return;
		}
	}

	private void Load(UserData userData) {
		GuiController.instance.currentUser = userData;
		logOutButton.gameObject.SetActive(userData != null);
		if (userData != null) {
			nameLabel.text = userData.name;
		} else {
			nameLabel.text = "Not logged in";
		}
	}

	public void Logout() {
		Load(null);
	}
}
