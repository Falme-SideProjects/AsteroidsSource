using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum ScoreScale { BigAsteroid, MedAsteroid, SmallAsteroid, BigUFO, SmallUFO } 

public class Score : MonoBehaviour {

    //Player Score of the gameplay
    public static int score;

    //Next number to get a life
    public static int nextLife = 10000;

    //List of highscores (RANK)
    public static List<ScoreData> highscores;

    [SerializeField]
    private Text text;

    //Write to a file
    private StreamWriter writer;

    //Get path to the file
    private string path;

    void Awake()
    {

        highscores = new List<ScoreData>();

        //File will be highscore.txt
        path = Application.persistentDataPath + "/highscore.txt";

        loadData(); //Load highscores


    }

    private void loadData()
    {

        //If file do no exist
        if (!System.IO.File.Exists(path))
        {
            //Create a new one with 10 (AAA : zero) values
            for (int a = 0; a < 10; a++)
                highscores.Add(new ScoreData("AAA", 0));
        
        // If exists, call the file and ser values to list
        } else
        {
            string values = System.IO.File.ReadAllText(path);

            string[] datas = values.Split('|');

            for(int a=0; a<datas.Length; a++)
            {
                string[] dataValue = datas[a].Split(':'); // Split Name of the player and Value Score
                highscores.Add( new ScoreData( dataValue[0],Int32.Parse(dataValue[1]) ) ); //Add to list
            }
        }

    }

    //ADD values to the score, to the player in gameplay
    public static void setScore(ScoreScale scoreScale)
    {
        switch (scoreScale)
        {
            case ScoreScale.BigAsteroid:
                Score.score += 30;
                break;
            case ScoreScale.MedAsteroid:
                Score.score += 50;
                break;
            case ScoreScale.SmallAsteroid:
                Score.score += 100;
                break;
            case ScoreScale.BigUFO:
                Score.score += 200;
                break;
            case ScoreScale.SmallUFO:
                Score.score += 1000;
                break;
        }
    }

    //Get Score of the player
    public int getScore()
    {
        return Score.score;
    }

    void FixedUpdate()
    {
        //refresh score
        if(GameStates.actualGameState == GameState.Gameplay) { 
            if(text.text != "" + getScore())
            {
                text.text = "" + getScore();

                verifyLifeUp(); //Verify if life up
            }
        }
    }

    void verifyLifeUp()
    {
        if (Score.score > Score.nextLife)
        {
            Lifes.lives++;
            Score.nextLife += 10000; // Set next milestone
        }
    }

    public static void setHighScore(string namePlayer)
    {
        
        //Put new highscore to the last place
        if(Score.score > Score.highscores[9].value)
        {
            Score.highscores[9].setValue(Score.score);
            Score.highscores[9].setName(namePlayer);
            
        }

        //Reorder Values 
        List<ScoreData> li = highscores.OrderByDescending(i => i.value).ToList();

        Score.highscores = li;

        //Save in a file

        string path = Application.persistentDataPath + "/highscore.txt";
        StreamWriter writer = new StreamWriter(path, false);

        string dataFile = "";

        //loop to write values
        for(int a=0; a<10; a++)
        {
            dataFile += Score.highscores[a].name + ":" + Score.highscores[a].value + (a == 9 ? "":"|");
        }

        //Save data
        writer.Write(dataFile);
        writer.Close();

    }

}
