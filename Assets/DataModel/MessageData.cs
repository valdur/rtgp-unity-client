using UnityEngine;
using System.Collections;
using Meteor;

namespace Wtg.DataModel {
    public class MessageData : MongoDocument {

        public string sender;
        public string senderName;
        public string recipent;
        public string recipentName;

        public string content;
        public System.DateTime createdAt;
        public System.DateTime updatedAt;
    }
}