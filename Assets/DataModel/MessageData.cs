using UnityEngine;
using System.Collections;
using Meteor;

namespace Rtgp.DataModel {
    public class MessageData : MongoDocument {
        public string id;
        public string sentMessageUserId;
        public string content;
        public System.DateTime createdAt;
        public System.DateTime updatedAt;
    }
}