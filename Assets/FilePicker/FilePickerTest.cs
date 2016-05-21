using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class FilePickerTest : MonoBehaviour {

    public FilePicker filePicker;

    public Button button;

    private const string dirName = "UserContent";

    // Use this for initialization
    void Start() {
        button.onClick.AddListener(ButtonClickHandler);
        if (!Directory.Exists(dirName))
            Directory.CreateDirectory(dirName);
    }

    private void ButtonClickHandler() {
        filePicker.Show("Save Wololo", "Save", dirName, "world.json", FilenameChosenHandler, "");
    }

    private void FilenameChosenHandler(string filename) {
        File.WriteAllText(Path.Combine(dirName, filename), "Wololo");
    }
}
