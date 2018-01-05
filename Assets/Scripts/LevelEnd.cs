using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour {

    public string m_levelToLoad;

    public void OnTriggerEnter(Collider other)
    {
        if(m_levelToLoad == "GameOver")
        {
            Debug.Log("Game Over");
            return;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(m_levelToLoad);
    }
}
