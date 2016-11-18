using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using Wtg.DataModel;
using System.Linq;
using System.Collections.Generic;

namespace Wtg.MapEditor {
    public class RegionProperties : AbstractProperties {

        [SerializeField]
        private InputField nameText;
        [SerializeField]
        private InputField descText;
        [SerializeField]
        private Dropdown typeDropdown;
        [SerializeField]
        private Dropdown ownerDropdown;

        private GameAreaData region;

        private List<UserData> users;

        protected override void Awake() {
            base.Awake();
            nameText.onEndEdit.AddListener(NameEditedHandler);
            descText.onEndEdit.AddListener(DescEditedHandler);
            typeDropdown.AddOptions(GameAreaData.areaTypeValues.Select(x => new Dropdown.OptionData(x)).ToList());
            typeDropdown.onValueChanged.AddListener(TypeEditedHandler);

            ownerDropdown.onValueChanged.AddListener(OwnerEditedHandler);

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

            ownerDropdown.ClearOptions();
            users = map.GetUsers().ToList();
            ownerDropdown.options.Add(new Dropdown.OptionData("None"));
            ownerDropdown.AddOptions(users.Select(x => new Dropdown.OptionData(x.username)).ToList());
            ownerDropdown.value = users.FindIndex(x => x._id == region.owner) + 1;
            ownerDropdown.interactable = map.IsEditMode();
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

        private void OwnerEditedHandler(int ownerIndex) {
            if (ownerIndex == 0)
                region.owner = string.Empty;
            else
                region.owner = users[ownerIndex - 1]._id;
            map.NotifyRegionUpdated(region);
        }
    }
}