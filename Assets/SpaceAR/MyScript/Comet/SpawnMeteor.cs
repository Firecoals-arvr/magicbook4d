using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnMeteor : MonoBehaviour
{
    public GameObject meteor;
    public GameObject parent;
    public GameObject cube;
    public void SpawnPrefabs()
    {

        var go = Instantiate(meteor, parent.transform) as GameObject;
        go.transform.position = cube.transform.position;

    }
    
    
}
