using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class Kart : Agent
{

   private KartController _kartController;
   private string LastCheckpoint;
    public Transform ResetPoint = null;


    public override void Initialize()
   {
      _kartController = GetComponent<KartController>();
   }
   

   public override void OnEpisodeBegin()
   {
        ResetCharacter();

    }

      public override void OnActionReceived(ActionBuffers actions)
      {
        var input = actions.ContinuousActions;

        _kartController.ApplyAcceleration(input[1]);
        _kartController.Steer(input[0]);
      }
      
      //manueel
      public override void Heuristic(in ActionBuffers actionsOut)
      {
        var action = actionsOut.ContinuousActions;
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        //action[1] = Input.GetKey(KeyCode.W) ? 1f : 0f;

        if (_kartController.currentSpeed > 0f || _kartController.speed > 0f)
        {
            EndEpisode();
        }
       }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.collider.tag == "Checkpoint")
        {
            AddReward(1.0f);
            LastCheckpoint = collision.collider.name;
        }
        else if (collision.collider.name == LastCheckpoint)
        {
            AddReward(-0.2f);
            EndEpisode();
        }
        Debug.Log(GetCumulativeReward().ToString("f2"));

    }



    private void ResetCharacter()
    {
        this.transform.position = new Vector3(ResetPoint.position.x, ResetPoint.position.y, ResetPoint.position.z);
    }


}
