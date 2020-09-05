using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public static int score, count, difficulty;
    public float speed, frequency;
    private Vector3 spawnPoint;
    public static bool gameOver, updateScore;
    
    
    private Material forbidden, okay;
    public static List<Transform> transforms;

    public Text txtScore, txtGameOver;

    void Start()
    {
        count = 0; //
        score = 0;
        difficulty = 5; //the lesser the value, the harder the game
        frequency = 1f; //how frequently cubes will be created


        gameOver = false;
        updateScore = false;
        spawnPoint = new Vector3(-4.8f, 0.25f, 0);

        forbidden = Instantiate((Material)Resources.Load("Materials/forbidden"));
        okay = Instantiate((Material)Resources.Load("Materials/okay"));

        transforms = new List<Transform>();

        CreateInvoke(frequency);
        UpdateScoreAndFrequency();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        { 
            if (updateScore)
            {
                UpdateScoreAndFrequency();
            }
        }
        else
        {
            txtScore.enabled = false;
            txtGameOver.text = $"GAME OVER! \n Your score is : {score}\n\n Click to restart!";

            //Mobile input
            OnTouch();

            //Desktop input
            if (Input.GetMouseButtonDown(0))
            {
                Restart();
            }
        }

    }

    void SpawnObjects()
    {
        if(!gameOver)
        {
            count++;
            GameObject obj = Instantiate((GameObject)Resources.Load("Prefabs/Cube"), spawnPoint, Quaternion.identity);
            obj.tag = (count % difficulty == 0) ? "forbidden" : "okay";
            obj.GetComponent<MeshRenderer>().material = (obj.tag == "forbidden") ? forbidden : okay;
            transforms.Add(obj.GetComponent<Transform>());
        }
    }

    private void FixedUpdate()
    {
        if (!gameOver)
        { 
            foreach (Transform trf in transforms)
            {
                trf.Translate(Vector3.right * speed / 30);
            }
        }
    }

    private void Restart()
    {
        count = 0;
        score = 0;
        frequency = 1;

        txtGameOver.text = "";
        txtScore.enabled = true;
        txtScore.text = $"Score : {Convert.ToString(score)}";

        foreach (Transform trf in transforms)
        {
            Destroy(trf.gameObject);
        }
        
        transforms = new List<Transform>();
        updateScore = false;
        CancelInvoke();
        gameOver = false;
        CreateInvoke(frequency);
    }

    private void UpdateScoreAndFrequency()
    {
        txtScore.text = $"Score : {Convert.ToString(score)}";
        updateScore = false;

        if(score > 0 && score <= 50 && score % 10 == 0)
        {
            if (score > 10 && score <= 20)
            {
                frequency = 1.1f;
            }
            else if(score > 20 && score <= 30)
            {
                frequency = 1.5f;
                difficulty = 4;
            }
            else if(score > 30 && score <= 40)
            {
                frequency = 1.9f;
                difficulty = 3;
            }
            else
            {
                frequency = 2.2f;
            }
            //speed += frequency * 4/7;

            RemoveInvoke();
            CreateInvoke(frequency);
        }
    }

    private void OnTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Restart();
            }
        }
    }


    void CreateInvoke(float frequency)
    {
        InvokeRepeating("SpawnObjects", 1f, (float)1 / frequency);
    }

    void RemoveInvoke()
    {
        CancelInvoke();
    }
}
