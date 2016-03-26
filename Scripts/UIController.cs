using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

	public void StartGame()
    {
        Application.LoadLevel("baseScene");
    }

    public void LoadLevel(string levelName)
    {
        Application.LoadLevel(levelName);
    }

    public void MenuSwitcher(GameObject menu)
    {
        menu.SetActive(true);
    }
}
