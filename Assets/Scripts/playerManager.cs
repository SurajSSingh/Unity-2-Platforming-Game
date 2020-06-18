using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerManager : MonoBehaviour
{
    // Player specific variables
    // private int health;
    private int score;

    // Boolean values
    private bool isGamePaused = false;

    // UI stuff
    public Text healthText;
    public Text scoreText;
    public GameObject pauseMenu;
    public GameObject winMenu;
    public GameObject loseMenu;

    // Inventory stuff
    private List<Collectable> inventory = new List<Collectable>();
    private int currentIndex = 0;
    public Text inventoryText;
    public Text descriptionText;

    public PlayerInfo info;

    // Start is called before the first frame update
    void Start()
    {
        info = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>();
        // Makes sure game is "unpaused"
        isGamePaused = false;
        Time.timeScale = 1.0f;

        // Make sure all menus are filled in
        FindAllMenus();

        //Start player with initial health and score
        // info.health = 100;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + info.health.ToString();
        scoreText.text  = "Score:  " + score.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        if (info.health <= 0)
        {
            LoseGame();
        }
        if (inventory.Count == 0)
        {
            inventoryText.text = "Nothing in inventory";
            descriptionText.text = "";
        }
        else
        {
            inventoryText.text = "Current Item: "
                + inventory[currentIndex].collectableName + " "
                + currentIndex.ToString();
            descriptionText.text = "Press [E] to " + inventory[currentIndex].description;
        }

        if (Input.GetKeyDown(KeyCode.E) && inventory.Count > 0)
        {
            inventory[currentIndex].Use();
            inventory.RemoveAt(currentIndex);
            if(inventory.Count > 0)
            {
                currentIndex = (currentIndex - 1) % inventory.Count;
            }
        }

        if (Input.GetKeyDown(KeyCode.I) && inventory.Count > 0)
        {
            currentIndex = (currentIndex + 1) % inventory.Count;
        }
    }

   void FindAllMenus()
    {
        if (healthText == null)
        {
            healthText = GameObject.Find("HealthText").GetComponent<Text>();
        }
        if (scoreText == null)
        {
            Debug.Log(GameObject.Find("ScoreText"));
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }
        if (winMenu == null)
        {
            winMenu = GameObject.Find("WinGameMenu");
            winMenu.SetActive(false);
        }
        if (loseMenu == null)
        {
            loseMenu = GameObject.Find("LoseGameMenu");
            loseMenu.SetActive(false);
        }
        if (pauseMenu == null)
        {
            pauseMenu = GameObject.Find("PauseGameMenu");
            pauseMenu.SetActive(false);
        }
    }

    public void WinGame()
    {
        Time.timeScale = 0.0f;
        winMenu.SetActive(true);
    }

    public void LoseGame()
    {
        Time.timeScale = 0.0f;
        loseMenu.SetActive(true);
    }

    public void PauseGame()
    {
        if (isGamePaused)
        {
            // Unpause game
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
            isGamePaused = false;
        }
        else
        {
            // Pause game
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
            isGamePaused = true;
        }
    }

    public void ChangeHealth(int value)
    {
        info.health += value;
    }

    public void ChangeScore(int value)
    {
        score += value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Collectable>() != null)
        {
            collision.gameObject.GetComponent<Collectable>().player = this.gameObject;
            inventory.Add(collision.gameObject.GetComponent<Collectable>());
            collision.gameObject.SetActive(false);
        }
    }

}
