using UnityEngine;
using System.Collections;
using System;
using Wtg.DataModel;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Wtg.MapEditor {

    public class ConnectionPlate : MonoBehaviour, IPointerClickHandler {

        [SerializeField]
        Image bg;

        MapCanvas canvas;
        MapController map;
        public ConnectionData data { get; private set; }
        RectTransform rectTransform;
        private bool _selected;

        public bool selected {
            get {
                return _selected;
            }
            set {
                _selected = value;
                SetupColor();
            }
        }

        void SetupColor() {
            bg.color = _selected ? canvas.selectedConnectionColor : canvas.deselectedConnectionColor;
        }

        internal void Load(MapCanvas mapCanvas, ConnectionData connection) {
            this.canvas = mapCanvas;
            this.data = connection;
            this.map = canvas.mapMainController;
            this.rectTransform = transform as RectTransform;

            map.RegionUpdatedEvent += RegionUpdatedHandler;
            RecalculatePosition();
        }

        void OnDestroy() {
            map.RegionUpdatedEvent -= RegionUpdatedHandler;
        }

        void RecalculatePosition() {
            var p1 = map.GetRegion(data.firstRegionId).position;
            var p2 = map.GetRegion(data.secondRegionId).position;

            rectTransform.localPosition = p1;

            var w = rectTransform.sizeDelta.x;
            var h = Vector3.Distance(p1, p2);
            rectTransform.sizeDelta = new Vector2(w, h);
            rectTransform.rotation = Quaternion.FromToRotation(Vector3.up, p2 - p1);

        }

        private void RegionUpdatedHandler(RegionData region) {
            if (data.firstRegionId == region._id || data.secondRegionId == region._id) {
                RecalculatePosition();
            }
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                if (_selected) {
                    map.RemoveFromSelection(data);
                } else {
                    map.AddToSelection(data);
                }
            } else {
                map.SelectSingle(data);
            }
        }
    }
}