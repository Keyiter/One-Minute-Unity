using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {
    public float hp;
    public float exp;

    public Position[] positions;

    [System.Serializable]
    public class Position
    {
        public float x;
        public float y;
        public float z;
    }
}
