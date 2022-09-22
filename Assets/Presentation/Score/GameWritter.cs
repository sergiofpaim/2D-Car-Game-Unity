using Assets.Logic.Game;
using UnityEngine;
using UnityEngine.UI;

public class GameWritter : MonoBehaviour
{
    [SerializeField] public AudioSource explosionSound;
    public Text WinnerText;
    public Image WinnerBG;

    public Image HelpBG;
    public Text HelpText;

    public void ShowWinner()
    {
        WinnerBG.color = Color.white;
        WinnerText.text = GameGoal.Winner;
    }

    public void PlayExplosion()
    {
        explosionSound.Play();
    }

    internal void ShowAbout()
    {
        HelpText.text =
        "ABOUT THIS GAME\n\n" +
        "2D Car Game\n" +
        "By Sérgio Filho Paim, under the guidance " +
        "of Sérgio Paim (2022)\n" +
        "Source code: https://github.com/sergiofpaim/2D-Car-Game-Unity\n\n\n" +
        "INSTRUCTIONS\n\n" +
        " Avoid rocks and mountains.\n" +
        " Hit the other car to win!\n" +
        " Use W, A, S, D keys to drive the green car, " +
        "and the arrows to drive the blue car.";
    }
}