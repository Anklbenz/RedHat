using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class GameController : MonoBehaviour
{
    




    public int MaxGemsCount = 5;
    [SerializeField]
    private GameObject gemPrefab;
    [SerializeField]
    private GameObject ghostPrefab;
    [SerializeField]
    private Text gemsCollectedLabel;

    [SerializeField]
    private int _goalGemCount = 3;
    private int gemsCollected;
    private PoolOfObjects listOfGams = new PoolOfObjects();
    private PoolOfObjects listOfGhosts = new PoolOfObjects();

    [Space]
    public Vector3 fieldSize;

    [SerializeField]
    private UnityEngine.Camera _cam;
    private Vector3 ScreenCenter = Vector3.zero;
    [SerializeField]
    private GameObject left;
    [SerializeField]
    private GameObject rigth;
    [SerializeField]
    private GameObject top;
    [SerializeField]
    private GameObject bottom;

    private void Awake()
    {
        listOfGams.Initialize(gemPrefab);
        listOfGhosts.Initialize(ghostPrefab);

        for (int i = 0; i < MaxGemsCount; i++)
            listOfGams.AddItem();
    }

    private void Start()
    {
        Player.singleton.onGemTake += OnPlayerTakeGem;
        DefineFildSize();
        ArrangeBorders();

        for (int i = 0; i < MaxGemsCount; i++)
            listOfGams.Put(RandomPoint());
    }

    void OnPlayerTakeGem()
    {
        listOfGams.Put(RandomPoint());
        gemsCollected++;
        RefreshLabel();

        if (gemsCollected % _goalGemCount == 0)
        {
            listOfGhosts.Put(RandomPoint());
        }
    }

    private void RefreshLabel()
    {
        gemsCollectedLabel.text = gemsCollected.ToString();
    }

    private Vector2 RandomPoint()
    {
        return new Vector2(Random.Range(-fieldSize.x, fieldSize.x), Random.Range(-fieldSize.y, fieldSize.y));
    }

    private void DefineFildSize()
    {
        Vector3 cameraToObject = ScreenCenter - _cam.transform.position;
        Debug.Log($"cameraToObject {cameraToObject}");
        float distance = -Vector3.Project(cameraToObject, _cam.transform.forward).y;
        Debug.Log($"distance  {distance}");
        fieldSize = _cam.ViewportToWorldPoint(new Vector3(1, 1, distance));
    }

    private void ArrangeBorders()
    {
        left.transform.position = new Vector3(-fieldSize.x, 0, 0);
        rigth.transform.position = new Vector3(fieldSize.x, 0, 0);
        top.transform.position = new Vector3(0, fieldSize.y, 0);
        bottom.transform.position = new Vector3(0, -fieldSize.y, 0);
    }


}
