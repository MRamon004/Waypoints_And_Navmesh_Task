using UnityEngine;
using UnityEngine.AI; // Requisito para el componente NavMesh Agent 

public class FollowNavMesh : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject wpManager; // Arrastra aquí tu objeto "GraphManager" desde la jerarquía
    private GameObject[] wps;    // Se rellenará automáticamente desde el Manager

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (wpManager != null)
        {
            wps = wpManager.GetComponent<GraphManager>().waypoints;
        }
    }

    public void GoToHeli()
    {
       
        if (wps != null && wps.Length > 0)
        {
            agent.SetDestination(wps[0].transform.position);
        }
    }

    public void GoToRuin()
    {
       
        if (wps != null && wps.Length > 7)
        {
            agent.SetDestination(wps[7].transform.position);
        }
    }

    public void GoToRadar()
    {
        // Suponiendo que el Radar está en el índice 5
        if (wps != null && wps.Length > 5)
        {
            agent.SetDestination(wps[5].transform.position);
        }
    }
}