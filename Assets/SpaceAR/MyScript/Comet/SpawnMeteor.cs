using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnMeteor : MonoBehaviour
{
    public GameObject effect;
    public GameObject parent;
    public Transform point;
    public GameObject bt;
    public void SpawnPrefabs()
    {
        NGUITools.SetActive(bt, false);
        parent.transform.GetChild(2).gameObject.SetActive(true);
        parent.transform.GetChild(0).gameObject.SetActive(false);
        effect.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        var go = Instantiate(effect, point.transform.position,Quaternion.identity,parent.transform) as GameObject;
        go.SetActive(true);
        Debug.Log("effect1 = " + go.activeSelf);
        StartCoroutine(WaitForEndEffect(go));
        Debug.Log("effect2 = " + go.activeSelf);
    }
    IEnumerator WaitForEndEffect(GameObject go)
    {
        yield return new WaitForSeconds(7f);
        parent.transform.GetChild(2).gameObject.SetActive(false);
        parent.transform.GetChild(0).gameObject.SetActive(true);
        go.SetActive(false);
        NGUITools.SetActive(bt, true);
    }    
    
}
