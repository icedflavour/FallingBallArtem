using System.Collections;
using System.Collections.Generic;
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
        var LevelInstance = Instantiate(Level);

        var ColumnInstance = Instantiate(Column, LevelInstance.transform);

        ColumnInstance.transform.position = new Vector3(0f, FloorsGap * (FloorsNumber + 2) * 0.25f, 0f);
        ColumnInstance.transform.localScale = new Vector3(2f, FloorsGap * (FloorsNumber + 2) * 0.25f, 2f);

        for (int i = 0; i < FloorsNumber; i++)
        {
            var floorSpawnPosition = new Vector3(0.0f, i * FloorsGap, 0.0f);
            var FloorInstance = Instantiate(Floor, floorSpawnPosition, Quaternion.identity, LevelInstance.transform);

            for (int j = 0; j < SegmentsNumber; j++)
            {
                var floorSegmentInstance = Instantiate(BasicSegment, floorSpawnPosition, Quaternion.identity, FloorInstance.transform);
                floorSegmentInstance.transform.Rotate(0.0f, AngleDiff * j, 0.0f, Space.Self);
            }
        }
    }
}

