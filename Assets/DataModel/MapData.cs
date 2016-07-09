using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Wtg.DataModel {
    [System.Serializable]
    public class MapData {
        public string bgFilename;
        public List<GameAreaData> gameAreas;
        public List<AreaConnectionData> areaConnections;
    }
}
