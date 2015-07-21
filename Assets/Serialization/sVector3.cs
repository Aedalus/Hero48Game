using UnityEngine;
using System.Collections;

[System.Serializable]
public class sVector3{

    float px;
    float py;
    float pz;

    public sVector3(Vector3 v)
    {
        px = v.x;
        py = v.y;
        pz = v.z;
    }

    public Vector3 getVector()
    {
        return new Vector3(px, py, pz);
    }
}
