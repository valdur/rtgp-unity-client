using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;


public class MessageListWidget : MonoBehaviour {

	public MessagePlateWidget messagePlateWidget;
	public Transform listContent;

	// Use this for initialization
	IEnumerator Start() {
		if (GuiController.instance.currentUser == null) {
			Destroy(gameObject);
			yield break;
		}
		var id = GuiController.instance.currentUser.id;
		var www = new WWW("http://192.168.0.11:8080/users/" + id + "/private_messages.json");
		yield return www;
		if (!string.IsNullOrEmpty(www.error)) {
			Debug.LogError(www.error);
		} else {
			var messages = JsonConvert.DeserializeObject<List<MessageData>>(www.text);
			foreach (var up in messages) {
				var uw = Instantiate(messagePlateWidget) as MessagePlateWidget;
				uw.Load(up);
				uw.transform.parent = listContent;
			}
		}
	}
}
