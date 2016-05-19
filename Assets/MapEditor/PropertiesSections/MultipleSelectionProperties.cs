using UnityEngine;
using System.Collections;
using System;

namespace Wtg.MapEditor {
    public class MultipleSelectionProperties : AbstractProperties {
        protected override void Load() {
            
        }

        protected override bool ShouldShow() {
            var count = map.selectedConnections.Count + map.selectedRegions.Count;
            return count > 1;
        }
    }
}