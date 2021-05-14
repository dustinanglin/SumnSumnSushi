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

    public static implicit operator Vector3(SerializeVector3 value)
    {
        return new Vector3(value.x, value.y, value.z);
    }

    public static implicit operator SerializeVector3(Vector3 value)
    {
        return new SerializeVector3(value.x, value.y, value.z);
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

    public static implicit operator Quaternion(SerializeQuaternion value)
    {
        return new Quaternion(value.x, value.y, value.z, value.w);
    }

    public static implicit operator SerializeQuaternion(Quaternion value)
    {
        return new SerializeQuaternion(value.x, value.y, value.z, value.w);
    }
}

[System.Serializable]
public struct Sushi
{
    public SerializeVector3 position;
    public SerializeQuaternion rotation;
    public string sushiType;
    public string sauceType;

    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}, {3}]", sushiType, sauceType, position, rotation);
    }
}

[System.Serializable]
public struct Dish
{
    public SerializeVector3 position;
    public SerializeQuaternion rotation;
    public string sauceType;
}

[System.Serializable]
public struct Saucebottle
{
    public SerializeVector3 position;
    public SerializeQuaternion rotation;
    public string sauceType;
}

