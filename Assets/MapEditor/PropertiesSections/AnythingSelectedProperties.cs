using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace Wtg.MapEditor {
    public class AnythingSelectedProperties : AbstractProperties {

        [SerializeField]
        public Button deleteButton;

        void Start() {
            deleteButton.onClick.AddListener(DeleteClickHandler);
        }

        void DeleteClickHandler() {
            map.DeleteSelectedObjects();
        }

        protected override bool ShouldShow() {
            if (map.IsViewMode())
                return false;
            return map.selectedConnections.Count > 0 || map.selectedRegions.Count > 0;
        }

        protected override void Load() {
            
        }
    }
}