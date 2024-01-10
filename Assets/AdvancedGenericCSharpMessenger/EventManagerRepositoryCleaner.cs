using System;
using UnityEngine;

public class EventManagerRepositoryCleaner : MonoBehaviour
{
    Action CleanupRepository;

    public void Initialize(Action CleanupRepositoryMethod)
    {
        CleanupRepository = CleanupRepositoryMethod;
        DontDestroyOnLoad(gameObject);
    }

    void Awake()
    {
        CleanupRepository();
    }
}
