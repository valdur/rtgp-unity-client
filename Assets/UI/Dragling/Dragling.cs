using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dragling : MonoBehaviour, ICanvasRaycastFilter {

    public Text text;

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera) {
        return false;
    }

    internal Dragling Create(string content, Vector3 position) {
        var d = Instantiate(this) as Dragling;
        d.text.text = content;
        d.transform.position = position;
        d.transform.SetParent(ViewsController.instance.canvas.transform);
        d.transform.SetAsLastSibling();
        return d;
    }
}
