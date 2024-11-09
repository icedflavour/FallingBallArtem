using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject Level;
    [SerializeField] private GameObject Column;
    [SerializeField] private GameObject Floor;
    [SerializeField] private GameObject BasicSegment;
    [SerializeField] private GameObject KillSegment;
    [SerializeField] private GameObject GlassSegment;

    [SerializeField] private int FloorsNumber;
    [SerializeField] private int FloorsGap;
    [SerializeField] private int SegmentsNumber;
    [SerializeField] private int AngleDiff;

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        var LevelInstance = Instantiate(Level);

        var ColumnInstance = Instantiate(Column, LevelInstance.transform);
        ColumnInstance.transform.position = new Vector3(0f, FloorsGap * FloorsNumber * 0.5f, 0f);
        ColumnInstance.transform.localScale = new Vector3(2f, FloorsGap * FloorsNumber * 0.5f, 2f);

        GenerateFloors(LevelInstance);
    }

    private void GenerateFloors(GameObject levelObject)
    {
        for (int i = 0; i < FloorsNumber; i++)
        {
            var floorSpawnPosition = new Vector3(0.0f, i * FloorsGap, 0.0f);
            var FloorInstance = Instantiate(Floor, floorSpawnPosition, Quaternion.identity, levelObject.transform);

            var gapBasis = Random.Range(0, SegmentsNumber);
            int[] gapIndexes = new int[5]; // switch to level difficulty

            for (int index = 0; index < gapIndexes.Length; index++)
            {
                gapIndexes[index] = (gapBasis + index) % SegmentsNumber;
            }

            for (int j = 0; j < SegmentsNumber; j++)
            {
                if (gapIndexes.Contains(j))
                {
                    continue;
                } 
                else
                {
                    var floorSegmentInstance = Instantiate(BasicSegment, floorSpawnPosition, Quaternion.identity, FloorInstance.transform);
                    floorSegmentInstance.transform.Rotate(0.0f, AngleDiff * j, 0.0f, Space.Self);
                }
            }
        }
    }
}

