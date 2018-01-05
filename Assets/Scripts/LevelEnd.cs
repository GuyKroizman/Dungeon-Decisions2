using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour {

    public string m_levelToLoad;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("?????????????? on collision.");
    }
}
