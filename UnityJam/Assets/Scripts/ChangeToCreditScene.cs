using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToCreditScene : MonoBehaviour
{
    [SerializeField] GameObject necromancer;
    bool stopUpdate = false;

    // Update is called once per frame
    void Update()
    {
        if(necromancer == null && !stopUpdate){
            stopUpdate = true;
            CreditScene();
        }
    }
    void CreditScene(){
        SceneManager.LoadScene("FinalScene");
    }
}
