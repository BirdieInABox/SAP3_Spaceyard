using System;
using UnityEngine;

public class EventManagerRepositoryCleaner : MonoBehaviour
{
    Action CleanupRepository;

    public void Initialize(Action CleanupRepositoryMethod)
    {
        CleanupRepository = CleanupRepositoryMethod;
        CleanupRepository();
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        CleanupRepository();
    }
}
