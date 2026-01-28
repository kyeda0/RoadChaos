using System;
using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject imageHealth;
    [SerializeField] private GameObject saveImageHealth;

    public void UpdateHealth()
    {
        var playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().health;
        for (int i = 0; i <= playerHealth; i++)
        {
            if(i == playerHealth)
            {
                saveImageHealth = Instantiate(imageHealth);
                saveImageHealth.transform.SetParent(GameObject.Find("PanelForHealth").transform,false);
            }

        }
    }

    public void DeleteHealth()
    {
        Destroy(GameObject.Find("ImageHealth(Clone)").gameObject);
    }
}
