using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZorroDeleteMeScript : MonoBehaviour
{
   
    public void ReloadUnityScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
