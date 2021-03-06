﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Assertions;
using Wtg.DataModel;

namespace Wtg.MapEditor {
    public class RegionPlate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler {

        [SerializeField]
        private Text text;
        [SerializeField]
        private Image bg;
        [SerializeField]
        private Image outline;

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

        public GameAreaData data { get; private set; }

        private MapController map;
        private MapCanvas canvas;

        public void Load(MapCanvas mapCanvas, GameAreaData region) {
            this.transform.localPosition = region.position;
            this.data = region;
            this.text.text = data.name;
            this.canvas = mapCanvas;
            this.map = mapCanvas.mapController;
            SetupColor();
        }

        public void OnPointerDown(PointerEventData eventData) {
            _potentialClick = true;
        }

        public void OnPointerUp(PointerEventData eventData) {
            if (_potentialClick) {
                HandleClick();
                _potentialClick = false;
            }
        }

        private void HandleClick() {
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

        void SetupColor() {
            if (_selected) {
                outline.color = canvas.selectedRegionColor;
            } else {
                int index = System.Array.IndexOf(GameAreaData.areaTypeValues, data.areaType);
                outline.color = canvas.deselectedRegionColors[index];
            }

            var owner = map.GetUser(data.owner);
            Color col = Color.white;
            if (owner != null) {
                bool res = ColorUtility.TryParseHtmlString(owner.profile.mainColor, out col);
                if (!res) {
                    var i = owner._id.GetHashCode() & 0x00FFFFFF;
                    ColorUtility.TryParseHtmlString("#" + i.ToString("X6"), out col);
                }

            }
            bg.color = col;

            text.color = ColorUtils.GetTextColorForBackground(bg.color);
        }

        public void OnBeginDrag(PointerEventData eventData) {
            _potentialClick = false;
        }

        public void OnDrag(PointerEventData eventData) {
            if (map.IsEditMode()) {
                data.position = transform.parent.InverseTransformPoint(eventData.position);
                map.NotifyRegionUpdated(data);
            }
        }
    }
}