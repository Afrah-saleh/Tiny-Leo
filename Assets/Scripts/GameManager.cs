using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject tryAgainMenu;
    public GameObject youWonMenu;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI healthText;
    public Slider healthSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible =false;
         coinText.text = "Coins: 0 ";
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenTryAgainMenu(){
        tryAgainMenu.SetActive(true);
         Cursor.visible =true;
    }

     public void OpenYouWonMenu(){
        youWonMenu.SetActive(true);
         Cursor.visible =true;
    }
    public void tryAgainYes(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void tryAgainNo(){
        Application.Quit();
    }

    public void UpdateCoinText(int coins){
        coinText.text = "Coins: " + coins.ToString();

    }
     public void UpdateHealthText(int currentHealth , int maxHealth){
        healthText.text = currentHealth + "/" + maxHealth.ToString();
        float newCurrentHealth = (float) currentHealth / maxHealth;
        healthSlider.value = newCurrentHealth;
    }

}
