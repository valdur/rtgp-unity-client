using UnityEngine;
using System.Collections;

public class GuiController : MonoBehaviour {

	public Canvas canvas;
	public Dragling draglingPrefab;
	public UserDataWidget userDataWidgetPrefab;
	public UserData currentUser;

	public static GuiController instance;

	// Use this for initialization
	void Start () {
		instance = this;
	}
}
