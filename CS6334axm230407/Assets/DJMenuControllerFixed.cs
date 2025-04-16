using UnityEngine;

public class DJMenuControllerFixed : MonoBehaviour
{
    public DJBoxController djBox;

    public GameObject rockButton;
    public GameObject classicalButton;
    public GameObject edmButton;
    public GameObject jazzButton;

    public void PlayRock()
    {
        djBox.PlaySong(0);
    }

    public void PlayClassical()
    {
        djBox.PlaySong(1);
    }

    public void PlayEDM()
    {
        djBox.PlaySong(2);
    }

    //public void PlayJazz()
    //{
    //    djBox.PlaySong(3);
    //}
}
