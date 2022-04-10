using ExitGames.Client.Photon.StructWrapping;
using JetBrains.Annotations;
using Photon.Pun;
using ProjectTM.Containers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ProjectTM.Core
{
    public class Randomizer : MonoBehaviour
    {
        [Header("Randomizer settings")]
        [Tooltip("Defines the maximum amount of puzzles that are created")]
        public int NumberOfPuzzles = 2;
        [Tooltip("Defines the maximum amount of the same type of container that are created")]
        public int MaxNumberOfSameContainers = 2;
        [Tooltip("The location where the first puzzle is created")]
        public Transform StartLocation;

        [Header("Asset lists")]
        public List<GameObject> Containers;
        public List<GameObject> Puzzles;
        public List<GameObject> Solutions;
        public List<Transform> Locations;
        public List<GameObject> Goals;
        public List<Color> Colors;

        private Dictionary<string, int> containersInScene = new Dictionary<string, int>();
        private GameObject generatedSolution;

        void Start()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (NumberOfPuzzles > Locations.Count)
                    NumberOfPuzzles = Locations.Count;

                GenerateContainer(GenerateGoal());

                // Generate number of puzzles -1, one puzzle already created for Goal
                for (int i = 0; i < NumberOfPuzzles - 1; ++i)
                {
                    //Generate puzzle
                    GenerateContainer(generatedSolution);
                }

                generatedSolution.transform.position = StartLocation.position + new Vector3(0, 0.1f, 0);
            }
        }

        GameObject GenerateGoal()
        {
            int index = Random.Range(0, Goals.Count);
            GameObject goal = PhotonNetwork.InstantiateRoomObject($"Goals/{Goals[index].name}", Vector3.zero, Quaternion.identity);
            return goal;
        }

        void GenerateContainer(GameObject items)
        {
            int index = Random.Range(0, Containers.Count);
            int location = Random.Range(0, Locations.Count);
            string containerName = Containers[index].name;

            if (containersInScene.ContainsKey(containerName))
            {
                if (containersInScene[containerName] >= MaxNumberOfSameContainers)
                {
                    Containers.RemoveAt(index);
                    GenerateContainer(items);
                    return;
                }
                else
                    containersInScene[containerName]++;
            }
            else
                containersInScene.Add(containerName, 1);

            GameObject newContainer = PhotonNetwork.InstantiateRoomObject($"Containers/{Containers[index].name}", Locations[location].position, Locations[location].rotation);
            Container containerScript = newContainer.GetComponent<Container>();
            containerScript.AddObject(items);
            
            GameObject generatedPuzzle = GeneratePuzzle();
            containerScript.AddPuzzle(generatedPuzzle);

            Puzzle puzzleScript = generatedPuzzle.GetComponent<Puzzle>();
            puzzleScript.LinkSolution(GenerateSolution(generatedPuzzle.name, generatedPuzzle.GetComponent<MeshRenderer>().material.color));
            puzzleScript.Container = containerScript;

            Locations.RemoveAt(location);
        }

        GameObject GeneratePuzzle()
        {
            int index = Random.Range(0, Puzzles.Count);

            GameObject puzzle = PhotonNetwork.InstantiateRoomObject($"Puzzles/{Puzzles[index].name}", Vector3.zero, Quaternion.identity);
            int colorIndex = Random.Range(0, Colors.Count);
            puzzle.GetComponent<MeshRenderer>().material.color = Colors[colorIndex];
            Colors.RemoveAt(colorIndex);
            return puzzle;
        }

        GameObject GenerateSolution(string puzzleName, Color color)
        {
            DirectoryInfo di = new DirectoryInfo($"{Application.dataPath}/Prefabs/Resources/Solutions/{puzzleName.Replace("(Clone)", "")}/");
            FileInfo[] solutions = di.GetFiles("*.prefab");
            int index = Random.Range(0, solutions.Length);
            generatedSolution = PhotonNetwork.InstantiateRoomObject($"Solutions/{puzzleName.Replace("(Clone)", "")}/{solutions[index].Name.Replace(".prefab", "")}", Vector3.zero, Quaternion.identity);
            generatedSolution.GetComponent<MeshRenderer>().material.color = color;
            return generatedSolution;
        }
    }
}
