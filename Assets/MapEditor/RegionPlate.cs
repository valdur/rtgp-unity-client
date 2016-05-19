using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Assertions;
using Wtg.DataModel;

namespace Wtg.MapEditor {
    public class RegionPlate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {

        [SerializeField]
        private Text text;
        [SerializeField]
        private Image bg;

        private bool _selected;
        private bool _potentialClick;

        void Awake() {
            Assert.IsNotNull(text, "Region.text is null");
            Assert.IsNotNull(bg, "Region.bg is null");
        }

        public bool selected {
            get {
                return _selected;
            }
            set {
                _selected = value;
                SetupColor();
            }
        }

        public RegionData regionData { get; private set; }

        private MapMainController map;
        private MapCanvas mapCanvas;

        public void Load(MapCanvas mapCanvas , RegionData region) {
            this.transform.localPosition = region.position;
            this.regionData = region;
            this.text.text = regionData.name;
            this.mapCanvas = mapCanvas;
            this.map = mapCanvas.mapMainController;
            SetupColor();
        }

        public void OnPointerDown(PointerEventData eventData) {
            _potentialClick = true;
        }

        public void OnPointerUp(PointerEventData eventData) {
            if (_potentialClick) {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                    if (_selected) {
                        map.RemoveFromSelection(regionData);
                    } else {
                        map.AddToSelection(regionData);
                    }
                } else {
                    map.SelectSingle(regionData);
                }
                _potentialClick = false;
            }
        }

        void SetupColor() {
            bg.color = _selected ? mapCanvas.selectedRegionColor : mapCanvas.deselectedRegionColor;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            _potentialClick = false;
        }

        public void OnDrag(PointerEventData eventData) {
            regionData.position = transform.parent.InverseTransformPoint(eventData.position);
            map.NotifyRegionUpdated(regionData);
        }

        public void OnEndDrag(PointerEventData eventData) {
        }
    }
}