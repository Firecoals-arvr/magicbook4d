using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    #region PRIVATE_MEMBER_VARIABLES
    UI2DSprite m_SpinnerImage;
    AsyncOperation m_AsyncOperation;
    bool m_SceneReadyToActivate;
    #endregion // PRIVATE_MEMBER_VARIABLES

    #region PUBLIC_MEMBER_VARIABLES
    public static string SceneToLoad { get; set; }
    #endregion // PUBLIC_MEMBER_VARIABLES

    public static void Run()
    {
        SceneManager.LoadSceneAsync("Loading");
    }

    #region MONOBEHAVIOUR_METHODS
    void Start()
    {
		m_SpinnerImage = GetComponentInChildren<UI2DSprite>();
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        StartCoroutine(LoadNextSceneAsync());
    }

    void Update()
    {
        if (m_SpinnerImage)
        {
            if (!m_SceneReadyToActivate)
            {
                m_SpinnerImage.transform.Rotate(Vector3.forward, 90.0f * Time.deltaTime);
            }
            else
            {
                m_SpinnerImage.enabled = false;
            }
        }

        if (m_AsyncOperation != null)
        {
            if (m_AsyncOperation.progress < 0.9f)
            {
                Debug.Log("Scene Loading Progress: " + m_AsyncOperation.progress * 100 + "%");
            }
            else
            {
                m_SceneReadyToActivate = true;
                m_AsyncOperation.allowSceneActivation = true;
            }
        }
    }
    #endregion // MONOBEHAVIOUR_METHODS


    #region PRIVATE_METHODS
    IEnumerator LoadNextSceneAsync()
    {
        //int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        //if (string.IsNullOrEmpty(SceneToLoad))
        //{
        //    m_AsyncOperation = SceneManager.LoadSceneAsync(nextSceneIndex);
        //}
        //else
        //{
            m_AsyncOperation = SceneManager.LoadSceneAsync(SceneToLoad);
        //}

        m_AsyncOperation.allowSceneActivation = false;

        yield return m_AsyncOperation;
    }
    #endregion // PRIVATE_METHODS
}
