using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInitialization : MonoBehaviour
{

    private void Awake()
    {
        GameSaver.Init();

        GameSaver.LoadGameSaves();
    }
}
