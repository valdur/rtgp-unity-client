using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using Wtg.DataModel;

namespace Wtg.MapEditor {
    public class RegionProperties : AbstractProperties {

        public InputField nameText;

        public InputField descText;

        private RegionData region;

        protected override void Awake() {
            base.Awake();
            nameText.onEndEdit.AddListener(EndEditNameHandler);
            descText.onEndEdit.AddListener(EndEditDescHandler);
        }

        protected override bool ShouldShow() {
            return map.selectedConnections.Count == 0 && map.selectedRegions.Count == 1;
        }

        protected override void Load() {
            region = map.GetRegion(map.selectedRegions[0]);
            nameText.text = region.name;
            descText.text = region.description;
        }

        private void EndEditDescHandler(string arg0) {
            region.description = arg0;
            map.NotifyRegionUpdated(region);
        }

        private void EndEditNameHandler(string arg0) {
            region.name = arg0;
            map.NotifyRegionUpdated(region);
        }
    }
}