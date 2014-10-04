using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

public static class XmlUtils {

	public static System.Object LoadXmlString(string s, System.Type t) {
		var serializer = new XmlSerializer(t);
		using (var stream = new StringReader(s)) {
			var data = serializer.Deserialize(stream);
			return data;
		}
	}

	public static T LoadXmlString<T>(string s) where T : class {
		return LoadXmlString(s, typeof(T)) as T;
	}

	public static string SaveXmlString(System.Object data, System.Type t) {
		var serializer = new XmlSerializer(t);
		using (var stream = new StringWriter()) {
			serializer.Serialize(stream, data);
			return stream.ToString();
		}
	}

	public static string SaveXmlString<T>(T data) where T : class {
		return SaveXmlString(data, typeof(T));
	}

}
