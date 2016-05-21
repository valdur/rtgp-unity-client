using UnityEngine;
using System.Collections;
using Wtg.DataModel;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Wtg.MapEditor {
    public class MapController : MonoBehaviour {

        private Dictionary<string,RegionData> regions = new Dictionary<string, RegionData>();
        private Dictionary<string, ConnectionData> connections = new Dictionary<string, ConnectionData>();
        public List<string> selectedRegions = new List<string>();
        public List<string> selectedConnections = new List<string>();

        public event System.Action<RegionData> RegionCreatedEvent = delegate { };
        public event System.Action<RegionData> RegionDestroyedEvent = delegate { };
        public event System.Action<RegionData> RegionUpdatedEvent = delegate { };
        public event System.Action<ConnectionData> ConnectionCreatedEvent = delegate { };
        public event System.Action<ConnectionData> ConnectionDestroyedEvent = delegate { };
        public event System.Action<ConnectionData> ConnectionUpdatedEvent = delegate { };

        public event System.Action SelectionChangedEvent = delegate { };

        public void SelectSingle(RegionData region) {
            QuietClearSelection();
            selectedRegions.Add(region._id);
            SelectionChangedEvent();
        }

        internal void Load(IEnumerable<RegionData> regions, IEnumerable<ConnectionData> connections) {
            DeleteAll();
            foreach( var reg in regions) {
                this.regions[reg._id] = reg;
                RegionCreatedEvent(reg);
            }
            foreach (var con in connections) {
                this.connections[con._id] = con;
                ConnectionCreatedEvent(con);
            }
        }

        internal IEnumerable<RegionData> GetRegions() {
            return regions.Values;
        }

        internal IEnumerable<ConnectionData> GetConnections() {
            return connections.Values;
        }

        public void RefreshRegions() {
            foreach (var reg in regions.Values) {

            }
        }

        internal void SelectSingle(ConnectionData connection) {
            QuietClearSelection();
            selectedConnections.Add(connection._id);
            SelectionChangedEvent();
        }

        public void AddToSelection(RegionData region) {
            selectedRegions.Add(region._id);
            SelectionChangedEvent();
        }

        public void AddToSelection(ConnectionData region) {
            selectedConnections.Add(region._id);
            SelectionChangedEvent();
        }

        public void RemoveFromSelection(RegionData connection) {
            selectedRegions.Remove(connection._id);
            SelectionChangedEvent();
        }

        public void RemoveFromSelection(ConnectionData connection) {
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

        public void NotifyRegionUpdated(RegionData region) {
            RegionUpdatedEvent(region);
        }

        public void NotifyConnectionUpdated(ConnectionData connection) {
            ConnectionUpdatedEvent(connection);
        }

        public void CreateRegion(Vector3 position) {
            var regionData = new RegionData() {
                _id = System.Guid.NewGuid().ToString().Replace("-",""),
                name = "Le Region",
                description = "Le Description",
                areaType = RegionData.areaTypeValues[0],
                position = position
            };
            regions[regionData._id] = regionData;
            RegionCreatedEvent(regionData);
        }

        public RegionData GetRegion(string id) {
            return regions[id];
        }

        public ConnectionData GetConnection(string id) {
            return connections[id];
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

        public void CreateConnection(RegionData fromRegion, RegionData toRegion) {
            if (AreRegionsConnected(fromRegion._id, toRegion._id))
                return;
            var connection = new ConnectionData() {
                _id = System.Guid.NewGuid().ToString().Replace("-", ""),
                firstRegionId = fromRegion._id,
                secondRegionId = toRegion._id,
                transport = ConnectionData.transportValues[0],
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

            regions.Clear();
            connections.Clear();
            selectedRegions.Clear();
            selectedConnections.Clear();
            SelectionChangedEvent();
        }

        internal void DeleteSelectedObjects() {
            var regs = selectedRegions.Select(x => GetRegion(x));

            var cons = new HashSet<ConnectionData>(selectedConnections.Select(c => GetConnection(c)));
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