using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour {

    public string m_levelToLoad;

    private Canvas m_canvasEndLevel;
    private DecisionMaster m_decisionMaster;
    private bool m_levelEnded = false;

    public void Start()
    {
        m_canvasEndLevel = GameObject.Find("CanvasEndLevel").GetComponent<Canvas>();
        m_decisionMaster = GameObject.Find("DecisionMaster").GetComponent<DecisionMaster>();
    }

    public void OnTriggerEnter(Collider other)
    {
        m_canvasEndLevel.GetComponent<Canvas>().enabled = true;
        m_decisionMaster.EndLevelStop();
        m_levelEnded = true;
    
    }

    public void Update()
    {
        if(m_levelEnded)
        {
            if (m_levelToLoad == "GameOver")
            {
                Debug.Log("Game Over");
                return;
            }

            if (Input.GetKeyDown("space"))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(m_levelToLoad);
            }
        }
        
    }
}
