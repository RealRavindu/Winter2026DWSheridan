using System.Collections;
using UnityEngine;


public class MovementTutorial : MonoBehaviour
{
    public CanvasGroup Player1Keys, Player2Keys;
    public IEnumerator MovementTutorialCR()
    {
        bool player1NotMoved = true, player2NotMoved = true;
        yield return new WaitForSeconds(3);
        LeanTween.alphaCanvas(Player1Keys, 1, 2);
        LeanTween.alphaCanvas(Player2Keys, 1, 2);

        while (player1NotMoved || player2NotMoved)
        {
            if(Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.D))
            {
                player1NotMoved = false;
                LeanTween.alphaCanvas(Player1Keys, 0, 2);
            }
            if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Keypad6))
            {
                player2NotMoved = false;
                LeanTween.alphaCanvas(Player2Keys, 0, 2);
            }
            yield return null;
        }
        
    }
}
