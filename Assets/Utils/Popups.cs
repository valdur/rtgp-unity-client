using UnityEngine;
using System.Collections;

public class Popups : MonoBehaviour {

    public static Popups instance;

    void Awake() {
        instance = this;
        DisableIfExists(filePicker);
    }

    public FilePicker filePicker;

    private void DisableIfExists(MonoBehaviour mb) {
        if (mb != null)
            mb.gameObject.SetActive(false);
    }
}
