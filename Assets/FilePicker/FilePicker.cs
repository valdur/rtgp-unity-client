using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System;

public class FilePicker : MonoBehaviour {

    public FilePlate filePlatePrefab;
    public Transform filePlateRoot;

    public Text titleLabel;
    public Text okButtonLabel;
    public Button okButton;
    public Button cancelButton;
    public InputField filenameInput;

    bool initialized;
    System.Action<string> callback;

    public void Initialize() {
        if (initialized)
            return;

        okButton.onClick.AddListener(OkButtonHandler);
        cancelButton.onClick.AddListener(CancelButtonHandler);

        initialized = true;
    }

    private void CancelButtonHandler() {
        Hide();
    }

    private void OkButtonHandler() {
        Hide();
        callback(filenameInput.text);
    }

    public void Show(string title, string okCaption, string directory, string filename, System.Action<string> callback) {
        Initialize();
        titleLabel.text = title;
        okButtonLabel.text = okCaption;
        gameObject.SetActive(true);
        filenameInput.text = filename;
        var dir = new DirectoryInfo(directory);
        foreach (var file in dir.GetFiles()) {
            var ins = Instantiate(filePlatePrefab) as FilePlate;
            ins.transform.SetParent(filePlateRoot);
            ins.Load(file.Name, FileClickedHandler);
        }
        this.callback = callback;
    }

    private void FileClickedHandler(string fileName) {
        filenameInput.text = fileName;
    }

    void Hide() {
        foreach (Transform tr in filePlateRoot) {
            Destroy(tr.gameObject);
        }
        gameObject.SetActive(false);
    }
}
