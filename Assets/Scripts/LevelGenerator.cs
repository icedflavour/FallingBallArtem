using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    
    [SerializeField] private GameObject Column;
    [SerializeField] private GameObject Floor;
    [SerializeField] private GameObject FloorSegment;
    [SerializeField] private int SegmentsQuantity;
    [SerializeField] private int AngleDiff;

    private void Start()
    {
        Instantiate(Column);
        var parentFloorInstance = Instantiate(Floor);

        var spawnPosition = new Vector3(0.0f, 0.0f, 0.0f);

        for (int i = 0; i < 24; i++)
        {
            var floorSegmentInstance = Instantiate(FloorSegment, spawnPosition, Quaternion.identity, parentFloorInstance.transform);
            floorSegmentInstance.transform.Rotate(0.0f, AngleDiff * i, 0.0f, Space.Self);
        }
    }
}

