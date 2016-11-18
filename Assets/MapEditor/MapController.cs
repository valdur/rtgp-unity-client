using UnityEngine;
using System.Collections;
using Wtg.DataModel;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;

namespace Wtg.MapEditor {
    public class MapController : MonoBehaviour {

        private Dictionary<string,GameAreaData> regions = new Dictionary<string, GameAreaData>();
        private Dictionary<string, AreaConnectionData> connections = new Dictionary<string, AreaConnectionData>();
        private Dictionary<string, UserData> users = new Dictionary<string, UserData>();
        public List<string> selectedRegions = new List<string>();
        public List<string> selectedConnections = new List<string>();
        public enum Mode { Edit, View};

        public event System.Action<GameAreaData> RegionCreatedEvent = delegate { };
        public event System.Action<GameAreaData> RegionDestroyedEvent = delegate { };
        public event System.Action<GameAreaData> RegionUpdatedEvent = delegate { };
        public event System.Action<AreaConnectionData> ConnectionCreatedEvent = delegate { };
        public event System.Action<AreaConnectionData> ConnectionDestroyedEvent = delegate { };
        public event System.Action<AreaConnectionData> ConnectionUpdatedEvent = delegate { };
        public event System.Action<Texture> BackgroundLoadedEvent = delegate { };

        public event System.Action SelectionChangedEvent = delegate { };

        public Mode mode { get; private set; }
        public Texture backgroundTexture { get; private set; }

        public void SelectSingle(GameAreaData region) {
            QuietClearSelection();
            selectedRegions.Add(region._id);
            SelectionChangedEvent();
        }

        internal bool IsEditMode() {
            return mode == Mode.Edit;
        }

        internal bool IsViewMode() {
            return mode == Mode.View;
        }

        internal void Load(IEnumerable<GameAreaData> regions, 
                IEnumerable<AreaConnectionData> connections,
                IEnumerable<UserData> users = null) {
            DeleteAll();
            if (users != null) {
                foreach (var user in users) {
                    this.users[user._id] = user;
                }
            }

            foreach ( var reg in regions) {
                this.regions[reg._id] = reg;
                RegionCreatedEvent(reg);
            }
            foreach (var con in connections) {
                this.connections[con._id] = con;
                ConnectionCreatedEvent(con);
            }
        }

        internal void LoadBackgroundFromUrl(string url) {
            StartCoroutine(LoadBackgroundCor(url));
        }

        internal void LoadBackgroundFromFile(string path) {
            var bytes = File.ReadAllBytes(path);
            var tex = new Texture2D(1, 1);
            tex.LoadImage(bytes);
            LoadBackground(tex);
        }

        private IEnumerator LoadBackgroundCor(string url) {
            WWW www = new WWW(url);
            yield return www;
            LoadBackground(www.texture);
        }

        private void LoadBackground(Texture2D tex) {
            if (backgroundTexture)
                Destroy(backgroundTexture);
            backgroundTexture = tex;
            BackgroundLoadedEvent(backgroundTexture);
            RefreshAll();
        }

        internal void EnterEditMode() {
            mode = Mode.Edit;
            RefreshAll();
        }

        internal void EnterViewMode() {
            mode = Mode.View;
            RefreshAll();
        }

        internal IEnumerable<GameAreaData> GetRegions() {
            return regions.Values;
        }

        internal IEnumerable<AreaConnectionData> GetConnections() {
            return connections.Values;
        }

        internal IEnumerable<UserData> GetUsers() {
            return users.Values.OrderBy(x => x.username);
        }

        public void RefreshAll() {
            foreach (var reg in regions.Values) {
                RegionUpdatedEvent(reg);
            }
            foreach (var con in connections.Values) {
                ConnectionUpdatedEvent(con);
            }
            SelectionChangedEvent();
        }

        internal void SelectSingle(AreaConnectionData connection) {
            QuietClearSelection();
            selectedConnections.Add(connection._id);
            SelectionChangedEvent();
        }

        public void AddToSelection(GameAreaData region) {
            selectedRegions.Add(region._id);
            SelectionChangedEvent();
        }

        public void AddToSelection(AreaConnectionData region) {
            selectedConnections.Add(region._id);
            SelectionChangedEvent();
        }

        public void RemoveFromSelection(GameAreaData connection) {
            selectedRegions.Remove(connection._id);
            SelectionChangedEvent();
        }

        public void RemoveFromSelection(AreaConnectionData connection) {
            selectedConnections.Remove(connection._id);
            SelectionChangedEvent();
        }

        public void ClearSelection() {
            QuietClearSelection();
            SelectionChangedEvent();
        }

        private void QuietClearSelection() {
            selectedRegions.Clear();
            selectedConnections.Clear();
        }

        public void NotifyRegionUpdated(GameAreaData region) {
            RegionUpdatedEvent(region);
        }

        public void NotifyConnectionUpdated(AreaConnectionData connection) {
            ConnectionUpdatedEvent(connection);
        }

        public void CreateRegion(Vector3 position) {
            var regionData = new GameAreaData() {
                _id = System.Guid.NewGuid().ToString().Replace("-",""),
                name = "Le Region",
                description = "Le Description",
                areaType = GameAreaData.areaTypeValues[0],
                position = position
            };
            regions[regionData._id] = regionData;
            RegionCreatedEvent(regionData);
        }

        public GameAreaData GetRegion(string id) {
            return regions[id];
        }

        public AreaConnectionData GetConnection(string id) {
            return connections[id];
        }

        public UserData GetUser(string id) {
            if (string.IsNullOrEmpty(id))
                return null;
            return users[id];
        }

        public bool IsRegionSelected(string id) {
            return selectedRegions.Contains(id);
        }

        public bool IsConnectionSelected(string id) {
            return selectedConnections.Contains(id);
        }

        public bool AreRegionsConnected(string id1, string id2) {
            return connections.Values.Any(x => (x.firstRegionId == id1 && x.secondRegionId == id2) || (x.firstRegionId == id2 && x.secondRegionId == id1));
        }

        public void CreateConnection(GameAreaData fromRegion, GameAreaData toRegion) {
            if (AreRegionsConnected(fromRegion._id, toRegion._id))
                return;
            var connection = new AreaConnectionData() {
                _id = System.Guid.NewGuid().ToString().Replace("-", ""),
                firstRegionId = fromRegion._id,
                secondRegionId = toRegion._id,
                transport = AreaConnectionData.transportValues[0],
                distance = 1,
            };
            connections[connection._id] = connection;
            ConnectionCreatedEvent(connection);
        }

        void SelectAll() {
            selectedRegions = regions.Values.Select(x => x._id).ToList();
            selectedConnections = connections.Values.Select(x => x._id).ToList();
            SelectionChangedEvent();
        }

        void DeleteAll() {

            foreach (var con in connections.Values.ToArray()) {
                ConnectionDestroyedEvent(con);
            }

            foreach (var reg in regions.Values.ToArray()) {
                RegionDestroyedEvent(reg);
            }

            users.Clear();
            regions.Clear();
            connections.Clear();
            selectedRegions.Clear();
            selectedConnections.Clear();
            SelectionChangedEvent();
        }

        internal void DeleteSelectedObjects() {
            var regs = selectedRegions.Select(x => GetRegion(x));

            var cons = new HashSet<AreaConnectionData>(selectedConnections.Select(c => GetConnection(c)));
            foreach(var con in connections) {
                if (selectedRegions.Contains(con.Value.firstRegionId) || selectedRegions.Contains(con.Value.secondRegionId)) {
                    cons.Add(con.Value);
                }
            }

            foreach (var reg in regs) {
                regions.Remove(reg._id);

                // TODO destroy multiple at once
                // TODO create multiple at once
                RegionDestroyedEvent(reg);
            }
            selectedRegions.Clear();

            foreach( var con in cons) {
                connections.Remove(con._id);
                ConnectionDestroyedEvent(con);
            }
            selectedConnections.Clear();
            SelectionChangedEvent();
        }
    }
}