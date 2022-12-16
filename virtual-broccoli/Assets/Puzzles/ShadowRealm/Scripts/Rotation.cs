using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public GameObject moveable;
    public float refresh = 0.2f;
    public int rotationAngle = 5;

    private float currentUpdateTime = 0;

    private void Update() {
        if (Time.time >= currentUpdateTime) {    
            //perform update
            currentUpdateTime = Time.time + refresh;
            int rotation = Input.GetKey(KeyCode.LeftShift) ? -rotationAngle : rotationAngle;
            moveable.transform.Rotate(
                Input.GetKey(KeyCode.X) ? rotation : 0,
                Input.GetKey(KeyCode.Y) ? rotation: 0,
                Input.GetKey(KeyCode.Z) ? rotation: 0);
        }
    }
}
