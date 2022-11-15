using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Mirror;
using TMPro;

public class PopUpSystem : NetworkBehaviour
{

    public GameObject popUpbox;
    public Animator animator;
    public TMP_Text popUptext;

    public void PopUp(string text){
        popUpbox.SetActive(true);
        popUptext.text = text;
        animator.SetTrigger("pop");
    } 	
    public void PopClose(){
        animator.SetTrigger("close");
    } 	
}
