using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance_;

    private GameManager()
    {

    }
    public static GameManager Instance
    {
        get { return instance_; }
    }
    private void Start()
    {
       if(instance_ != null && instance_ != this)
        {
            Destroy(this.gameObject);
        }else
        {
            instance_ = this;
        }
    }

    private int sceneIndex;
    private void Update()
    {
        
    }
    public void GoToNextScene(int index_)
    {
        sceneIndex = index_;
        //Use a fade in  animation 
    }
    public void SceneOperator()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
}
