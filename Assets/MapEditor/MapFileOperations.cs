using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using Wtg.MapEditor;
using Wtg.DataModel;
using Newtonsoft.Json;
using System.Linq;

public class MapFileOperations : MonoBehaviour {

    [SerializeField]
    private FilePicker filePicker;
    [SerializeField]
    private Button loadButton;
    [SerializeField]
    private Button saveButton;
    [SerializeField]
    private Button selectBackgroundButton;
    [SerializeField]
    private MapController mapController;


    [SerializeField]
    private RawImage background;

    private string bgFilename;

    const string dirName = "UserContent";
    const string mapExtension = ".rtgmap";
    const string mapSearchPattern = "*.rtgmap";

    private void Awake() {
        loadButton.onClick.AddListener(LoadHandler);
        saveButton.onClick.AddListener(SaveHandler);
        selectBackgroundButton.onClick.AddListener(SelectBackgroundHandler);
    }

    private void LoadHandler() {
        filePicker.Show("Load Map", "Load", dirName, "", LoadMap, mapSearchPattern);
    }

    private void LoadMap(string filename) {
        MapData md = JsonConvert.DeserializeObject<MapData>(File.ReadAllText(GetPath(filename)));
        LoadBackground(md.bgFilename);
        mapController.Load(md.regions, md.connections);
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
            regions = mapController.GetRegions().ToArray(),
            connections = mapController.GetConnections().ToArray()
        };
        File.WriteAllText(GetPath(filename), JsonConvert.SerializeObject(md));
    }

    private string GetPath(string filename) {
        return Path.Combine(dirName, filename);
    }

    private void SelectBackgroundHandler() {
        filePicker.Show("Select Background", "Select", dirName, "", LoadBackground, "*.jpeg *.jpg *.png");
    }

    private void LoadBackground(string filename) {
        Destroy(background.texture);
        bgFilename = filename;
        var bytes = File.ReadAllBytes(GetPath(filename));
        var tex = new Texture2D(1,1);
        tex.LoadImage(bytes);
        background.texture = tex;
        background.rectTransform.sizeDelta = new Vector2(tex.width, tex.height);
    }

    public class MapData {
        public string bgFilename;
        public RegionData[] regions;
        public ConnectionData[] connections;
    }
}