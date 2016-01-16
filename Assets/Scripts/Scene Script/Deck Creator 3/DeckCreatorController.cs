using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeckCreatorController : MonoBehaviour {

    private DeckCreatorView view = null;
    private DeckCreatorModel model = null;
    private InputField portugueseText = null;
    private InputField englishText = null;

    // Use this for initialization
    void Start () {
        view = gameObject.GetComponent<DeckCreatorView>();
        model = gameObject.GetComponent<DeckCreatorModel>();

        GameObject portObj = GameObject.Find("Portuguese Field");
        portugueseText = portObj.GetComponent<InputField>();

        GameObject englObj = GameObject.Find("English Field");
        englishText = englObj.GetComponent<InputField>();
    }		

    public void OnCardClick(Card card)
    {
        model.SetSelectedCard(card);
        view.UpdateScreen();
    }

    public void AddOrUpdateCard()
    {
        Card selectedCard = model.GetSelectedCard();
        selectedCard.PortugueseText = portugueseText.text;
        selectedCard.EnglishText = englishText.text;
        if (selectedCard.Equals(model.GetNewCard()))
        {
            model.CreateNewCard();
        }
        view.UpdateScreen();
    }

    public void DeleteCard()
    {
        Card selectedCard = model.GetSelectedCard();
        if (!selectedCard.Equals(model.GetNewCard()))
        {
            model.DeletSelectedCard();
            view.UpdateScreen();
        }
    }

    public void SaveDeck()
    {
        model.Save();
    }

    public void Exit()
    {

    }
}
