using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.EventSystems;

public class UserListWidget : MonoBehaviour {

    public UserPlateWidget userPlateWidget;
    public Transform listContent;

    // Use this for initialization
    void Start() {
        foreach (var up in MeteorDAO.instance.users) {
            var uw = Instantiate(userPlateWidget) as UserPlateWidget;
            uw.Load(up);
            uw.transform.parent = listContent;
        }
    }
}
