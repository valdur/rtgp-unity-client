using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FilePlate : MonoBehaviour {

    public Text label;
    public Button button;

    private System.Action<string> clickCallback;

    public void Load(string text, System.Action<string> clickCallback) {
        label.text = text;
        button.onClick.AddListener(ClickHandler);
        this.clickCallback = clickCallback;
    }

    private void ClickHandler() {
        clickCallback(label.text);
    }

    private void OnDestroy() {
        button.onClick.RemoveAllListeners();
    }
}
