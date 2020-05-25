using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerManager : MonoBehaviour
{
    // Player specific variables
    private int health;
    [SerializeField]
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
    public Text inventoryText;
    public Text descriptionText;
    private int currentIndex = 0;
    public SaveBox saveBox;

    // Start is called before the first frame update
    void Start()
    {
        if (saveBox == null)
        {
            saveBox = GameObject.Find("SaveBox").GetComponent<SaveBox>();
        }
        // Makes sure game is "unpaused"
        isGamePaused = false;
        Time.timeScale = 1.0f;

        // Make sure all menus are filled in
        FindAllMenus();

        //Start player with initial health and score
        health = saveBox.LoadHealth();
        score = saveBox.LoadScore();
        inventory = saveBox.LoadInventory();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health.ToString();
        scoreText.text  = "Score:  " + score.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        if (health <= 0)
        {
            LoseGame();
        }

        // Handling Inventory
        if (inventory.Count == 0)
        {
            inventoryText.text = "Nothing in Inventory";
            descriptionText.text = "No Description";
        }
        else
        {
            inventoryText.text = "Currently selected: " + inventory[currentIndex].collectableName.ToString();
            descriptionText.text = "Press [E] to " + inventory[currentIndex].description.ToString();
            inventoryText.text = inventoryText + " " + currentIndex.ToString();
        }

        // Handle Inputs
        if (Input.GetKeyDown(KeyCode.E) && inventory.Count > 0)
        {
            // Use Item
            inventory[currentIndex].Use();
            inventory.RemoveAt(currentIndex);
            currentIndex = (currentIndex - 1) % inventory.Count;
        }
        if (Input.GetKeyDown(KeyCode.I) && inventory.Count > 0)
        {
            // Move to the next item in inventory (loop if at the end)
            currentIndex = (currentIndex + 1) % inventory.Count;
        }
        if (Input.GetKeyDown(KeyCode.U) && inventory.Count > 0)
        {
            // Move to the prev item in inventory (loop if at the end)
            currentIndex = (currentIndex - 1) % inventory.Count;
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
        saveBox.Save(score, health, inventory);
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
        health += value;
    }

    public void ChangeScore(int value)
    {
        score += value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collectable>() != null)
        {
            collision.GetComponent<Collectable>().player = this.gameObject;
            inventory.Add(collision.GetComponent<Collectable>());
            Destroy(collision.gameObject);
        }
    }

}
