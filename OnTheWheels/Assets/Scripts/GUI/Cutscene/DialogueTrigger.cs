using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
    public bool alreadyClicked = false;

    public void TriggerDialogue ()
	{
        if (!this.alreadyClicked) { 
		    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
	}

    public void setAlreadyClicked(bool ac)
    {
        this.alreadyClicked = ac;
    }

}
