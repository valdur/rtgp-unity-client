using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Meteor;

public class UserData : MongoDocument {
    public string username;
    public string email;
    public Profile profile;

    public class Profile {
        public string role;
    }
}
