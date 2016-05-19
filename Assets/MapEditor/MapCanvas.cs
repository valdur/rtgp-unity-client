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
        public Color deselectedRegionColor;

        private List<RegionPlate> regions = new List<RegionPlate>();
        private List<ConnectionPlate> connections = new List<ConnectionPlate>();

        public MapMainController mapMainController;

        private void OnEnable() {
            mapMainController.SelectionChangedEvent += SelectionChangedHandler;
            mapMainController.RegionCreatedEvent += CreateRegion;
            mapMainController.ConnectionCreatedEvent += CreateConnection;

            mapMainController.RegionUpdatedEvent += ReloadRegion;
            mapMainController.RegionDestroyedEvent += DestroyRegion;
        }

        private void OnDisable() {
            mapMainController.SelectionChangedEvent -= SelectionChangedHandler;
        }

        public void SelectionChangedHandler() {
            foreach (var reg in regions) {
                reg.selected = mapMainController.IsRegionSelected(reg.regionData._id);
            }
        }

        void Start() {
            Assert.IsNotNull(regionPrefab, "regionPrefab in MapController is null");
        }


        public void OnPointerClick(PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                DeselectAll();
            } else if (eventData.button == PointerEventData.InputButton.Right) {
                mapMainController.CreateRegion(transform.InverseTransformPoint(eventData.position));
            }
        }

        private void CreateRegion(RegionData region) {
            var ins = Instantiate(regionPrefab) as RegionPlate;
            regions.Add(ins);
            ins.transform.SetParent(transform);
            ins.transform.SetAsLastSibling();
            ins.Load(this, region);
        }

        private void ReloadRegion(RegionData regionData) {
            var reg = regions.Find(x => x.regionData == regionData);
            reg.Load(this, regionData);
        }

        private void DestroyRegion(RegionData region) {
            var reg = regions.Find(x => x.regionData._id == region._id);
            regions.Remove(reg);
            Destroy(reg.gameObject);
        }

        private void CreateConnection(ConnectionData connection) {
            var ins = Instantiate(connectionPrefab) as ConnectionPlate;
            connections.Add(ins);
            ins.transform.SetParent(transform);
            ins.transform.SetAsFirstSibling();
            ins.Load(this, connection);
        }

        private void DestroyConnection(ConnectionData connection) {
            var con = connections.Find(x => x.data._id == connection._id);
            connections.Remove(con);
            Destroy(con.gameObject);
        }

        public void SelectSingle(RegionPlate regionToSelect) {
            mapMainController.SelectSingle(regionToSelect.regionData);
        }

        public void DeselectAll() {
            mapMainController.ClearSelection();
        }
    }
}