using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using Wtg.DataModel;

namespace Wtg.MapEditor {
    public class TwoRegionsProperties : AbstractProperties {

        [SerializeField]
        private Button connectButton;

        private GameAreaData firstRegion;
        private GameAreaData secondRegion;

        protected override void Awake() {
            base.Awake();
            this.connectButton.onClick.AddListener(ConnectClickHandler);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                ConnectClickHandler();
            }
        }

        void ConnectClickHandler() {
            map.CreateConnection(firstRegion, secondRegion);
            SetupButtonVisibility();
        }

        protected override void Load() {
            firstRegion = map.GetRegion(map.selectedRegions[0]);
            secondRegion = map.GetRegion(map.selectedRegions[1]);
            SetupButtonVisibility();
        }

        private void SetupButtonVisibility() {
            connectButton.gameObject.SetActive(!map.AreRegionsConnected(firstRegion._id, secondRegion._id));
        }

        protected override bool ShouldShow() {
            if (map.IsViewMode())
                return false;
            return map.selectedRegions.Count == 2 && map.selectedConnections.Count == 0;
        }
    }
}