using System;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private int isClick = 1;
    public event Action OnClickPause;
    public event Action OnPlayningGame;




    public void OnAndOffPause()
    {
        if (isClick == 1)
        {
            isClick = 0;
            OnClickPause.Invoke();
        }

        else
        {
            isClick = 1;
            OnPlayningGame.Invoke();
        }
    }
}
