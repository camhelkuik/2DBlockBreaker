using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //configuration parameters
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;
    [SerializeField] float screenWidthInUnits = 16f;

    //cached references
    GameSession theGameSession;
    Ball theBall;

    void Start()
    {
        theGameSession = FindObjectOfType<GameSession>();
        theBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        //creating paddle position
        Vector2 paddlePosition = new Vector2(transform.position.x, transform.position.y);

        //updating just the paddle x position based on mouse
        paddlePosition.x = Mathf.Clamp(GetXPos(), minX, maxX);
        
        transform.position = paddlePosition;
    }

    private float GetXPos()
    {
        if (theGameSession.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
