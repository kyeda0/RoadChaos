using System;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private int isClick = 1;
    public event Action OnClickPause;
    public event Action OnPlayingGame;



    public void OnAndOffPause()
    {
        if (isClick == 1)
        {
            isClick = 0;
            OnClickPause.Invoke();
        }

        else if(isClick == 0)
        {
            isClick = 1;
            OnPlayingGame.Invoke();
        }
    }
}
