using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioStorage : MonoBehaviour
{
    private static Dictionary<AudioType, AudioSourceHandler> _handlers;
    
    public static Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = transform;
        _handlers = GetComponentsInChildren<AudioSourceHandler>(true)?.ToDictionary(h => h.AudioType, h => h);
    }

    public static AudioSourceHandler Get(AudioType type)
    {
        if (!_handlers.ContainsKey(type))   
            return null;

        return _handlers[type];
    }
}
