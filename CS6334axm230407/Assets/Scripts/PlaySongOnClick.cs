using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;  // The AudioSource component attached to the AudioManager
    public AudioClip[] songs;        // List of AudioClip(s) (songs) to play
    private int currentSongIndex = 0; // Keeps track of the current song index

    void Start()
    {
        // Get the AudioSource component attached to this AudioManager
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing from AudioManager.");
        }

        // Start playing the first song
        if (songs.Length > 0)
        {
            PlayNextSong();
        }
        else
        {
            Debug.LogWarning("No songs assigned to the AudioManager.");
        }
    }

    // Play the next song in the list and loop back to the first song when the end is reached
    void PlayNextSong()
    {
        if (songs.Length == 0) return;

        // Play the current song
        audioSource.clip = songs[currentSongIndex];
        audioSource.Play();

        Debug.Log("Now Playing: " + songs[currentSongIndex].name);

        // Move to the next song in the list
        currentSongIndex++;

        // If we've reached the end of the list, loop back to the first song
        if (currentSongIndex >= songs.Length)
        {
            currentSongIndex = 0;
        }

        // Continue playing the next song after the current one ends
        StartCoroutine(WaitForSongEnd());
    }

    // Coroutine to wait for the song to finish before playing the next one
    IEnumerator WaitForSongEnd()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        PlayNextSong();  // Play the next song after the current one ends
    }
}
