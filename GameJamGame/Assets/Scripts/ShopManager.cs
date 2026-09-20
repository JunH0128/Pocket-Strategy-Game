using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerShopManager : MonoBehaviour
{
    [Header("Tower Definitions")]
    [SerializeField] private TowerData[] towers = new TowerData[]
    {
        new TowerData("Basic Cat Turret", 50),
        new TowerData("Fire Cat Turret", 75),
        new TowerData("Water Cat Turret", 100)
    };

    [Header("Cat Turret Prefabs")]
    [SerializeField] private GameObject basicCatTurretPrefab;
    [SerializeField] private GameObject fireCatTurretPrefab;
    [SerializeField] private GameObject waterCatTurretPrefab;

    [Header("Tower Card UI References")]
    [SerializeField] private Button basicCatCardButton;
    [SerializeField] private Button fireCatCardButton;
    [SerializeField] private Button waterCatCardButton;
    [SerializeField] private TextMeshProUGUI basicCatCostText;
    [SerializeField] private TextMeshProUGUI fireCatCostText;
    [SerializeField] private TextMeshProUGUI waterCatCostText;
    [SerializeField] private TextMeshProUGUI currencyText;

    [Header("Card Visual Feedback")]
    [SerializeField] private Color selectedCardColor = new Color(1f, 1f, 0.5f, 1f); 
    [SerializeField] private Color normalCardColor = Color.white;
    [SerializeField] private Color cannotAffordColor = new Color(0.5f, 0.5f, 0.5f, 1f);

    private TowerData selectedTower;
    private GameObject selectedTowerPrefab;
    private Button selectedButton;
    private Button[] allButtons;
    private TextMeshProUGUI[] allCostTexts;
    private Image[] allCardImages;


    [System.Serializable]
    //  Data structure for tower information
    public class TowerData
    {
        public string name;
        public int cost;

        public TowerData(string towerName, int towerCost)
        {
            name = towerName;
            cost = towerCost;
        }
    }

    private void Start()
    {
        
        allButtons = new Button[] { basicCatCardButton, fireCatCardButton, waterCatCardButton };
        allCostTexts = new TextMeshProUGUI[] { basicCatCostText, fireCatCostText, waterCatCostText };
        
        
        allCardImages = new Image[allButtons.Length];
        for (int i = 0; i < allButtons.Length; i++)
        {
            if (allButtons[i] != null)
                allCardImages[i] = allButtons[i].GetComponent<Image>();
        }

       
        if (basicCatCardButton != null)
            basicCatCardButton.onClick.AddListener(() => SelectTower(0, basicCatTurretPrefab, basicCatCardButton));
        
        if (fireCatCardButton != null)
            fireCatCardButton.onClick.AddListener(() => SelectTower(1, fireCatTurretPrefab, fireCatCardButton));
        
        if (waterCatCardButton != null)
            waterCatCardButton.onClick.AddListener(() => SelectTower(2, waterCatTurretPrefab, waterCatCardButton));

       
        UpdateCostTexts();
        UpdateCurrencyDisplay();
    }

    private void Update()
    {
        UpdateTowerCards();
        UpdateCurrencyDisplay();
    }

    // Update the cost texts on the tower cards NOT DONE 
    private void UpdateCostTexts()
    {
        for (int i = 0; i < allCostTexts.Length && i < towers.Length; i++)
        {
            if (allCostTexts[i] != null)
            {
                allCostTexts[i].text = "$" + towers[i].cost;
            }
        }
    }



    // Handle tower card selection
    private void SelectTower(int index, GameObject prefab, Button clickedButton)
    {
        if (index < 0 || index >= towers.Length || prefab == null)
        {
            return;
        }

        TowerData tower = towers[index];

       
        if (EnemyManager.main.currency < tower.cost)
        {   
            return;
        }

       
        if (selectedButton != null)
        {
            Image prevImage = selectedButton.GetComponent<Image>();
            if (prevImage != null)
                prevImage.color = normalCardColor;
        }

       
        if (selectedButton == clickedButton)
        {
            selectedTower = null;
            selectedTowerPrefab = null;
            selectedButton = null;
            Debug.Log("Deselected tower card");
            return;
        }

        
        selectedTower = tower;
        selectedTowerPrefab = prefab;
        selectedButton = clickedButton;

        
        Image cardImage = clickedButton.GetComponent<Image>();
        if (cardImage != null)
            cardImage.color = selectedCardColor;

        Debug.Log("Selected: " + tower.name + " (Cost: $" + tower.cost + ")");
    }

   
    // Attempt to purchase the selected tower at the given position

    public bool TryPurchaseTower(Vector3 position)
    {
        if (selectedTower == null || selectedTowerPrefab == null)
        {
            Debug.Log("No tower card selected");
            return false;
        }

        if (EnemyManager.main.currency < selectedTower.cost)
        {
            Debug.Log("Not enough gold");
            return false;
        }

     

        
        if (EnemyManager.main.SpendCurrency(selectedTower.cost))
        {
            Instantiate(selectedTowerPrefab, position, Quaternion.identity);
            
            
            
            if (selectedButton != null)
            {
                Image cardImage = selectedButton.GetComponent<Image>();
                if (cardImage != null)
                    cardImage.color = normalCardColor;
            }
            selectedTower = null;
            selectedTowerPrefab = null;
            selectedButton = null;
            
            return true;
        }

        return false;
    }

    // Update the visual state of tower cards based on affordability and selection
    private void UpdateTowerCards()
    {
        
        for (int i = 0; i < allButtons.Length && i < towers.Length; i++)
        {
            if (allButtons[i] == null || allCardImages[i] == null) continue;

            bool canAfford = EnemyManager.main.currency >= towers[i].cost;
            allButtons[i].interactable = canAfford;
            
            
            if (allButtons[i] == selectedButton) continue;

            
            allCardImages[i].color = canAfford ? normalCardColor : cannotAffordColor;
        }
    }

    private void UpdateCurrencyDisplay()
    {
        if (currencyText != null)
        {
            currencyText.text = "$" + EnemyManager.main.currency;
        }
    }

    public TowerData GetSelectedTower()
    {
        return selectedTower;
    }

    public void DeselectTower()
    {
        if (selectedButton != null)
        {
            Image cardImage = selectedButton.GetComponent<Image>();
            if (cardImage != null)
                cardImage.color = normalCardColor;
        }
        
        selectedTower = null;
        selectedTowerPrefab = null;
        selectedButton = null;
    }


     // Get tower cost by index
    public int GetTowerCost(int index)
    {
        if (index >= 0 && index < towers.Length)
            return towers[index].cost;
        return 0;
    }
}