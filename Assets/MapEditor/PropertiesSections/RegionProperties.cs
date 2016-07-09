using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using Wtg.DataModel;
using System.Linq;

namespace Wtg.MapEditor {
    public class RegionProperties : AbstractProperties {

        [SerializeField]
        private InputField nameText;
        [SerializeField]
        private InputField descText;
        [SerializeField]
        private Dropdown typeDropdown;

        private GameAreaData region;

        protected override void Awake() {
            base.Awake();
            nameText.onEndEdit.AddListener(NameEditedHandler);
            descText.onEndEdit.AddListener(DescEditedHandler);
            typeDropdown.AddOptions(GameAreaData.areaTypeValues.Select(x => new Dropdown.OptionData(x)).ToList());
            typeDropdown.onValueChanged.AddListener(TypeEditedHandler);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.F2)) {
                nameText.ActivateInputField();
            }
            if (Input.GetKeyDown(KeyCode.F3)) {
                descText.ActivateInputField();
            }
        }

        protected override bool ShouldShow() {
            return map.selectedConnections.Count == 0 && map.selectedRegions.Count == 1;
        }

        protected override void Load() {
            region = map.GetRegion(map.selectedRegions[0]);
            nameText.text = region.name;
            nameText.interactable = map.IsEditMode();
            descText.text = region.description;
            descText.interactable = map.IsEditMode();
            typeDropdown.value = System.Array.IndexOf(GameAreaData.areaTypeValues, region.areaType);
            typeDropdown.interactable = map.IsEditMode();
        }

        private void DescEditedHandler(string arg0) {
            region.description = arg0;
            map.NotifyRegionUpdated(region);
        }

        private void NameEditedHandler(string arg0) {
            region.name = arg0;
            map.NotifyRegionUpdated(region);
        }

        private void TypeEditedHandler(int typeIndex) {
            region.areaType = GameAreaData.areaTypeValues[typeIndex];
            map.NotifyRegionUpdated(region);
        }
    }
}