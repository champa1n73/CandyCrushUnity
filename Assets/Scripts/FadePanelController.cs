using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePanelController : MonoBehaviour
{
    public Animator panelAnim;
    public Animator gameInfoAnim;
    
    public void OK()
    {
        if (panelAnim != null && gameInfoAnim != null) 
        {
            panelAnim.SetBool("Out", true);
            gameInfoAnim.SetBool("Out", true);
        } 
    }
}
