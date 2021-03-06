﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    private SwimmerCharacter2D swimmerObject;
    private LeaderboardController leaderboardController;
    private GM thisGM;

    private static int fishCount = 0;
    private Text EndFishCount;

    // leaderboard connection information
    bool findInstance()
    {
        if (thisGM != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            if (findInstance())
            {
                foreach (GM source in FindObjectsOfType<GM>())
                {
                    if (source != this.gameObject.GetComponent<GM>())
                    {
                        Destroy(source.gameObject);
                    }
                }
            }
        }

        if (scene.buildIndex == 1)
        {
            swimmerObject = FindObjectOfType<SwimmerCharacter2D>();
        }

        if (scene.buildIndex == 2)
        {
            SetFishCountText();

            //: TODO Add LeadboardController Functionlity
            leaderboardController = FindObjectOfType<LeaderboardController>();
            leaderboardController.GetFishCount(fishCount);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (thisGM == null)
        {
            thisGM = this.gameObject.GetComponent<GM>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //! replace search
        //@ sceneCount begins at 1?
        if (swimmerObject != null)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1 && swimmerObject.PlayerHealth == 0)
            {
                FindSwimmerCount();
            }
        }
    }

    void FindSwimmerCount()
    {
        fishCount = FindObjectOfType<SwimmerCharacter2D>().FishCount;
    }

    void SetFishCountText()
    {
        Text[] textarray = FindObjectsOfType<Text>();
        for (int i = 0; i < textarray.Length; i++)
        {
            if (textarray[i].gameObject.name == "FishCount")
            {
                EndFishCount = textarray[i];
            }
        }

        EndFishCount.text = "Fish Count: " + fishCount.ToString();
    }

}