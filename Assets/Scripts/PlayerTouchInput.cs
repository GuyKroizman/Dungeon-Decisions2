using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerTouchInput : MonoBehaviour {

    public GameObject m_buttonUp;
    public GameObject m_buttonRight;
    public GameObject m_buttonLeft;

    // The game is for two players/users. we have a set of directional buttons for each player
    // this flag determine whether the set of buttons is for player one or two.
    public bool m_isPlayerOne;

    // for convinience - change the bool from above to an int with value 1 for player one and 2 for player two.
    private int m_userIndex;

    public DecisionMaster m_decisionMaster;

    private void Start()
    {
        m_userIndex = m_isPlayerOne ? 1 : 2;
    }

    void Update () {
        
        if (Input.GetMouseButtonUp(0))
        {
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;

            m_decisionMaster.Move(m_userIndex, GetDirectionOfButtonAt(x, y));
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Stationary)
                continue;

            float x = touch.position.x;
            float y = touch.position.y;

            m_decisionMaster.Move(m_userIndex, GetDirectionOfButtonAt(x, y));
        }

    }

    private int GetDirectionOfButtonAt(float x, float y)
    {
        GameObject clickedButton = GetButtonClicked(x, y);
        int direction = GetDirection(clickedButton);
        return direction;
    }

    /// <summary>
    /// Check if in the given x,y there is one of the buttons and return it
    /// </summary>
    /// <param name="x">x coord</param>
    /// <param name="y">y coord</param>
    /// <returns>The button in x,y or null</returns>
    private GameObject GetButtonClicked(float x, float y)
    {
        if (IsButtonCollision(x, y, m_buttonLeft))
            return m_buttonLeft;

        if (IsButtonCollision(x, y, m_buttonRight))
            return m_buttonRight;

        if (IsButtonCollision(x, y, m_buttonUp))
            return m_buttonUp;

        return null;
    }

    private bool IsButtonCollision(float x, float y, GameObject button)
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform rectTransform = button.transform.GetComponent<RectTransform>();
        float buttonWidth = rectTransform.sizeDelta.x * canvas.scaleFactor;
        float buttonHeight = rectTransform.sizeDelta.y * canvas.scaleFactor;


        if (button.transform.position.x > x && 
            button.transform.position.x - buttonWidth < x  &&
            button.transform.position.y < y &&
            button.transform.position.y + buttonHeight > y)
            return true;

        return false;
    }

    private int GetDirection(GameObject clickedButton)
    {
        if (clickedButton == m_buttonLeft)
            return 1;
        if (clickedButton == m_buttonRight)
            return 2;
        if (clickedButton == m_buttonUp)
            return 0;
        return -1;
    }

}
