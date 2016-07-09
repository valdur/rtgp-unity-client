using UnityEngine;
using System.Collections;
using Meteor;
using System.Collections.Generic;

namespace Wtg.DataModel {

    public class ActiveUnitData : MongoDocument {

        public string name;
        public string description;
        public Stats stats;
        public Props props;
        public string owner;
        public string ownerName;
        public string factionName;
        public string location;
        public string locationName;

        public struct Stats {
            public int white;
            public int green;
            public int red;
            public int black;
            public int blue;
        }

        public struct Props {
            public bool cavalary;
            public bool infantry;
            public bool ranged;
            public bool flying;
            public bool flamebreathing;
            public bool monster;
            public bool slayer;
            public bool necro;
            public bool machine;
            public bool demon;
            public bool sealer;
        }
    }
}