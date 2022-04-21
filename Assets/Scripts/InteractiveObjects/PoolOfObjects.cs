using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PoolOfObjects : MonoBehaviour
{
    private GameObject prefab;
    private List<GameObject> listOfObj = new List<GameObject>();

    public void Initialize(GameObject _prefab)
    {
        prefab = _prefab;
    }

    public void AddItem()
    {
        GameObject temp = Instantiate(prefab);
        temp.SetActive(false);
        listOfObj.Add(temp);
    }

 // temp = Instantiate(Resources.Load("Ghost") as GameObject);
    public void Put(Vector2 position)
    {
        foreach (var gem in listOfObj)
        {
            if (!gem.activeInHierarchy)
            {
                gem.transform.position = position; //;Random.RangeinsideUnitCircle * 10;
                gem.SetActive(true);
                return;
            }      
        }
        GameObject temp = Instantiate(prefab, position, Quaternion.identity);
        listOfObj.Add(temp);
    }
    public int ItemsCount => listOfObj.Count;
}

    
