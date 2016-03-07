using UnityEngine;
using System.Collections;

public class ViewsController : MonoBehaviour {

    public Canvas canvas;
    public Dragling draglingPrefab;
    public UserPage userPagePrefab;
    public MessagePage messagePagePrefab;

    [SerializeField]
    View[] views;

    public static ViewsController instance;

    // Use this for initialization
    void Awake() {
        instance = this;

        for (int i = 0; i < views.Length; i++) {
            views[i].gameObject.SetActive(i == 0);
        }
    }

    public void ShowView<T>() where T : View {
        for (int i = 0; i < views.Length; i++) {
            views[i].gameObject.SetActive(views[i] is T);
        }
    }
}
