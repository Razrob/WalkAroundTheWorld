using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{

    private void Start()
    {
        transform.Translate(Vector3.up * 20, Space.Self);

        Debug.Log($"{transform.position.x}  {transform.position.y}  {transform.position.z}");
    }


    private void Update()
    {
        
    }
}
