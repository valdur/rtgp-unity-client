﻿using UnityEngine;
using System.Collections;

namespace Wtg.DataModel {
    [System.Serializable]
    public class OrderData {
        public int turn;
        public int number;
        public System.DateTime date;
        public string status;
        public string content;
        public string reply;
    }
}