using System.Collections;
using System.Linq;
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
    [SerializeField] private int LevelDifficulty;
    private int GapDifference;
    private int GapDifficulty;
    private int GlassDifficulty;
    private int KillDifficulty;

    private void Start()
    {
        ChangeDifficulty(LevelDifficulty);
        GenerateLevel();
    }

    private void ChangeDifficulty(int levelDifficulty)
    {
        switch (levelDifficulty)
        {
            case 1:
                GapDifference   = 2;
                GapDifficulty   = 5;
                GlassDifficulty = 0;
                KillDifficulty  = 0;
                break;
            case 2:
                GapDifference   = 4;
                GapDifficulty   = 5;
                GlassDifficulty = 0;
                KillDifficulty  = 2;
                break;
            case 3:
                GapDifference   = 6;
                GapDifficulty   = 4;
                GlassDifficulty = 2;
                KillDifficulty  = 4;
                break;
            case 4:
                GapDifference   = 8;
                GapDifficulty   = 4;
                GlassDifficulty = 4;
                KillDifficulty  = 6;
                break;
            case 5:
                GapDifference   = 10;
                GapDifficulty   = 3;
                GlassDifficulty = 6;
                KillDifficulty  = 8;
                break;
        }
    }

    private void GenerateLevel()
    {
        var levelInstance = Instantiate(Level);

        var columnInstance = Instantiate(Column, levelInstance.transform);
        columnInstance.transform.position = new Vector3(0f, FloorsGap * FloorsNumber * -0.5f, 0f);
        columnInstance.transform.localScale = new Vector3(2f, FloorsGap * FloorsNumber * 0.5f, 2f);

        GenerateFloors(levelInstance);
    }


    private void GenerateFloors(GameObject levelObject)
    {
        var gapBasis = Random.Range(0, SegmentsNumber); // Вибираємо перший рандомний індекс

        for (int i = 0; i < FloorsNumber; i++)
        {
            var floorSpawnPosition = new Vector3(0.0f, -i * FloorsGap, 0.0f);
            var floorInstance = Instantiate(Floor, floorSpawnPosition, Quaternion.identity, levelObject.transform);

        //********************* РОЗРАХУНОК GAPS *********************//

            gapBasis = (gapBasis + Random.Range(-GapDifference, GapDifference)) % SegmentsNumber;
            int[] gapIndexes = new int[GapDifficulty];

            for (int index = 0; index < gapIndexes.Length; index++)
            {
                gapIndexes[index] = (gapBasis + index) % SegmentsNumber;
            }

        //**********************************************************//

        //********************** РОЗРАХУНОК KILLBLOCKS **********************//
            int[] killIndexes = new int[KillDifficulty];
            var killBegin = (gapBasis + GapDifficulty) % SegmentsNumber;

            for (int j = 0; j < KillDifficulty; j++)
            {
                var killIndex = (killBegin + Random.Range(0, SegmentsNumber - GapDifficulty)) % SegmentsNumber;

                while (killIndexes.Contains(killIndex))
                {
                    killIndex = (killBegin + Random.Range(0, SegmentsNumber - GapDifficulty)) % SegmentsNumber;
                }

                killIndexes[j] = killIndex;
            }
        //*****************************************************************//

        //********************** РОЗРАХУНОК GLASSBLOCKS **********************//
  

  
        //*****************************************************************//

        //********************** ЗАПОВНЕННЯ ПОВЕРХУ **********************//
            for (int j = 0; j < SegmentsNumber; j++)
            {
                if (gapIndexes.Contains(j))
                {
                    continue;
                }
                else if (killIndexes.Contains(j))
                {
                    var KillSegmentInstance = Instantiate(KillSegment, floorSpawnPosition, Quaternion.identity, floorInstance.transform);
                    KillSegmentInstance.transform.Rotate(0.0f, AngleDiff * j, 0.0f, Space.Self);
                    KillSegmentInstance.GetComponent<Segment>().Index = j; ///
                }
                else
                {
                    var floorSegmentInstance = Instantiate(BasicSegment, floorSpawnPosition, Quaternion.identity, floorInstance.transform);
                    floorSegmentInstance.transform.Rotate(0.0f, AngleDiff * j, 0.0f, Space.Self);
                    floorSegmentInstance.GetComponent<Segment>().Index = j; ///
                }
            }
        //*****************************************************************//
        }
    }
}