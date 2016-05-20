using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;

public class MapFileOperations : MonoBehaviour {

    [Header("External References")]
    public FilePicker filePicker;

    [Header("Internal References")]
    [SerializeField]
    private Button loadButton;
    [SerializeField]
    private Button saveButton;
    [SerializeField]
    private Button selectBackgroundButton;

    [SerializeField]
    private RawImage background;

    const string dirName = "UserContent";

    private void Awake() {
        loadButton.onClick.AddListener(LoadHandler);
        saveButton.onClick.AddListener(SaveHandler);
        selectBackgroundButton.onClick.AddListener(SelectBackgroundHandler);
    }

    private void LoadHandler() {
        throw new NotImplementedException();
    }

    private void SaveHandler() {
        throw new NotImplementedException();
    }

    private void SelectBackgroundHandler() {
        filePicker.Show("Select Background", "Select", dirName, "", BackgroundFilenameSelectedHandler);
    }

    private void BackgroundFilenameSelectedHandler(string filename) {

        Destroy(background.texture);

        var bytes = File.ReadAllBytes(Path.Combine(dirName, filename));
        var tex = new Texture2D(1,1);
        tex.LoadImage(bytes);
        background.texture = tex;
    }
}
