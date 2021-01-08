using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class Kart : Agent
{

   private newMovement _kartController;
    private string LastCheckpoint;
    private string LastCheckpoint2;
    private string LastCheckpoint3;
    public Transform ResetPoint = null;
    private Rigidbody rBody;
    private Vector3 startingPosition;
    private Quaternion startingRotation;


    public GameObject something;
    public float period = 0.0f;
    public event Action OnReset;
    

    public override void Initialize()
   {
        rBody = GetComponent<Rigidbody>();
       startingPosition = rBody.transform.position;
        startingRotation = rBody.transform.rotation;
        _kartController = GetComponent<newMovement>();

    }


    private void Reset()
    {
       rBody.velocity = Vector3.zero;
       transform.position = startingPosition;
        rBody.transform.rotation = startingRotation;
        OnReset?.Invoke();
    }


    public override void OnEpisodeBegin()
   {
        Reset();
        LastCheckpoint = "Checkpoint 1";
        LastCheckpoint2 = "Finish";
        LastCheckpoint3 = "Checkpoint 1 (18)";



    }

    public override void OnActionReceived(ActionBuffers actions)
      {
        var script = GetComponent<newMovement>();
        var speed = script.currentSpeed;
        var input = actions.ContinuousActions;
        if (input[1] > 0f)
        {
            AddReward(0.0002f * speed);
        }
        if (input[1] < 0f)
        {

            AddReward(-0.0001f);
        }

        _kartController.ApplyAcceleration(input[1]);
        _kartController.Steer(input[0]);
      }
      
      //manueel
      public override void Heuristic(in ActionBuffers actionsOut)
      {
        var action = actionsOut.ContinuousActions;
        action[0] = 0f;  //Input.GetAxis("Horizontal");
        action[1] = 0f;   //Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.UpArrow))
        {
            action[1] = 1f;//


        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            action[1] = -1f;//


        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            action[0] = -1f; //
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            action[0] = 1f;

        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Checkpoint")
        {
            AddReward(1.0f);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Wall")
        {
            AddReward(-1f);
            EndEpisode();
        }
    }
}
