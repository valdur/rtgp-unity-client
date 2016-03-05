using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Rtgp.DataModel;
using Meteor.Extensions;
using Meteor;

public class MeteorDAO : MonoBehaviour {

    string serverAddress = "ws://192.168.0.107:3000/websocket";
    public static MeteorDAO instance;

    void Awake() {
        instance = this;
    }

    public Collection<MessageData> messages;
    public Collection<UserData> users;

    public void Connect(System.Action successCallback, System.Action failCallback) {
        StartCoroutine(ConnectCor(successCallback, failCallback));
    }

    IEnumerator ConnectCor(System.Action successCallback, System.Action failCallback) {
        yield return Meteor.Connection.Connect(serverAddress);
        if (Meteor.Connection.Connected) {
            successCallback();
        } else {
            failCallback();
        }
    }


    public void Login(string email, string password, System.Action successCallback = null, System.Action failCallback = null) {
        StartCoroutine(LoginCor(email, password, successCallback, failCallback));
    }

    IEnumerator LoginCor(string email, string password, System.Action successCallback, System.Action failCallback) {
        yield return (Coroutine)Meteor.Accounts.LoginWith(email, password);

        messages = new Meteor.Collection<MessageData>("myMessages");
        users = new Collection<UserData>("users");

        if (Meteor.Accounts.IsLoggedIn) {
            Debug.Log("Meteor says it's on");
            successCallback();
        } else {
            failCallback();
        }

        //var observer = collection.Find().Observe(added: (string id, MessageData document) => {
        //    Debug.Log(string.Format("Document added:\n{0}", document.Serialize()));
        //});
    }
}