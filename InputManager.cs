using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float panSpeed = 2.0f;
    public float upDownSpeed = 12.0f;
    public float panDetect = 35.0f;
    public float maxHeight = 800.0f;
    public float tiltFactor = 0.98f;
    public float minHeight = 10.0f;
    public float ExpThreshold = 45.0f; //threshold below which the angle is sharpened exponentially
    private bool lastFrameMoved = false;
    private int consecFrames = 0;
    private float fExp = 1.01f;
    private float maxAngle = 85.0f;
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
        if (moveY > maxHeight)
            moveY = maxHeight;
        if (moveY < minHeight)
            moveY = minHeight;
        float sFactor = maxHeight - (maxHeight - moveY);
        float angle = (((sFactor / maxHeight)) * maxAngle) % 360.0f;
        if (angle < ExpThreshold)
            angle *= Mathf.Pow(tiltFactor, (ExpThreshold - angle) / (ExpThreshold / 10.0f));
        if (angle > maxAngle)
            angle = maxAngle;
        Quaternion newAngle = Quaternion.Euler(angle, Camera.main.transform.rotation.y, Camera.main.transform.rotation.z);
        Vector3 newPos = new Vector3(moveX, moveY, moveZ);
        Camera.main.transform.position = newPos;
        Camera.main.transform.rotation = newAngle;
    }
}
