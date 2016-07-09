using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using System;
using Wtg.DataModel;

namespace Wtg.MapEditor {
    public class MapCanvas : MonoBehaviour, IPointerClickHandler {

        public RegionPlate regionPrefab;
        public ConnectionPlate connectionPrefab;

        public Color selectedRegionColor;
        public Color[] deselectedRegionColors;

        public Color selectedConnectionColor;
        public Color[] deselectedConnectionColors;

        public int editConnectionThickness = 20;
        public int viewConnectionThickness = 8;

        private List<RegionPlate> regions = new List<RegionPlate>();
        private List<ConnectionPlate> connections = new List<ConnectionPlate>();

        public MapController mapController;

        private void OnEnable() {
            mapController.SelectionChangedEvent += SelectionChangedHandler;

            mapController.ConnectionCreatedEvent += CreateConnection;
            mapController.ConnectionDestroyedEvent += DestroyConnection;

            mapController.RegionCreatedEvent += CreateRegion;
            mapController.RegionUpdatedEvent += ReloadRegion;
            mapController.RegionDestroyedEvent += DestroyRegion;
        }

        private void OnDisable() {
            mapController.SelectionChangedEvent -= SelectionChangedHandler;
        }

        public void SelectionChangedHandler() {
            foreach (var reg in regions) {
                reg.selected = mapController.IsRegionSelected(reg.data._id);
            }
            foreach (var con in connections) {
                con.selected = mapController.IsConnectionSelected(con.data._id);
            }
        }

        void Start() {
            Assert.IsNotNull(regionPrefab, "regionPrefab in MapController is null");
        }

        void Update() {
            var sw = Input.GetAxis("Mouse ScrollWheel");
            var sc = Mathf.Clamp(transform.localScale.x + sw, 0.2f, 5f);

            transform.localScale = Vector3.one * sc;

        }


        public void OnPointerClick(PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                DeselectAll();
            } else if (eventData.button == PointerEventData.InputButton.Right) {
                if (mapController.IsEditMode())
                    mapController.CreateRegion(transform.InverseTransformPoint(eventData.position));
            }
        }

        private void CreateRegion(GameAreaData region) {
            var ins = Instantiate(regionPrefab) as RegionPlate;
            regions.Add(ins);
            ins.transform.SetParent(transform, false);
            ins.transform.SetAsLastSibling();
            ins.Load(this, region);
        }

        private void ReloadRegion(GameAreaData regionData) {
            var reg = regions.Find(x => x.data == regionData);
            reg.Load(this, regionData);
        }

        private void DestroyRegion(GameAreaData region) {
            var reg = regions.Find(x => x.data._id == region._id);
            regions.Remove(reg);
            Destroy(reg.gameObject);
        }

        private void CreateConnection(AreaConnectionData connection) {
            var ins = Instantiate(connectionPrefab) as ConnectionPlate;
            connections.Add(ins);
            ins.transform.SetParent(transform, false);
            ins.transform.SetAsFirstSibling();
            ins.Load(this, connection);
        }

        private void DestroyConnection(AreaConnectionData connection) {
            var con = connections.Find(x => x.data._id == connection._id);
            connections.Remove(con);
            Destroy(con.gameObject);
        }

        public void SelectSingle(RegionPlate regionToSelect) {
            mapController.SelectSingle(regionToSelect.data);
        }

        public void DeselectAll() {
            mapController.ClearSelection();
        }
    }
}