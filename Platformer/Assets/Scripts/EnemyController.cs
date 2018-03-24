using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float rangeY = 2;
    public float speed = 3;
    int direction = 1;
    Vector3 initialPos;

	// Use this for initialization
	void Start () {
        initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float modifiedSpeed = speed;

        if (direction == -1)
        {
            modifiedSpeed *= 1.2f;
        } 

        float movementY = modifiedSpeed * Time.deltaTime * direction;
        float newY = transform.position.y + movementY;

        if (Mathf.Abs(newY - initialPos.y) > rangeY)
        {
            direction *= -1;
        }
        else
        {
            transform.position += new Vector3(0, movementY, 0);
        }
	}
}
