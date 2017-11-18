using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerTouchInput : MonoBehaviour {

    public GameObject m_buttonUp;
    public GameObject m_buttonRight;
    public GameObject m_buttonLeft;

    public Player m_player;



	
	void Update () {
        
        if (Input.GetMouseButtonUp(0))
        {
            GameObject clickedButton = GetButtonClicked(Input.mousePosition.x, Input.mousePosition.y);
            int direction = GetDirection(clickedButton);
            m_player.Move(direction);
        }

        foreach (Touch touch in Input.touches)
        {
            GameObject clickedButton = GetButtonClicked(touch.position.x, touch.position.y);
            int direction = GetDirection(clickedButton);
            m_player.Move(direction);
        }

    }

    private GameObject GetButtonClicked(float x, float y)
    {
        if (IsButtonCollition(x, y, m_buttonLeft))
            return m_buttonLeft;

        if (IsButtonCollition(x, y, m_buttonRight))
            return m_buttonRight;

        if (IsButtonCollition(x, y, m_buttonUp))
            return m_buttonUp;

        return null;
    }

    private bool IsButtonCollition(float x, float y, GameObject button)
    {
        float buttonWidth = 100f;
        float buttonHeight = 100f;
        float test = button.transform.GetComponent<RectTransform>().sizeDelta.x;

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
