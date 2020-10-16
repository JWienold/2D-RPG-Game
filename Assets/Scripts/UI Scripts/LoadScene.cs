using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void ChangeSceneToHousing()
    {
    	GlobalData.door = 7;
        SceneManager.LoadScene("InBuildings", LoadSceneMode.Single);
    }

    public void ChangeSceneToMainMenu()
    {
        Destroy(GameObject.Find("GlobalData"));
        SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
    }
}
