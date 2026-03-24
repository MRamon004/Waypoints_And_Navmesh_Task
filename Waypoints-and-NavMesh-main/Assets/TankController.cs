using UnityEngine;
using UnityEngine.AI;

public class TankController : MonoBehaviour
{
    public enum NavigationMode { Waypoints, NavMesh }
    public NavigationMode currentMode;

    private FollowPath waypointScript;
    private FollowNavMesh navMeshScript;
    private NavMeshAgent agent;

    void Start()
    {
        waypointScript = GetComponent<FollowPath>();
        navMeshScript = GetComponent<FollowNavMesh>();
        agent = GetComponent<NavMeshAgent>();

        UpdateMode();
    }

    // Método para cambiar el modo desde el Inspector o una tecla
    void UpdateMode()
    {
        if (currentMode == NavigationMode.Waypoints)
        {
            waypointScript.enabled = true;
            navMeshScript.enabled = false;
            agent.enabled = false; // Desactivamos el agente para que no interfiera
        }
        else
        {
            waypointScript.enabled = false;
            navMeshScript.enabled = true;
            agent.enabled = true;
        }
    }

    // Estos son los métodos que asignarás a los botones UNA SOLA VEZ
    public void RequestGoToHeli()
    {
        if (currentMode == NavigationMode.Waypoints) waypointScript.GoToHeli();
        else navMeshScript.GoToHeli();
    }

    public void RequestGoToRuin()
    {
        if (currentMode == NavigationMode.Waypoints) waypointScript.GoToRuin();
        else navMeshScript.GoToRuin();
    }

    public void RequestGoToRadar()
    {
        if (currentMode == NavigationMode.Waypoints) waypointScript.GoToRadar();
        else navMeshScript.GoToRadar();
    }
}