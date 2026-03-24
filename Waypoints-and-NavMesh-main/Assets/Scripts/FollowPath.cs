using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal; //Goal from A* algorithm
    float speed = 5.0f;          //Velocidad del tanque
    float accuracy = 1.0f;		 //Distancia por debajo de la cual considero que he alcanzado el nodo
    float rotSpeed = 2.0f;       //Velocidad de rotacion del tanque
    public GameObject wpManager; //All the graph points and links
    GameObject[] wps;            //Nodos del grafo
    GameObject currentNode;		 //El nodo mas cercano en cada momento
    int currentWP = 0;           //Indice a lo largo del camino que seguimos, no tiene que ver con el numero/posicion de cada WP
    Graph g;                     //Grafo

    // Start is called before the first frame update
    void Start()
    {
        // Obtenemos los datos del GraphManager
        g = wpManager.GetComponent<GraphManager>().graph;
        wps = wpManager.GetComponent<GraphManager>().waypoints;

        // Empezamos asumiendo que el nodo actual es el primero
        currentNode = wps[0];
    }

    public void GoToHeli()
    {
       
        g.AStar(currentNode, wps[0]);
        currentWP = 0; 
    }

    public void GoToRuin()
    {
       
        g.AStar(currentNode, wps[7]);
        currentWP = 0;
    }
    public void GoToRadar()
    {

        g.AStar(currentNode, wps[5]);
        currentWP = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // 1. Si el grafo no tiene camino o hemos llegado al final, no hacemos nada
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
            return;

        // 2. El nodo que estamos visitando actualmente del camino A*
        currentNode = g.getPathPoint(currentWP);

        // 3. Si estamos cerca del nodo actual, pasamos al siguiente
        if (Vector3.Distance(transform.position, currentNode.transform.position) < accuracy)
        {
            currentWP++;
        }

        // 4. Si aún hay nodos en el camino, nos movemos
        if (currentWP < g.getPathLength())
        {
            goal = g.getPathPoint(currentWP).transform;

            // Rotación suave hacia el objetivo
            Vector3 lookAtGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                        Quaternion.LookRotation(direction),
                                        Time.deltaTime * rotSpeed);

            // Movimiento frontal
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }

        g.debugDraw();
    }
}
