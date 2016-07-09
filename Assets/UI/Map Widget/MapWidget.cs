using UnityEngine;
using System.Collections;
using Wtg.MapEditor;
using Wtg.DataModel;
using System.Linq;

public class MapWidget : MonoBehaviour {

    [SerializeField]
    private MapController mapControllerPrefab;
    private MapController mapController;

    void Start() {
        MakeFullWidget(transform as RectTransform);
        SpawnMap();
    }

    private void SpawnMap() {
        mapController = Instantiate(mapControllerPrefab);
        var rect = mapController.transform as RectTransform;
        rect.SetParent(transform);
        MakeFullWidget(rect);

        mapController.Load(
            MeteorAccess.instance.gameAreas.Get(),
            MeteorAccess.instance.areaConnections.Get()
        );
        mapController.LoadBackgroundFromUrl("http://seriousdragon.com/~valdur/r2g5-physical.jpg");
        mapController.EnterViewMode();
    }

    private static void MakeFullWidget(RectTransform rect) {
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;
        rect.anchoredPosition = Vector2.zero;
        rect.pivot = Vector2.one / 2;
    }
}
