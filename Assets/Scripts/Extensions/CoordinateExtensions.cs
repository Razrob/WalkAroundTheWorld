using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class CoordinateExtensions
{
    public static Vector3 TransformFromCustomCoordinate(this Vector3 _position)
    {
        return new Vector3(_position.x - _position.z, _position.y + (_position.x + _position.z) / 2f, _position.x + _position.z);
    }

    public static Vector3 TransformToCustomCoordinate(this Vector3 _position)
    {
        float x = (_position.z + _position.x) / 2f;
        float z = x - _position.x;
        float y = _position.y - (x + z) / 2f;

        return new Vector3(x, y, z);

    }
    public static Vector2Int TransformFromCustomCoordinate(this Vector2 _position)
    {
        return new Vector2Int(Convert.ToInt32(_position.x - _position.y), Convert.ToInt32(_position.x + _position.y));
    }

    public static Vector2Int TransformToCustomCoordinate(this Vector2 _position)
    {
        float x = (_position.y + _position.x) / 2f;
        float z = x - _position.x;

        return new Vector2Int(Convert.ToInt32(x), Convert.ToInt32(z));
    }

    public static Vector2Int Normalize(this Vector2Int _position)
    {
        Vector2 _vector2 = new Vector2(_position.x > 0 ? 1 : _position.x < 0 ? -1  : 0, _position.y > 0 ? 1 : _position.y < 0 ? -1 : 0).normalized;

        _position = new Vector2Int(Convert.ToInt32(_vector2.x), Convert.ToInt32(_vector2.y));
        return _position;
    }

}
