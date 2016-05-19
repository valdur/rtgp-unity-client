using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Wtg.DataModel;
using Meteor.Extensions;
using System;

public class MeteorAccess : MonoBehaviour {

    public string serverAddress = "ws://seriousdragon.com:3000/websocket";
    public static MeteorAccess instance;

    void Awake() {
        instance = this;
    }

    private Meteor.Collection<MessageData> messagesCollection;
    private Meteor.Collection<UserData> usersCollection;
    private Meteor.Cursor<MessageData> messagesCursor;
    private Meteor.Cursor<UserData> usersCursor;

    IEnumerator ConnectCor(System.Action successCallback, System.Action failCallback) {
        yield return Meteor.Connection.Connect(serverAddress);
        if (Meteor.Connection.Connected) {

            SetupUsersAccess();
            SetupMessagesAccess();

            successCallback();
        } else {
            failCallback();
        }
    }

    private void SetupUsersAccess() {
        usersCollection = new Meteor.Collection<UserData>("users");
        usersCursor = usersCollection.Find();
        usersCursor.Observe(
            (addedId, addedObject) => UserAddedEvent(addedObject),
            (changedId, changedObject, fuzzyDict, fuzzyStringList) => UserChangedEvent(changedObject),
            (removedId) => UserRemovedEvent(removedId));
    }

    private void SetupMessagesAccess() {
        messagesCollection = new Meteor.Collection<MessageData>("messages");
        messagesCursor = messagesCollection.Find(x => x.recipent == Meteor.Accounts.UserId || x.sender == Meteor.Accounts.UserId);
        messagesCursor.Observe(
            (addedId, addedObject) => MessageAddedEvent(addedObject),
            (changedId, changedObject, fuzzyDict, fuzzyStringList) => MessageChangedEvent(changedObject),
            (removedId) => MessageRemovedEvent(removedId));
    }

    IEnumerator LoginCor(string email, string password, System.Action successCallback, System.Action failCallback) {
        yield return (Coroutine)Meteor.Accounts.LoginWith(email, password);

        if (Meteor.Accounts.IsLoggedIn) {
            successCallback();
        } else {
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

    public IEnumerable<UserData> GetUsers() {
        return usersCursor.Fetch();
    }

    public void SetUser(UserData ud) {

    }


    public System.Action<UserData> UserAddedEvent = delegate { };
    public System.Action<UserData> UserChangedEvent = delegate { };
    public System.Action<string> UserRemovedEvent = delegate { };


    public IEnumerable<MessageData> GetMessages() {
        return messagesCursor.Fetch();
    }
    public System.Action<MessageData> MessageAddedEvent = delegate { };
    public System.Action<MessageData> MessageChangedEvent = delegate { };
    public System.Action<string> MessageRemovedEvent = delegate { };

}