using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ManejoTiempo : MonoBehaviour
{
    private static List<RewindObject> rewindObjects = new List<RewindObject>();
    private static bool isRewinding = false; 

    public static void Register(RewindObject obj)
    {
        if(!rewindObjects.Contains(obj))
            rewindObjects.Add(obj);
    }

    public static void Unregister(RewindObject obj)
    {
        rewindObjects.Remove(obj);
    }

    public void StartRewindAll()
    {
        isRewinding = true;
        foreach (var obj in rewindObjects)
        {
            obj.StartRewind();
        }
    }

    public void StopRewindAll()
    {
        isRewinding = false;
        foreach (var obj in rewindObjects)
        {
            obj.StopRewind();
        }
    }

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            StartRewindAll();
        }
        if (Keyboard.current.rKey.wasReleasedThisFrame)
        {
            StopRewindAll();
        }
    }
}
