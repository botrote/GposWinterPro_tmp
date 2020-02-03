using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> where T : Singleton<T>, new()
{
    protected Singleton() { }
    protected static T _instance = new T();
    public static T Instance { get { return _instance; } }
}
