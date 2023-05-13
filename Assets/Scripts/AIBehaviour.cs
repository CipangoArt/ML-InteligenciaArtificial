using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] Transform destsination;
    [SerializeField] NavMeshAgent ai;
    [SerializeField] Camera camera;

    [SerializeField] OffMeshLink link;
    public LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = 100f;
            mousePoint = camera.ScreenToWorldPoint(mousePoint);
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                ai.SetDestination(hit.point);
            }
        }
        if (ai.isOnOffMeshLink)
        {
            ai.CompleteOffMeshLink();
        }
    }
    
    
}
