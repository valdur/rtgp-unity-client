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
            t.transform.SetParent(transform);
            var r = t.GetComponent<RectTransform>();
            r.offsetMin = r.offsetMax = Vector2.zero;
            return;
        }

        UserPlate upw = eventData.pointerDrag.GetComponent<UserPlate>();
        if (upw) {
            var t = Instantiate(ViewsController.instance.userPagePrefab) as UserPage;
            t.transform.SetParent(transform);
            var r = t.GetComponent<RectTransform>();
            r.offsetMin = r.offsetMax = Vector2.zero;
            t.Load(upw.userData);
        }

        MessagePlate mp = eventData.pointerDrag.GetComponent<MessagePlate>();
        if (mp) {
            var t = Instantiate(ViewsController.instance.messagePagePrefab) as MessagePage;
            t.transform.SetParent(transform);
            var r = t.GetComponent<RectTransform>();
            r.offsetMin = r.offsetMax = Vector2.zero;
            t.Load(mp.messageData);
        }
    }
}
