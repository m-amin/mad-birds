using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] string _nextLevelName;
    
     Monster[] _monsters;

     private void OnEnable()
    {
        _monsters = FindObjectsOfType<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MonstersAllDead())
        {
            GoToNextLevel();
        }
    }

    private void GoToNextLevel()
    {
        Debug.Log("Got to next" + _nextLevelName);
        SceneManager.LoadScene(_nextLevelName);
    }

    private bool MonstersAllDead()
    {
        foreach (var monster in _monsters)
        {
            if (monster.gameObject.activeSelf)
                return false;
        }
        return true;
    }
}
