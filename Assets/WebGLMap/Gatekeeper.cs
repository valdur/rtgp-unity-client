using UnityEngine;
using System.Collections;
using Wtg.MapEditor;
using Wtg.DataModel;
using UnityEngine.UI;

namespace Wtg.WebGL {

    public class Gatekeeper : MonoBehaviour {

        [SerializeField]
        private MapController mapController;

        [SerializeField]
        private RawImage background;

        private void Start() {
            mapController.EnterViewMode();
        }

        public void LoadJson(string mapData) {
            MapData md = JsonUtility.FromJson<MapData>(mapData);
            StartCoroutine(LoadBackgroundCor(md.bgFilename));
            mapController.Load(md.gameAreas, md.areaConnections);
        }

        public void Test(string testString) {
            Debug.Log("Hello world" + testString);
        }

        private IEnumerator LoadBackgroundCor(string url) {

            WWW www = new WWW(url);
            yield return www;
            if (background.texture)
                Destroy(background.texture);
            background.texture = www.texture;
            background.rectTransform.sizeDelta = new Vector2(background.texture.width, background.texture.height);
            mapController.RefreshAll();
        }

    }
}