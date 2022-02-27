using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectUI : MonoBehaviour
{
    private static List<IObserverUI> observers = new List<IObserverUI>();

    public static void AddObserver(IObserverUI observer)
    {
        observers.Add(observer);
    }
    public static void RemoveObserver(IObserverUI observer)
    {
        observers.Remove(observer);
    }

    public static void RemoveAllObservers()
    {
        observers.Clear();
    }

    public static void Notify(GameObject sender, UIEvent eventData)
    {
        foreach (IObserverUI observer in observers)
        {
            observer.onNotify(sender, eventData);
        }
    }

    public static void Notify(ScriptableObject objectData, UIEvent eventData)
    {
        foreach (IObserverUI observer in observers)
        {
            observer.onNotify(objectData, eventData);
        }
    }
}
