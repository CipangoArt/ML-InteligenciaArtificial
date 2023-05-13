using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgentMovementScript : Agent
{
    [SerializeField] float speed = 5;
    Vector3 initialPos;
    List<Danmaku> ofudaBulletPool;
    [SerializeField] SpawnerBehaviour localSpawner;
    [SerializeField] BufferSensorComponent bufferSensor;
    private void Start()
    {
        ofudaBulletPool = new List<Danmaku>();
        Debug.Log(localSpawner);
        Debug.Log(localSpawner.pooledObjects);
        foreach (GameObject previousPool in localSpawner.pooledObjects)
        {
            Danmaku temp=previousPool.GetComponent<Danmaku>();
            Debug.Log(temp);
            ofudaBulletPool.Add(temp);
        }
        
        initialPos = transform.localPosition;
    }
    public override void OnEpisodeBegin()
    {
        SetReward(1f);
        transform.localPosition = initialPos;
        
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[1];
        float moveZ = actions.ContinuousActions[0];
        
        transform.localPosition += new Vector3(moveX, 0, moveZ)*Time.deltaTime*speed;

       
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        
        foreach (Danmaku bullet in ofudaBulletPool)
        {
            if (bullet.gameObject.activeInHierarchy)
            {
                float[] info = { bullet.transform.localPosition.x, bullet.transform.localPosition.y, bullet.transform.localPosition.z, Vector3.Distance(bullet.transform.localPosition,transform.localPosition) };
                bufferSensor.AppendObservation(info);   
            }
           
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;
        continousActions[0] = -Input.GetAxisRaw("Horizontal");
        continousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Wall>(out Wall wall))
        {

            SetReward(-1f);
            EndEpisode();
        }
        
        if (other.TryGetComponent<Danmaku>(out Danmaku danmaku))
        {
            SetReward(-1f);
            EndEpisode();
        }
        
    }
}
