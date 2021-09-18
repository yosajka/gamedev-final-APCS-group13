using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform ball;
    public Transform box;

    Vector3 ballStartPosition;
    Vector3 boxStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        ballStartPosition = ball.position;
        boxStartPosition = box.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void Replay()
    {
        ball.gameObject.SetActive(true);
        ball.position = ballStartPosition;
        box.position = boxStartPosition;
    }

    public void Setting()
    {

    }

    public void ExitToMenu()
    {

    }

    
}
