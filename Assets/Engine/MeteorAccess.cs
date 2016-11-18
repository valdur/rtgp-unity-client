using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Wtg.DataModel;
using Meteor.Extensions;
using System;
using Meteor;

public class MeteorAccess : MonoBehaviour {

    public string serverAddress;
    public static MeteorAccess instance;

    void Awake() {
        instance = this;
    }

    public Collection<MessageData> messages;
    public Collection<UserData> users;
    public Collection<GameAreaData> gameAreas;
    public Collection<AreaConnectionData> areaConnections;
    public Collection<ActiveUnitData> activeUnits;

    IEnumerator ConnectCor(System.Action successCallback, System.Action failCallback) {
        yield return Meteor.Connection.Connect(serverAddress);
        if (Meteor.Connection.Connected) {
            Debug.Log("connected to server");

            successCallback();
        } else {
            failCallback();
        }
    }

    string[] subscriptions = new string[] {
        "allUsers",
        "Messages.myMessages",
        "gameAreas",
        "AreaConnections",
        "ActiveUnits"
    };

    IEnumerator LoginCor(string email, string password, System.Action successCallback, System.Action failCallback) {

        Debug.Log("Logging in");

        yield return (Coroutine)Meteor.Accounts.LoginWith(email, password);

        if (Meteor.Accounts.IsLoggedIn) {

            users = new Collection<UserData>("users");
            messages = new Collection<MessageData>("messages", x => x.recipent == Meteor.Accounts.UserId || x.sender == Meteor.Accounts.UserId);
            gameAreas = new Collection<GameAreaData>("game-areas");
            areaConnections = new Collection<AreaConnectionData>("area-connections");
            activeUnits = new Collection<ActiveUnitData>("active-units");

            foreach (var sub in subscriptions)
                yield return (Coroutine)Meteor.Subscription.Subscribe(sub);

            Debug.Log("Logged In");

            successCallback();
        } else {

            Debug.Log("Not logged in");

            failCallback();
        }
    }

    // API

    public void Connect(System.Action successCallback, System.Action failCallback) {
        StartCoroutine(ConnectCor(successCallback, failCallback));
    }

    public void Login(string email, string password, System.Action successCallback = null, System.Action failCallback = null) {
        StartCoroutine(LoginCor(email, password, successCallback, failCallback));
    }

    public bool IsAdmin() {
        return users.Get(Meteor.Accounts.UserId).profile.role == "admin";
    }

    public class Collection<T> where T : MongoDocument, new() {
        public System.Action<T> AddedEvent = delegate { };
        public System.Action<T> ChangedEvent = delegate { };
        public System.Action<string> RemovedEvent = delegate { };

        private Meteor.Collection<T> collection;
        private Meteor.Cursor<T> cursor;

        public Collection(string collectionName, Func<T, bool> selector = null) {
            collection = new Meteor.Collection<T>(collectionName);
            cursor = collection.Find(selector);
            cursor.Observe(
                (addedId, addedObject) => AddedEvent(addedObject),
                (changedId, changedObject, fuzzyDict, fuzzyStringList) => ChangedEvent(changedObject),
                (removedId) => RemovedEvent(removedId));
        }

        public IEnumerable<T> GetAll() {
            return cursor.Fetch();
        }

        public T Get(string id) {
            return collection.FindOne(id);
        }
    }
}