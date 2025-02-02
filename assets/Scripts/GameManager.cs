﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

    public class GameManager : MonoBehaviour
    {
    //THISCODEISANANGRYMARTIAN
        public float LevelStartDelay =  2f;
        public float turnDelay = .1f;
        public static GameManager instance; //not assigned as default = null
        public BoardManager boardScript;
        [HideInInspector] public bool playersTurn = true;
        public Player playerscript;

    //THISCOMMENTISANANGRIERMARTIAN
        private Text levelText;
        private GameObject levelImage;
        private List<Enemy> enemies;
        private bool enemiesMoving;
        private bool doingSetup;

        // Use this for initialization
        public void Awake()
        {
            Debug.Log("waking up " + playerscript.restarts + " " + MyGlobals.jlevel);
           // if (instance == null)
            instance = this;
         //   else if (instance != this)
            //    {
                //    Destroy(gameObject);
             //       Debug.LogWarning("Destroyed instance, already running", instance);
            //     }
            DontDestroyOnLoad(gameObject);
            enemies = new List<Enemy>();
            boardScript = GetComponent<BoardManager>();
            Debug.Log("logging " + playerscript.restarts);
            levelcheck();
            Debug.Log(MyGlobals.jlevel);
            ClearConsole();
        }
    private void ClearConsole()
    {
        Debug.ClearDeveloperConsole();
    }
    private void levelcheck()
    {
            Debug.Log("level " + MyGlobals.jlevel + " load " + playerscript.restarts);
            InitGame();
       }

      public void InitGame()
        {
             Debug.Log("Doing Setup " + playerscript.restarts);
            doingSetup = true;
            levelImage = GameObject.Find("LevelImage");
            levelText = GameObject.Find("LevelText").GetComponent<Text>();
        if (MyGlobals.jlevel == 0)
        {
            levelText.text = "Level Select";
        }
        else
        {
            levelText.text = "Day " + MyGlobals.jlevel;
        }
            levelImage.SetActive(true);
            Invoke("HideLevelImage", LevelStartDelay);
            enemies.Clear();
            boardScript.SetupScene(MyGlobals.jlevel);
        }

        private void HideLevelImage()
        {
        levelImage.SetActive(false);
        doingSetup = false;
        }

        public void GameOver()
        {
            levelText.text = "After " + MyGlobals.jlevel + " days, you starved.";
            levelImage.SetActive(true);
            enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (playersTurn || enemiesMoving || doingSetup)
            return;

            StartCoroutine(MoveEnemies());

        }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);

    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay); // yields (does it just surrender?) and waits for the turn delay
        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay); // yields and waits for the turn delay
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }
        playersTurn = true;
        enemiesMoving = false;
    }
    }
