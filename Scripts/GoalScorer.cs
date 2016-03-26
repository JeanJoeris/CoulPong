using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoalScorer : MonoBehaviour {

    public int PlayerNum;

    private GameController gameController;

	// Use this for initialization
	void Start () {

        GameObject GameControllerObject = GameObject.FindWithTag("GameController");
        gameController = GameControllerObject.GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            gameController.AddScore(PlayerNum);
        }
    }
}
