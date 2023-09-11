using UnityEngine;
using UnityEngine.UI;


public class CardSelect : MonoBehaviour
{
    public enum CardType
    {
        cannon,
        catapult,
        xbow,
        doubleCannon,
        heal
    }

    public CardType cardType;

    [SerializeField] private Button myButton;
    [SerializeField] private int myDefenseBudget;

    private UISystem uiSystem;


    void Start()
    {
        uiSystem = UISystem.instance;
    }

    void Update()
    {
        if (uiSystem.gameCurrency >= myDefenseBudget)
        {
            myButton.interactable = true;
        }
        else
        {
            myButton.interactable = false;
        }
    }

    public void CardClicked()
    {
        AudioManager.instance.Play("UI CLICK");

        if (cardType == CardType.cannon)
        {
            uiSystem.defenseToSpawn = uiSystem.cannon;
            uiSystem.defenseToSpawnBudget = myDefenseBudget;
        }
        else if (cardType == CardType.catapult)
        {
            uiSystem.defenseToSpawn = uiSystem.catapult;
            uiSystem.defenseToSpawnBudget = myDefenseBudget;
        }
        else if (cardType == CardType.xbow)
        {
            uiSystem.defenseToSpawn = uiSystem.xbow;
            uiSystem.defenseToSpawnBudget = myDefenseBudget;
        }
        else if (cardType == CardType.doubleCannon)
        {
            uiSystem.defenseToSpawn = uiSystem.doubleCannon;
            uiSystem.defenseToSpawnBudget = myDefenseBudget;
        }
        else
        {
            uiSystem.Heal();
            uiSystem.gameCurrency -= 30;
        }

    }
}
