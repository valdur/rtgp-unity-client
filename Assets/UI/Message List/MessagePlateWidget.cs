using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Rtgp.DataModel;

public class MessagePlateWidget : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

	[System.NonSerialized]
	public MessageData messageData;
	public Text label;
	Dragling mDragling;

	internal void Load(MessageData message) {
		messageData = message;
		label.text = message.content;
	}

	public void OnBeginDrag(PointerEventData eventData) {
		mDragling = GuiController.instance.draglingPrefab.Create(messageData.content, eventData.position);
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
