using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    Camera _cam;
    NavMeshAgent _agent;
    public LayerMask ground;
    // Start is called before the first frame update
    private void Start()
    {
        _cam = Camera.main;       
        _agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                _agent.SetDestination(hit.point);
            }
        }
    }
}
