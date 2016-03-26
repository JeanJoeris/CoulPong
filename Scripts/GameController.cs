using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    public int[] Score;

    public Text[] ScoreText;

    public Canvas StartMenu;

    public GameObject Ball;

	void Start ()
    {
        Score[0] = 0;
        Score[1] = 0;
    }

   void Update()
    {
        if (Input.GetKeyUp("escape"))
        {
            Application.Quit();
        }
    }

    public void AddScore(int playerNum)
    {
        Score[playerNum - 1]++;
        UpdateScore();
    }

    private void UpdateScore()
    {
        for(int i =0; i < Score.Length; i++)
        {
            ScoreText[i].text = string.Format("Score: {0}", Score[i]);
        }
    }
}
