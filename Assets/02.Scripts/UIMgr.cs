using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class UIMgr : MonoBehaviour {

//    void OnGUI(){
//        if (GUI.Button(new Rect(10, 10, 250, 50), "START"))
//        {
//            SceneManager.LoadScene("Level01");
//            SceneManager.LoadScene("Play",LoadSceneMode.Additive);
//        }
//    }
//
    public void OnStartBtnClick(){
        SceneManager.LoadScene("Level01");
        SceneManager.LoadScene("Play",LoadSceneMode.Additive);
    }
}
