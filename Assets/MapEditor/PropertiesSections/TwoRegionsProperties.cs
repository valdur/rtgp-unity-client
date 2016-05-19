using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using Wtg.DataModel;

namespace Wtg.MapEditor {
    public class TwoRegionsProperties : AbstractProperties {

        [SerializeField]
        private Button connectButton;

        private RegionData firstRegion;
        private RegionData secondRegion;

        protected override void Awake() {
            base.Awake();
            this.connectButton.onClick.AddListener(ConnectClickHandler);
        }

        void ConnectClickHandler() {
            map.CreateConnection(firstRegion, secondRegion);
        }

        protected override void Load() {
            firstRegion = map.GetRegion(map.selectedRegions[0]);
            secondRegion = map.GetRegion(map.selectedRegions[1]);
        }

        protected override bool ShouldShow() {
            return map.selectedRegions.Count == 2 && map.selectedConnections.Count == 0;
        }
    }
}