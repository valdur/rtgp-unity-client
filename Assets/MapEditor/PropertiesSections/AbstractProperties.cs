using UnityEngine;
using System.Collections;

namespace Wtg.MapEditor {
    public abstract class AbstractProperties : MonoBehaviour {

        [SerializeField]
        protected MapMainController map;

        protected virtual void Awake() {
            map.SelectionChangedEvent += SelectionChangedHandler;
            SelectionChangedHandler();
        }

        private void SelectionChangedHandler() {
            var ss = ShouldShow();
            gameObject.SetActive(ss);
            if (ss) {
                Load();
            }
        }

        protected abstract bool ShouldShow();
        protected abstract void Load();

    }
}