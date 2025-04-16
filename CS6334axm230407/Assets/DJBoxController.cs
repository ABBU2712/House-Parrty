using UnityEngine;

public class DJBoxController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] songs;

    public void PlaySong(int index)
    {
        if (index >= 0 && index < songs.Length)
        {
            audioSource.clip = songs[index];
            audioSource.Play();
        }
    }
}
