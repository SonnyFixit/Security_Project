using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
  public AudioSource buttonSoundSource;
  public AudioClip hoverSound;
  public AudioClip clickSound;


public void HoverSound()
{

  buttonSoundSource.PlayOneShot(hoverSound);

}

public void ClickSound()
{
    buttonSoundSource.PlayOneShot(clickSound);
}



}
