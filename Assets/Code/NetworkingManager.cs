using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class NetworkingManager : MonoBehaviour {

	public string serverAddress;
	public static NetworkingManager instance;

	void Awake () {
		instance = this;
	}

	public void Login(string email, string password, System.Action successCallback = null, System.Action failCallback = null) {
		StartCoroutine(LoginCoroutine(email, password, successCallback, failCallback));
	}

	IEnumerator LoginCoroutine(string email, string password, System.Action successCallback, System.Action failCallback) {
		var headers = new Dictionary<string,string>();
		headers["Content-Type"] = "text/json";

		var poststr = JsonConvert.SerializeObject(new { email = email, password = password });
		Debug.Log(poststr);
		byte[] postbytes = System.Text.Encoding.UTF8.GetBytes(poststr);

		var www = new WWW(serverAddress + "/users/login", postbytes, headers);
		yield return www;
		if (string.IsNullOrEmpty(www.error)) {
			Debug.Log("Um, success?");
			Debug.Log(www.text);
		} else {
			Debug.Log("Nah, fail");
			Debug.Log(www.error);
		}
	}
}
