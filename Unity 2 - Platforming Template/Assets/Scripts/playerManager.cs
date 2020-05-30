using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This is with using static variables, to see to way in which you store it in another GameObject, check playerManager2.cs

public class playerManager : MonoBehaviour
{

    //Day 2 added --------------------

    public static List<Collectable> inventory;
    public List<Collectable> inventoryCopy;
    public Text inventoryText;
    public Text descriptionText;
    public Text InventoryInfoText;
    public Text InventoryOpenInfoText;
    public GameObject InventoryImage;
    public GameObject InventoryPanel;
    public RawImage InventoryRawImage;
    private static int currentIndex;
    private static bool inventoryOpen;

    //You can just set the variables as static to avoid them being reloaded

    public PlayerInfo info;

    private float jumpForceCopy;

    // Player specific variables
    public static int health;
    public static int score;

    private int healthCopy;
    private int scoreCopy;

    // Boolean values
    private bool isGamePaused = false;

    // UI stuff
    public Text healthText;
    public Text scoreText;
    public GameObject pauseMenu;
    public GameObject winMenu;
    public GameObject loseMenu;

    // Start is called before the first frame update
    void Start()
    {
        // Makes sure game is "unpaused"
        isGamePaused = false;
        Time.timeScale = 1.0f;

        // Make sure all menus are filled in
        FindAllMenus();

        info = FindObjectOfType<PlayerInfo>();

        //Start player with initial health and score
        CheckIfVariablesExist();
        InventoryImage.SetActive(false);
        if (inventoryOpen == true)
        {
            InventoryPanel.SetActive(true);
            InventoryOpenInfoText.text = "";
        }
        else if (inventoryOpen == false)
        {
            InventoryPanel.SetActive(false);
            InventoryOpenInfoText.text = "Press M to open your inventory.";
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Day 2 added ------------------------------
        if (inventory.Count == 0 && inventoryOpen == true)
        {
            inventoryText.text = "Current Selection: None";
            descriptionText.text = "";
            InventoryInfoText.text = "Press M to close your inventory.";
        }

        else if (inventoryOpen == true)
        {
            inventoryText.text = "Current Selection: " + inventory[currentIndex].collectableName + "\n" + (currentIndex + 1).ToString() + "/" + inventory.Count.ToString();
            descriptionText.text = "Press [E] to " + inventory[currentIndex].description;
            InventoryInfoText.text = "Press I to cycle to next item. Press M to close your inventory.";
            SetInventoryImage();
        }

        if (Input.GetKeyDown(KeyCode.E) && inventoryOpen == true)
        {
            //Using
            if (inventory.Count > 0)
            {
                inventory[currentIndex].Use();
                Destroy(inventory[currentIndex]);
                inventory.RemoveAt(currentIndex);
                if (inventory.Count > 0)
                {
                    currentIndex = currentIndex % inventory.Count;
                }
                else
                {
                    currentIndex = 0;
                    InventoryInfoText.text = "";
                    InventoryImage.SetActive(false);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.I) && inventory.Count > 0 && inventoryOpen == true)
        {
            currentIndex = (currentIndex + 1) % inventory.Count;
            SetInventoryImage();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (inventoryOpen == true)
            {
                InventoryPanel.SetActive(false);
                inventoryOpen = false;
                InventoryOpenInfoText.text = "Press M to open your inventory.";
            }
            else if (inventoryOpen == false)
            {
                InventoryPanel.SetActive(true);
                inventoryOpen = true;
                InventoryOpenInfoText.text = "";
            }
        }


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
    }

   void FindAllMenus()
    {
        if (!inventoryText)
        {
            inventoryText = GameObject.Find("InventoryText").GetComponent<Text>();
        }
        if (!descriptionText)
        {
            descriptionText = GameObject.Find("DescriptionText").GetComponent<Text>();
        }
        if (!InventoryInfoText)
        {
            InventoryInfoText = GameObject.Find("InventoryInfoText").GetComponent<Text>();
        }
        if (!InventoryOpenInfoText)
        {
            InventoryOpenInfoText = GameObject.Find("InventoryOpenInfoText").GetComponent<Text>();
        }
        if (!InventoryImage)
        {
            InventoryImage = GameObject.Find("Image");
            InventoryRawImage = InventoryImage.GetComponent<RawImage>();
        }
        if (!InventoryPanel)
        {
            InventoryPanel = GameObject.Find("InventoryPanel");
        }
        if (healthText == null)
        {
            healthText = GameObject.Find("HealthText").GetComponent<Text>();
        }
        if (scoreText == null)
        {
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
            collision.gameObject.SetActive(false);
            SetInventoryImage();
        }
    }

    

    private void SetInventoryImage()
    {
        if (inventory[currentIndex].collectableName.Equals("coin"))
        {
            InventoryImage.SetActive(true);
            InventoryRawImage.texture = Resources.Load("coin0") as Texture2D;
        }
        else if (inventory[currentIndex].collectableName.Equals("Bronze Coin"))
        {
            InventoryImage.SetActive(true);
            InventoryRawImage.texture = Resources.Load("BronzeCoin") as Texture2D;
        }
        else if (inventory[currentIndex].collectableName.Equals("Red Potion"))
        {
            InventoryImage.SetActive(true);
            InventoryRawImage.texture = Resources.Load("redPotion") as Texture2D;
        }
        else if (inventory[currentIndex].collectableName.Equals("Blue Potion"))
        {
            InventoryImage.SetActive(true);
            InventoryRawImage.texture = Resources.Load("bluePotion") as Texture2D;
        }
    }

    private void CheckIfVariablesExist()
    {
        if (health == 0)
        {
            health = 100;
        }
        if (score == 0)
        {
            score = 0;
        }
        if (inventory == null)
        {
            inventory = new List<Collectable>();
        }
        inventoryCopy = inventory.ConvertAll(Collectable => Collectable);
        healthCopy = health;
        scoreCopy = score;
        jumpForceCopy = this.GetComponent<NinjaController.NinjaController>().PhysicsParams.jumpUpForce;
    }

    public void ResetStats()
    {
        inventory = inventoryCopy;
        currentIndex = 0;
        if (inventory.Count >= 1)
        {
            SetInventoryImage();
        }
        else
        {
            InventoryImage.SetActive(false);
        }

        health = healthCopy;
        score = scoreCopy;
        this.GetComponent<NinjaController.NinjaController>().PhysicsParams.jumpUpForce = jumpForceCopy;
    }
}
