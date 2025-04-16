using UnityEngine;
using UnityEngine.UI;

public class DJMenuController : MonoBehaviour
{
    public Button[] songButtons; // assign these manually in Inspector

    private DJBoxController dj;

    public void SetupMenu(DJBoxController djBox)
    {
        dj = djBox;

        for (int i = 0; i < songButtons.Length; i++)
        {
            int index = i; // avoid closure bug
            songButtons[i].onClick.RemoveAllListeners(); // clear previous
            songButtons[i].onClick.AddListener(() =>
            {
                dj.PlaySong(index);
            });

            // Optional: update button label with song name
            Text btnText = songButtons[i].GetComponentInChildren<Text>();
            if (btnText != null && index < dj.songs.Length)
                btnText.text = dj.songs[index].name;
        }
    }
}
