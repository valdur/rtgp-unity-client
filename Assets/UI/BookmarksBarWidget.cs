using UnityEngine;
using System.Collections;

public class BookmarksBarWidget : MonoBehaviour {

	public BookmarkWidget bookmarkPrefab;
	public Transform[] prefabs;
	public Transform bookmarkParent;


	// Use this for initialization
	void Start () {
		foreach (var p in prefabs) {
			var bw = Instantiate(bookmarkPrefab) as BookmarkWidget;
			bw.prefab = p;
			bw.text.text = p.name;
			bw.transform.parent = bookmarkParent;
		}
	}
}
