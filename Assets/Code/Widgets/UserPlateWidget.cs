using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UserPlateWidget : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

	[System.NonSerialized]
	public UserData userData;
	public Text label;
	Dragling mDragling;

	public void Load(UserData user) {
		userData = user;
		label.text = user.name;
	}

	public void OnBeginDrag(PointerEventData eventData) {
		mDragling = GuiController.instance.draglingPrefab.Create(userData.name, eventData.position);
	}

	public void OnDrag(PointerEventData eventData) {
		mDragling.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData) {
		if (mDragling)
			Destroy(mDragling.gameObject);
		mDragling = null;
	}
}
