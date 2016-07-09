using UnityEngine;
using System.Collections;
using Meteor;
using System.Collections.Generic;

namespace Wtg.DataModel {

    public class GameAreaData : MongoDocument {

        public static string[] areaTypeValues = new string[] {
            "normal",
            "rough",
            "water"
        };

        public string name;
        public string description;
        public string areaType;
        public string owner;
        public string ownerName;
        public float x;
        public float y;

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