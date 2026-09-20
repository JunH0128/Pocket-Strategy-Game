using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager main;

    [Header("References")]
    public Transform startPoint;
    public Transform[] checkPoints;

    [Header("Currency")]
    public int currency = 100;

    private void Awake()
    {
        main = this;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
       
    }

    public bool SpendCurrency(int amount)
    {
        
        
        if (currency >= amount)
        {
            currency -= amount;
            return true;
        }
        else
        {
                return false;
        }
    }
}