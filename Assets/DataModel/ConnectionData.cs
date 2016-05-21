using UnityEngine;
using System.Collections;
using Meteor;

namespace Wtg.DataModel {
    public class ConnectionData : MongoDocument {

        public static string[] transportValues = new string[] {
            "land",
            "water",
            "air"
        };

        public string firstRegionId;
        public string secondRegionId;
        public int distance;
        public string transport;
    }
}
