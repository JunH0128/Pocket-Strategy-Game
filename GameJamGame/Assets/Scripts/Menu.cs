using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] Animator anim;

    private bool isMenuOpen = true;

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

        public void OpenMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("CardOpen", isMenuOpen);
    }

    private void OnGUI()
    {
        currencyUI.text = EnemyManager.main.currency.ToString();
    }
    
    public void SetSelected()
    {
        
    }
}
