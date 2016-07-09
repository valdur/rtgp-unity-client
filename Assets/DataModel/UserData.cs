using UnityEngine;
using System.Collections;
using Meteor;

namespace Wtg.DataModel {
    public class UserData : MongoDocument {
        public string username;
        public string email;
        public Profile profile;

        public class Profile {
            public string role;
        }
    }
}