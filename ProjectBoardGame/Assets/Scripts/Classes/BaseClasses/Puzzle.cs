using ProjectTM.Containers;
using ProjectTM.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour, ISolvable
{
    public GameObject Solution { get; private set; }
    public Container Container { get; set; }

    public static int PuzzleCount { get; private set; }

    private void Awake()
    {
        PuzzleCount += 1;
    }

    public void LinkSolution(GameObject solution)
    {
        Solution = solution;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Solution != null && collision.collider == Solution.GetComponent<Collider>())
        {
            Container.Unlock();
        }                  
    }
}
