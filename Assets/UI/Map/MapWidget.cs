using UnityEngine;
using System.Collections;
using Wtg.MapEditor;

public class MapWidget : MonoBehaviour {

    [SerializeField]
    private MapController mapControllerPrefab;
    private MapController mapController;

	void Start () {
        MakeFullWidget(transform as RectTransform);
        SpawnMap();
    }

    private void SpawnMap() {
        mapController = Instantiate(mapControllerPrefab);
        var rect = mapController.transform as RectTransform;
        rect.SetParent(transform);
        MakeFullWidget(rect);
    }

    private static void MakeFullWidget(RectTransform rect) {
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;
        rect.pivot = Vector2.one / 2;
    }
}
