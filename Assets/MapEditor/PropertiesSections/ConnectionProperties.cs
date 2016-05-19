using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using Wtg.DataModel;

namespace Wtg.MapEditor {

    public class ConnectionProperties : AbstractProperties {

        [SerializeField]
        private Text firstRegionName;
        [SerializeField]
        private Text secondRegionName;

        ConnectionData connection;
        RegionData firstRegion;
        RegionData secondRegion;

        protected override bool ShouldShow() {
            return map.selectedConnections.Count == 1 && map.selectedRegions.Count == 0;
        }

        protected override void Load() {
            connection = map.GetConnection(map.selectedConnections[0]);
            firstRegion = map.GetRegion(connection.firstRegionId);
            secondRegion = map.GetRegion(connection.secondRegionId);
            firstRegionName.text = firstRegion.name;
            secondRegionName.text = secondRegion.name;
        }
    }
}