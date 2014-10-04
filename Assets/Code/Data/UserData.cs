using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[System.Serializable]
public class UserData{
	public string id;
	public string name;
	public string email;
	public string password;
	public string role;
	[JsonConverter(typeof(IsoDateTimeConverter))]
	public System.DateTime lastLogin;
	[JsonConverter(typeof(IsoDateTimeConverter))]
	public System.DateTime createdAt;
	[JsonConverter(typeof(IsoDateTimeConverter))]
	public System.DateTime updatedAt;
}
