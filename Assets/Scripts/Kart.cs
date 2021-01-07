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
    private Rigidbody rigidbody = null;


    public override void Initialize()
   {  
        _kartController = GetComponent<KartController>();
        LastCheckpoint = "Checkpoint 1";
        LastCheckpoint2 = "Finish";
        LastCheckpoint3 = "Checkpoint 1 (18)";
    }
   

   public override void OnEpisodeBegin()
   {
        ResetCharacter();



    }

      public override void OnActionReceived(ActionBuffers actions)
      {
        
        var input = actions.ContinuousActions;
        Debug.Log(input[1]);
        if (input[1] > 0f)
        {
            AddReward(0.0002f);
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


        //action[1] = Input.GetKey(KeyCode.W) ? 1f : 0f;

        /*if (_kartController.currentSpeed > 0f || _kartController.speed > 0f)
        {
            EndEpisode();
        }*/
    }

    private void OnTriggerEnter(Collider collision)
    {        
        if (collision.name == LastCheckpoint || collision.name == LastCheckpoint2 || collision.name == LastCheckpoint3)
        {
            AddReward(-1f);
            EndEpisode();
        }
        else if (collision.tag == "Checkpoint")
        {
            AddReward(1.0f);

            LastCheckpoint3 = LastCheckpoint2;
            LastCheckpoint2 = LastCheckpoint;
            LastCheckpoint = collision.name;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Wall")
        {
            AddReward(-.1f);
            EndEpisode();

        }
    }


    private void ResetCharacter()
    {
        this.transform.position = new Vector3(ResetPoint.position.x, ResetPoint.position.y, ResetPoint.position.z);
    }


}
