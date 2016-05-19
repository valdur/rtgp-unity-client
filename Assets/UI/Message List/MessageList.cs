using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
using Wtg.DataModel;

public class MessageList : MonoBehaviour {

    public MessagePlate messagePlateWidget;
    public Transform listContent;

    public void Start() {
        foreach (var msg in MeteorAccess.instance.GetMessages()) {
            CreateMessage(msg);
        }
        MeteorAccess.instance.MessageAddedEvent += CreateOrUpdateMessage;
        MeteorAccess.instance.MessageChangedEvent += CreateOrUpdateMessage;
        MeteorAccess.instance.MessageRemovedEvent += RemoveMessage;
    }

    private void CreateOrUpdateMessage(MessageData msg) {
        var tr = listContent.Find(msg._id);
        if (tr)
            tr.GetComponent<MessagePlate>().Load(msg);
        else
            CreateMessage(msg);
    }

    private void CreateMessage(MessageData msg) {
        var uw = Instantiate(messagePlateWidget) as MessagePlate;
        uw.Load(msg);
        uw.name = msg._id;
        uw.transform.SetParent(listContent);
    }

    private void RemoveMessage(string _id) {
        var tr = listContent.Find(_id);
        Destroy(tr.gameObject);
    }
}
