using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using Wtg.DataModel;
using System.Linq;

namespace Wtg.MapEditor {

    public class ConnectionProperties : AbstractProperties {

        [SerializeField]
        private Text firstRegionName;
        [SerializeField]
        private Text secondRegionName;
        [SerializeField]
        Dropdown transportDropdown;
        [SerializeField]
        InputField distanceInputField;

        AreaConnectionData connection;
        GameAreaData firstRegion;
        GameAreaData secondRegion;

        protected override void Awake() {
            base.Awake();
            transportDropdown.AddOptions(AreaConnectionData.transportValues.Select(x => new Dropdown.OptionData(x)).ToList());
            transportDropdown.onValueChanged.AddListener(TransportEditedHandler);
            distanceInputField.onEndEdit.AddListener(DistanceEditedHandler);
        }

        private void DistanceEditedHandler(string value) {
            connection.distance = int.Parse(value);
            map.NotifyConnectionUpdated(connection);
        }

        private void TransportEditedHandler(int value) {
            connection.transport = AreaConnectionData.transportValues[value];
            map.NotifyConnectionUpdated(connection);
        }

        protected override bool ShouldShow() {
            return map.selectedConnections.Count == 1 && map.selectedRegions.Count == 0;
        }

        protected override void Load() {
            connection = map.GetConnection(map.selectedConnections[0]);
            firstRegion = map.GetRegion(connection.firstRegionId);
            secondRegion = map.GetRegion(connection.secondRegionId);
            firstRegionName.text = firstRegion.name;
            secondRegionName.text = secondRegion.name;
            transportDropdown.value = System.Array.IndexOf(AreaConnectionData.transportValues, connection.transport);
            transportDropdown.interactable = map.IsEditMode();
            distanceInputField.text = connection.distance.ToString();
            distanceInputField.interactable = map.IsEditMode();
        }
    }
}