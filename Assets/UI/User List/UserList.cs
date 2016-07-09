using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class UserList : MonoBehaviour {

    public UserPlate userPlateWidget;
    public Transform listContent;

    // Use this for initialization
    void Start() {
        foreach (var up in MeteorAccess.instance.GetUsers()) {
            var uw = Instantiate(userPlateWidget) as UserPlate;
            uw.Load(up);
            uw.transform.SetParent(listContent);
        }
    }
}