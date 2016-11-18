using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using Wtg.MapEditor;
using Wtg.DataModel;
using System.Linq;

public class MapFileOperations : MonoBehaviour {

    [SerializeField]
    private Button loadButton;
    [SerializeField]
    private Button saveButton;
    [SerializeField]
    private Button selectBackgroundButton;
    [SerializeField]
    private Button editModeButton;
    [SerializeField]
    private Button viewModeButton;

    public FilePicker filePicker;
    public MapController mapController;

    private string bgFilename;

    const string dirName = "UserContent";
    const string mapExtension = ".rtgmap";
    const string mapSearchPattern = "*.rtgmap";

    private void Start() {
        EnsureDirectoryExistance();
        loadButton.onClick.AddListener(LoadHandler);
        saveButton.onClick.AddListener(SaveHandler);
        editModeButton.onClick.AddListener(EditModeHandler);
        viewModeButton.onClick.AddListener(ViewModeHandler);
        selectBackgroundButton.onClick.AddListener(SelectBackgroundHandler);
        SetupModeButtons();
    }

    void SetupModeButtons() {
        viewModeButton.interactable = mapController.mode != MapController.Mode.View;
        editModeButton.interactable = mapController.mode != MapController.Mode.Edit;
    }

    private void EnsureDirectoryExistance() {
        if (!Directory.Exists(dirName))
            Directory.CreateDirectory(dirName);
    }

    private void LoadHandler() {
        filePicker.Show("Load Map", "Load", dirName, "", LoadMap, mapSearchPattern);
    }

    private void LoadMap(string filename) {
        var path = GetPath(filename);
        Debug.Log(path);
        File.SetAttributes(path, FileAttributes.Normal);
        var fileContent = File.ReadAllText(path);
        MapData md = JsonUtility.FromJson<MapData>(fileContent);
        LoadBackground(md.bgFilename);
        mapController.Load(md.gameAreas, md.areaConnections);
    }

    private void ViewModeHandler() {
        mapController.EnterViewMode();
        SetupModeButtons();
    }

    private void EditModeHandler() {
        mapController.EnterEditMode();
        SetupModeButtons();
    }

    private void SaveHandler() {
        filePicker.Show("Save Map", "Save", dirName, "le map.rtgmap", SaveMap, mapSearchPattern);
    }

    private void SaveMap(string filename) {
        if (!filename.EndsWith(mapExtension)) {
            filename = filename + mapExtension;
        }
        MapData md = new MapData {
            bgFilename = bgFilename,
            gameAreas = mapController.GetRegions().ToList(),
            areaConnections = mapController.GetConnections().ToList()
        };

        var path = GetPath(filename);

        File.WriteAllText(path, JsonUtility.ToJson(md));
        File.SetAttributes(path, FileAttributes.Normal);
    }

    private string GetPath(string filename) {
        return Path.Combine(dirName, filename);
    }

    private void SelectBackgroundHandler() {
        filePicker.Show("Select Background", "Select", dirName, "", LoadBackground, "*.jpeg *.jpg *.png");
    }

    private void LoadBackground(string filename) {
        bgFilename = filename;
        mapController.LoadBackgroundFromFile(GetPath(filename));
    }
}