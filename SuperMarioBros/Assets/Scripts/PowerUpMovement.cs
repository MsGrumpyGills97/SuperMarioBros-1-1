﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMovement : MonoBehaviour
{
    private Rigidbody2D rBody;
    private float moveSpeed = 2f;

    private Transform minY;

    private string objType;

    private PowerUpTracker trackerPU;
    private ScoreCounter trackerScore;

    private Collider2D colliderPU;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        minY = GameObject.Find("MinY").GetComponent<Transform>();
        trackerPU = GameObject.Find("GameController").GetComponent<PowerUpTracker>();
        trackerScore = GameObject.Find("GameController").GetComponent<ScoreCounter>();
        colliderPU = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (name.Contains("Mushroom"))
        {
            objType = "mushroom";
            rBody.velocity = new Vector2(moveSpeed, rBody.velocity.y);
        }
        else if (name.Contains("Flower"))
        {
            //Object stays still
            objType = "flower";
        }
        else if (name.Contains("Star"))
        {
            //Bounce
            objType = "star";
            rBody.velocity = new Vector2(moveSpeed * 1.5f, rBody.velocity.y);
        }

        if (transform.position.y < minY.position.y)
        {
            Destroy(gameObject);
        }

        if (objType == "star")
        {
            colliderPU.enabled = true;
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.gameObject.tag;
        IEnumerator coroutine = WaitForCollider();

        if (objType == "star")
        {   
            if (collisionTag != "Ground" && collisionTag != "Player")
            {
                colliderPU.enabled = false;
            }
        }
        //else if (objType == "mushroom")
        //{
        //    if (collisionTag == "Enemy")
        //    {
        //        colliderPU.enabled = false;

        //        StartCoroutine(coroutine);
        //    }
        //}


        if (collisionTag != "Block" && collisionTag != "Ground" && collisionTag != "Player" && colliderPU.enabled) //Mushroom turns around when hitting collidable object that is not player or ground
        {
            moveSpeed *= -1;
        }
        else if (collisionTag == "Player")
        {
            //Player gets power up
            if (objType == "mushroom")
            {
                GrowAnimation();
                trackerPU.isBig = true;
            }
            else if (objType == "flower")
            {
                FlowerPowerAnimation();
                trackerPU.isFlower = true;
            }
            else
            {
                StarManAnimation();
                trackerPU.isStar = true;
            }

            trackerScore.ScoreChecker(objType);

            Destroy(gameObject);
        }
    }

    private void GrowAnimation()
    {

    }

    private void FlowerPowerAnimation()
    {

    }

    private void StarManAnimation()
    {

    }

    IEnumerator WaitForCollider()
    {
        yield return new WaitForSeconds(0.5f);

        colliderPU.enabled = true;
    }
}