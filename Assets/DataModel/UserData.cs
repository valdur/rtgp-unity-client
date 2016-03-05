using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Meteor;

//[System.Serializable]
public class UserData : MongoDocument{
	public string _id;
	public string username;
	public string email;
	public string password;
	public string role;
	//[JsonConverter(typeof(IsoDateTimeConverter))]
	//public System.DateTime lastLogin;
	//[JsonConverter(typeof(IsoDateTimeConverter))]
	//public System.DateTime createdAt;
	//[JsonConverter(typeof(IsoDateTimeConverter))]
	//public System.DateTime updatedAt;
}
