using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class BookmarkWidget : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {

	public Transform prefab;
	public Text text;

	Dragling mDragling;

	public void OnBeginDrag(PointerEventData eventData) {
		mDragling = ViewsController.instance.draglingPrefab.Create(prefab.name, eventData.position);
	}

	public void OnDrag(PointerEventData eventData) {
		mDragling.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData) {
		if (mDragling)
			Destroy(mDragling.gameObject);
		mDragling = null;
	}

    public void OnPointerClick(PointerEventData eventData) {
        Frame.activeFrame.LoadFromBookmarkWidget(this);
    }
}
