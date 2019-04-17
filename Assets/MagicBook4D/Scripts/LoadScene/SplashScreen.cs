
using System.Collections;
using Firecoals.SceneTransition;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        SceneLoader.LoadScene("Menu");
    }
}
