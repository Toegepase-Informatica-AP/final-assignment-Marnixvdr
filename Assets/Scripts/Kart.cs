using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class Kart : Agent
{

   private KartController _kartController;
   private string LastCheckpoint;
    private string LastCheckpoint2;
    private string LastCheckpoint3;
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
        action[0] = 0f;  //Input.GetAxis("Horizontal");
        action[1] = 0f;   //Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.UpArrow))
        {
            action[1] = 1f;//
            AddReward(0.001f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            action[1] = -1f;//
            AddReward(-0.01f);

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            action[0] = -1f; //
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            action[0] = 1f;

        }


        //action[1] = Input.GetKey(KeyCode.W) ? 1f : 0f;

        /*if (_kartController.currentSpeed > 0f || _kartController.speed > 0f)
        {
            EndEpisode();
        }*/
    }

    private void OnTriggerEnter(Collider collision)
    {
        //AddReward(0.0001f);
        if (collision.tag == "Checkpoint")
        {
            AddReward(1.0f);
            LastCheckpoint = collision.name;
        }
        else if (collision.name == LastCheckpoint)
        {
            AddReward(-1f);
            EndEpisode();
        }
        Debug.Log(GetCumulativeReward().ToString("f2"));
    }

    /*private void OnCollisionEnter(Collider collision)
    {
        AddReward(0.0001f);
        if (collision.transform.CompareTag("Checkpoint"))
        {
            AddReward(1.0f);
            LastCheckpoint = collision.name;
        }
        else if (collision.transform.CompareTag(LastCheckpoint))
        {
            //AddReward(-1f);
            EndEpisode();
        }
        Debug.Log(GetCumulativeReward().ToString("f2"));

    }*/


    private void ResetCharacter()
    {
        this.transform.position = new Vector3(ResetPoint.position.x, ResetPoint.position.y, ResetPoint.position.z);
    }


}
