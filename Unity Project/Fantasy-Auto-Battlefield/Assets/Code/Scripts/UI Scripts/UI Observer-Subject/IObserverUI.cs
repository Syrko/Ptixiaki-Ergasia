using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserverUI
{
    abstract void onNotify(GameObject sender, UIEvent eventData);
    abstract void onNotify(ScriptableObject sender, UIEvent eventData);
}
