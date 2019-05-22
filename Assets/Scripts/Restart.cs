using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(Button))]
public class Restart : MonoBehaviour
{

    private Button button;

    void Start()
    {
        //button = GetComponent<Button>();
        //button.onClick.AddListener(TaskOnClick);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            TaskOnClick();
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
  
    }
}

//public class Restart : MonoBehaviour
//{

//    public void RestartGame()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
//    }

//}