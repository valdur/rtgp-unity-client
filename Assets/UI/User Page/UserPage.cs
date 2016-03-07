using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UserPage : MonoBehaviour, IDropHandler {
    public Text nameLabel;
    public Text idLabel;
    public Text emailLabel;
    public Text roleLabel;
    public Button roleButton;
    UserData mUserData;
    bool mInitialized;

    string[] roles = new string[] { "user", "master", "admin" };

    public void Load(UserData data) {
        mUserData = data;

        nameLabel.text = data.username;
        idLabel.text = data._id;
        emailLabel.text = data.email;
        roleLabel.text = data.profile.role;

        Initialize();
    }

    private void Initialize() {
        if (!mInitialized) {
            mInitialized = true;
            MeteorAccess.instance.UserChangedEvent += UpdateIfMatching;
            roleButton.onClick.AddListener(RoleButtonClickHandler);
        }
    }

    private void RoleButtonClickHandler() {
        var roleIndex = System.Array.IndexOf(roles, mUserData.profile.role);
        roleIndex = (roleIndex + 1) % roles.Length;
        mUserData.profile.role = roles[roleIndex];
        Debug.Log("new role: " + mUserData.profile.role);
    }

    void OnDestroy() {
        MeteorAccess.instance.UserChangedEvent -= UpdateIfMatching;
    }

    void UpdateIfMatching(UserData ud) {
        if (ud._id == mUserData._id)
            Load(ud);
    }

    public void OnDrop(PointerEventData eventData) {
        if (!eventData.pointerDrag)
            return;
        UserPlate upw = eventData.pointerDrag.GetComponent<UserPlate>();
        if (upw) {
            Load(upw.userData);
            return;
        }
    }
}