﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody rigidbody;
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void move(int asdf)
    {
        
        if (asdf == 0)
        {
            rigidbody.AddForce(transform.forward * 10);
        }
            //MoveForward();

        if (asdf == 1)
        {
            //Vector2 direction = rigidbody.velocity.normalized;
            //Vector3.Angle(rigidbody.transform.position, direction);
            //Vector3 cross = Vector3.Cross(rigidbody.transform.position, direction);
            //Quaternion q = Quaternion.Euler(cross);
            //rigidbody.MoveRotation(q);

            //TurnRight();
        }

        if (asdf == 2)
        {
            //TurnLeft();
        }
    }

    //private void MoveForward()
    //{
    //    GetComponent<Rigidbody>().AddForce(transform.forward * 3);
    //    //if (lookingDir == DIRECTIONS.N)
    //    //{
    //    //    transform.position += Vector3.up;

    //    //}
    //    //else if (lookingDir == DIRECTIONS.S)
    //    //{
    //    //    transform.position += Vector3.down;

    //    //}
    //    //else if (lookingDir == DIRECTIONS.W)
    //    //{
    //    //    transform.position += Vector3.left;

    //    //}
    //    //else if (lookingDir == DIRECTIONS.E)
    //    //{
    //    //    transform.position += Vector3.right;

    //    //}
    //}

    //private void TurnLeft()
    //{
    //    if (lookingDir == DIRECTIONS.N)
    //    {
    //        Camera.main.transform.rotation = Quaternion.Euler(0, 270, 90);
    //        //SetDir(CharController.DIRECTIONS.W);
    //    }
    //    else if (lookingDir == DIRECTIONS.S)
    //    {
    //        Camera.main.transform.rotation = Quaternion.Euler(0, 90, 270);
    //        //SetDir(CharController.DIRECTIONS.E);
    //    }
    //    else if (lookingDir == DIRECTIONS.W)
    //    {
    //        Camera.main.transform.rotation = Quaternion.Euler(90, 180, 0);
    //        //SetDir(CharController.DIRECTIONS.S);
    //    }
    //    else if (lookingDir == DIRECTIONS.E)
    //    {
    //        Camera.main.transform.rotation = Quaternion.Euler(270, 0, 0);
    //        //SetDir(CharController.DIRECTIONS.N);
    //    }
    //}

    //private void TurnRight()
    //{
    //    if (lookingDir == DIRECTIONS.N)
    //    {
    //        Camera.main.transform.rotation = Quaternion.Euler(0, 90, 270);
    //        //SetDir(CharController.DIRECTIONS.E);
    //    }
    //    else if (lookingDir == DIRECTIONS.S)
    //    {
    //        Camera.main.transform.rotation = Quaternion.Euler(0, 270, 90);
    //        //SetDir(CharController.DIRECTIONS.W);
    //    }
    //    else if (lookingDir == DIRECTIONS.W)
    //    {
    //        Camera.main.transform.rotation = Quaternion.Euler(270, 0, 0);
    //        //SetDir(CharController.DIRECTIONS.N);
    //    }
    //    else if (lookingDir == DIRECTIONS.E)
    //    {
    //        Camera.main.transform.rotation = Quaternion.Euler(90, 180, 0);
    //        //SetDir(CharController.DIRECTIONS.S);
    //    }
    //}

    //public void SetDir(DIRECTIONS dir)
    //{
    //    lookingDir = dir;
    //}

}
