using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public struct SerializeVector3
{
    public float x;
    public float y;
    public float z;

    public SerializeVector3(float t_x, float t_y, float t_z)
    {
        x = t_x;
        y = t_y;
        z = t_z;
    }

    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}]", x, y, z);
    }
}

[System.Serializable]
public struct SerializeQuaternion
{
    public float x;
    public float y;
    public float z;
    public float w;

    public SerializeQuaternion(float t_x, float t_y, float t_z, float t_w)
    {
        x = t_x;
        y = t_y;
        z = t_z;
        w = t_w;
    }

    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}, {3}]", x, y, z, w);
    }
}

[System.Serializable]
public struct Sushi
{
    public SerializeVector3 position;
    public SerializeQuaternion rotation;
    public string sushiType;

    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}]", sushiType, position, rotation);
    }
}
