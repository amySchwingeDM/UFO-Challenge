﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public float speed;                //Floating point variable to store the player's movement speed.
    public Text countText;            //Store a reference to the UI Text component which will display the number of pickups collected.
    public Text winText;            //Store a reference to the UI Text component which will display the 'You win' message.
    public Text livesText;

    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private int count;                //Integer to store the number of pickups collected so far.
    private int lives;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        //Initialize count to zero.
        count = 0;

        //Initialze winText to a blank string since we haven't won yet at beginning.
        winText.text = "";

        lives = 3;

        livesText.text = "";

        //Call our SetCountText function which will update the text with the current value for count.
        SetCountText();

        SetLivesText();
    }

    // Quit application code snippet. Used from Hello World Tutorial 0
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);
    }

    //OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("PickUp"))
        {

            //... then set the other object we just collided with to inactive.
            other.gameObject.SetActive(false);

            //Add one to the current value of our count variable.
            count = count + 1;

            //Update the currently displayed count by calling the SetCountText function.
            SetCountText();
        }

        else if (other.gameObject.CompareTag("Enemy"))
        {

            //... then set the other object we just collided with to inactive.
            other.gameObject.SetActive(false);

            //Add one to the current value of our count variable.
            lives = lives - 1;

            //Update the currently displayed count by calling the SetCountText function.
            SetLivesText();
        }
    }

    //This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
    void SetCountText()
    {
        //Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
        countText.text = "Count: " + count.ToString();

        //Check if we've collected all 12 pickups. If we have...
        if (count == 16) //note that this is the number of yellow pickups on the first playfield
        {
            transform.position = new Vector2(39.5f, 60.5f);
            rb2d.velocity = new Vector2(0f, 0f);
        }
        if (count >= 29)
        {
            //... then set the text property of our winText object to "You win!"
            winText.color = new Color(255, 255, 0, 255);
            winText.text = "You win! Game created by Amy Schwinge";
        }
    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();

        if (lives == 0 && count < 29)
        {
            //... then set the text property of our winText object to "You win!"
            winText.color = new Color(255, 0, 0, 255);
            winText.text = "You lose!";
            rb2d.velocity = new Vector2(0f, 0f);
            // Disables the option to quit
            Destroy(this);
        }
    }
}