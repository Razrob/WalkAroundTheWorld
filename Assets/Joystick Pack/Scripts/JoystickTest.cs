using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JoystickTest : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;

    private void Start()
    {
        
    }


    private void Update()
    {
        

    }

    private Vector3 GetMoveDirection()
    {
        Vector2Int _direction = new Vector2Int(Convert.ToInt32(_joystick.Horizontal), Convert.ToInt32(_joystick.Vertical));

        if (_direction.x != 0 && _direction.y != 0)
        {
            if (_direction.x == _direction.y) _direction = new Vector2Int(_direction.x, 0);
            else _direction = new Vector2Int(0, _direction.y);
        }
        else _direction = Vector2Int.zero;
        return new Vector3(_direction.x, _direction.y);

    }
}
