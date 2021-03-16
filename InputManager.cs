using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float panSpeed = 2.0f;
    public float upDownSpeed = 12.0f;
    private float panDetect = 35.0f;
    private bool lastFrameMoved = false;
    private int consecFrames = 0;
    private float fExp = 1.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        RTSSetCamera();
    }

    void RTSSetCamera()
    {
        lastFrameMoved = false;
        float moveX = Camera.main.transform.position.x;
        float moveZ = Camera.main.transform.position.z;
        float moveY = Camera.main.transform.position.y;

        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        float dScroll = Input.mouseScrollDelta.y;
        float fSpeed = panSpeed / 10.0f;
        float speedCoeff = Mathf.Pow(fExp, (float)consecFrames);
        if(mouseX > 0 && mouseX < panDetect)
        {
            moveX -= fSpeed * speedCoeff;
            lastFrameMoved = true;
        }
        if(mouseX < Screen.width && mouseX > Screen.width - panDetect)
        {
            moveX += fSpeed * speedCoeff;
            lastFrameMoved = true;
        }
        if(mouseY > 0 && mouseY < panDetect)
        {
            moveZ -= fSpeed * speedCoeff;
            lastFrameMoved = true;
            
        }
        if(mouseY < Screen.height && mouseY > (Screen.height - panDetect))
        {
            moveZ += fSpeed * speedCoeff;
            lastFrameMoved = true;
        }
        if (Mathf.Abs(dScroll) > 0.1f)
        { 
            moveY += (upDownSpeed * dScroll);
            lastFrameMoved = true;
            if (moveY < 1.0f)
                moveY = 1.0f;
        }
        if (lastFrameMoved)
        {
            consecFrames += 1;
        }
        else
            consecFrames = 0;
           

        Vector3 newPos = new Vector3(moveX, moveY, moveZ);
        Camera.main.transform.position = newPos;
    }
}
