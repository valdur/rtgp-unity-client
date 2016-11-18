using UnityEngine;
using System.Collections;
using Wtg.MapEditor;
using Wtg.DataModel;
using System.Linq;

public class MapWidget : MonoBehaviour {

    [SerializeField]
    private MapController mapControllerPrefab;
    [SerializeField]
    private MapFileOperations fileOperationsPrefab;

    [SerializeField]
    private RectTransform spawneesParent;

    private MapController mapController;
    private MapFileOperations fileOperations;

    void Start() {
        MakeFullWidget(transform as RectTransform);

        if (MeteorAccess.instance.IsAdmin())
            SpawnFileOperations();
        SpawnMap();
        if (MeteorAccess.instance.IsAdmin())
            fileOperations.mapController = mapController;
    }

    private void SpawnFileOperations() {
        fileOperations = Instantiate(fileOperationsPrefab);
        var rect = fileOperations.transform as RectTransform;
        rect.SetParent(spawneesParent);
        fileOperations.filePicker = Popups.instance.filePicker;
    }

    private void SpawnMap() {
        mapController = Instantiate(mapControllerPrefab);
        var rect = mapController.transform as RectTransform;
        rect.SetParent(spawneesParent);

        mapController.Load(
            MeteorAccess.instance.gameAreas.GetAll(),
            MeteorAccess.instance.areaConnections.GetAll(),
            MeteorAccess.instance.users.GetAll()
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
