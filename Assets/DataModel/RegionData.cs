using UnityEngine;
using System.Collections;
using Meteor;
using System.Collections.Generic;

namespace Wtg.DataModel {
    public class RegionData : MongoDocument {

        public static string[] allowedAreaTypes = new string[] {
            "normal",
            "rough",
            "water"
        };

        public string name;
        public string description;
        public string areaType;
        public string owner; // id
        public string ownerName;
        internal Vector3 position;
    }
}