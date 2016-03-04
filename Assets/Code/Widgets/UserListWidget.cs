using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.EventSystems;

public class UserListWidget : MonoBehaviour{

	public UserPlateWidget userPlateWidget;
	public Transform listContent;

	// Use this for initialization
	IEnumerator Start () {
		var www = new WWW(NetworkingManager.instance.serverAddress + "/users.json");
		yield return www;
		if (!string.IsNullOrEmpty(www.error)) {
			Debug.LogError(www.error);
		} else {
			var users = JsonConvert.DeserializeObject<List<UserData>>(www.text);
			foreach (var up in users) {
				var uw = Instantiate(userPlateWidget) as UserPlateWidget;
				uw.Load(up);
				uw.transform.parent = listContent;
			}
		}
	}
}
