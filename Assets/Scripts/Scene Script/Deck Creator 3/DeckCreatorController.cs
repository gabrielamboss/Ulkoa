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

    public void OnPortugueseTextFieldChange()
    {        
        if (model != null)
        {
            GameObject cardUI = model.GetSelectedCard();            
            cardUI.GetComponentInChildren<Text>().text = model.GetPortugueseText();
            Card card = cardUI.GetComponent<CardHolder>().GetCard();
            card.PortugueseText = model.GetPortugueseText();
        }        
    }

    public void OnEnglishTextFieldChange()
    {
        if (model != null)
        {
            GameObject cardUI = model.GetSelectedCard();
            Card card = cardUI.GetComponent<CardHolder>().GetCard();
            card.EnglishText = model.GetEnglishText();
        }        
    }

    public void AddNewCard()
    {        
        model.CreateNewCard();
        view.UpdateScreen();
    }

    public void DeleteCard()
    {
        model.DeleteSelectedCard();
        view.UpdateScreen();        
    }

    public void SaveDeck()
    {
        model.Save();
    }

    public void Exit()
    {
        new LevelManager().LoadLevel(SceneBook.MAIN_MENU_NAME);
    }
}
