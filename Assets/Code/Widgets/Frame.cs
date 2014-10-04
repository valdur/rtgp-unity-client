using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Frame : MonoBehaviour, IDropHandler {

	public void OnDrop(PointerEventData eventData) {
		if (!eventData.pointerDrag)
			return;
		BookmarkWidget bmw = eventData.pointerDrag.GetComponent<BookmarkWidget>();
		if (bmw) {
			var t = Instantiate(bmw.prefab) as Transform;
			t.transform.parent = transform;
			var r = t.GetComponent<RectTransform>();
			r.offsetMin = r.offsetMax = Vector2.zero;
			return;
		}
		UserPlateWidget upw = eventData.pointerDrag.GetComponent<UserPlateWidget>();
		if (upw) {
			var t = Instantiate(GuiController.instance.userDataWidgetPrefab) as UserDataWidget;
			t.transform.parent = transform;
			var r = t.GetComponent<RectTransform>();
			r.offsetMin = r.offsetMax = Vector2.zero;
			t.Load(upw.userData);
		}
	}
}
