using UnityEngine;
using UnityEngine.UI;
using YG;

public class SwitchLanguage : MonoBehaviour
{
   public bool isRussuan;



    private void Start()
    {
        isRussuan = PlayerPrefs.GetInt("IsRussian",1) == 1;  
        SwitchLanguageButton();
    }



    public void ToggleLanguage()
    {
        isRussuan = !isRussuan;
        PlayerPrefs.SetInt("IsRussian",isRussuan?1:0);
        PlayerPrefs.Save();
        SwitchLanguageButton();
    }

    private void SwitchLanguageButton()
    {
        if(isRussuan == true)
        {
            YG2.SwitchLanguage("ru");
        }
        else
        {
            YG2.SwitchLanguage("en");
        }
    }
}
