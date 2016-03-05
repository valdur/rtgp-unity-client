using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UserDataWidget : MonoBehaviour, IDropHandler {
	public Text nameLabel;
	public Text idLabel;
	public Text emailLabel;
	public Text roleLabel;
	UserData mUserData;

	public void Load(UserData data ) {
		mUserData = data;
		nameLabel.text = data.username;
		idLabel.text = data._id;
		emailLabel.text = data.email;
		roleLabel.text = data.role;
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
}