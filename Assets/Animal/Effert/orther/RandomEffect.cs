using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEffect : SingletonTien<RandomEffect>
{

    [SerializeField] GameObject[] effect;
    public IEnumerator RandomizeEffect(float time)
    {
        int a = Random.Range(0, effect.Length);
        Debug.LogWarning(a.ToString());
        effect[a].SetActive(true);
        yield return new WaitForSeconds(time);
        effect[a].SetActive(false);
    }

    public void Onfound(float time)
    {
        StartCoroutine(RandomizeEffect(time));
    }

    public void Onlost()
    {
        for (int i = 0; i < effect.Length; i++)
        {
            effect[i].SetActive(false);
        }
    }
}
