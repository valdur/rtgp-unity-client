using UnityEngine;
using System.Collections;
using Meteor;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wtg.DataModel {
    public class RegionData : MongoDocument {

        public static string[] areaTypeValues = new string[] {
            "normal",
            "rough",
            "water"
        };

        public string name;
        public string description;
        public string areaType;
        public float x;
        public float y;

        [JsonIgnore]
        public Vector3 position {
            get {
                return new Vector3(x, y);
            }
            set {
                x = value.x;
                y = value.y;
            }
        }
    }
}