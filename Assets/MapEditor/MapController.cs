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

        public void CreateRegion(Vector3 position) {
            var regionData = new RegionData() {
                _id = System.Guid.NewGuid().ToString(),
                name = "Le Region",
                description = "Le Description",
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

        public void CreateConnection(RegionData fromRegion, RegionData toRegion) {
            var connection = new ConnectionData() {
                _id = System.Guid.NewGuid().ToString(),
                firstRegionId = fromRegion._id,
                secondRegionId = toRegion._id
            };
            connections[connection._id] = connection;
            ConnectionCreatedEvent(connection);
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