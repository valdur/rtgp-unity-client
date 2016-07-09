using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Frame : MonoBehaviour, IDropHandler {

    private Transform spawnedWidget;

    public void OnDrop(PointerEventData eventData) {
        if (!eventData.pointerDrag)
            return;
        BookmarkWidget bmw = eventData.pointerDrag.GetComponent<BookmarkWidget>();
        if (bmw) {
            EnsureUnloaded();
            spawnedWidget = Instantiate(bmw.prefab) as Transform;
            spawnedWidget.transform.SetParent(transform);
            var r = spawnedWidget.GetComponent<RectTransform>();
            r.offsetMin = r.offsetMax = Vector2.zero;
            return;
        }

        // todo: make that stuff below abstract
        UserPlate upw = eventData.pointerDrag.GetComponent<UserPlate>();
        if (upw) {
            EnsureUnloaded();
            var upi = Instantiate(ViewsController.instance.userPagePrefab) as UserPage;
            upi.transform.SetParent(transform);
            spawnedWidget = upi.transform;
            var r = upi.GetComponent<RectTransform>();
            r.offsetMin = r.offsetMax = Vector2.zero;
            upi.Load(upw.userData);
        }

        MessagePlate mpw = eventData.pointerDrag.GetComponent<MessagePlate>();
        if (mpw) {
            EnsureUnloaded();
            var mpi = Instantiate(ViewsController.instance.messagePagePrefab) as MessagePage;
            spawnedWidget = mpi.transform;
            mpi.transform.SetParent(transform);
            var r = mpi.GetComponent<RectTransform>();
            r.offsetMin = r.offsetMax = Vector2.zero;
            mpi.Load(mpw.messageData);
        }
    }

    void EnsureUnloaded() {
        if (spawnedWidget)
            Destroy(spawnedWidget.gameObject);
    }
}
