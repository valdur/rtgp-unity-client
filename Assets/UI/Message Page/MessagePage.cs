using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Rtgp.DataModel;

public class MessagePage : MonoBehaviour {

    public Text senderLabel;
    public Text recipantLabel;
    public Text contentLabel;
    MessageData mMessageData;

    bool initialized;

    public void Load(MessageData data) {
        mMessageData = data;
        senderLabel.text = data.senderName;
        recipantLabel.text = data.recipentName;
        contentLabel.text = data.content;
        if (!initialized) {
            initialized = true;
            MeteorAccess.instance.MessageChangedEvent += ReloadIfMatching;
        }
    }

    void ReloadIfMatching(MessageData newMessage) {
        if (newMessage._id == mMessageData._id)
            Load(newMessage);
    }

    void OnDestroy() {
        MeteorAccess.instance.MessageChangedEvent -= ReloadIfMatching;
    }

    public void OnDrop(PointerEventData eventData) {
        if (!eventData.pointerDrag)
            return;
        MessagePlate mp = eventData.pointerDrag.GetComponent<MessagePlate>();
        if (mp) {
            Load(mp.messageData);
            return;
        }
    }
}
