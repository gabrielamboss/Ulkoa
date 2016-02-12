using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EvolutionSceneController : MonoBehaviour {

    private EvolutionSceneView view = null;
    private EvolutionSceneModel model = null;
    private bool editable;

    public InputField portugueseText;
    public InputField englishText;

    public GameObject panel;
    public Text errorMsg;
    public Text deckName;
    public Text sessionNumber;

    private bool PreviousIsEditable;

    // Use this for initialization
    void Start () {
        view = gameObject.GetComponent<EvolutionSceneView>();
        model = gameObject.GetComponent<EvolutionSceneModel>();

        Deck deck = GlobalVariables.GetSelectedDeck();
        PreviousIsEditable = deck.IsEditable;
        deck.IsEditable = false;
        List<Card> cardList = deck.cardList;

        if(cardList.Count == 0)
        {
            Card newCard = Card.creatNewCard();
            cardList.Add(newCard);
        }

        model.init(deck);
        view.init();

        editable = deck.IsEditable;
        if (editable)
        {
            errorMsg.text = "";
        }
        else
        {
            portugueseText.interactable = false;
            englishText.interactable = false;
        }
    }		

    public void OnCardClick(GameObject cardUI)
    {
        model.setSelectedCardUI(cardUI);
        view.updatePorEnglText();
    }

    public void OnPortugueseTextFieldChange()
    {        
        if (model != null)
        {
            GameObject cardUI = model.getSelectedCardUI();                        
            CardHolderES ch = cardUI.GetComponent<CardHolderES>();
            ch.setPortugueseText(portugueseText.text);
            view.updateCardContainer();
        }        
    }

    public void OnEnglishTextFieldChange()
    {
        if (model != null)
        {
            GameObject cardUI = model.getSelectedCardUI();
            CardHolderES ch = cardUI.GetComponent<CardHolderES>();
            ch.setEnglishText(englishText.text);
            view.updateCardContainer();
        }        
    }

    public void AddNewCard()
    {
        if (!editable)
            return;

        Card newCard = Card.creatNewCard();
        GameObject cardUI = model.addCard(newCard);
        model.setSelectedCardUI(cardUI);
        view.updateCardContainer();
        view.updatePorEnglText();        
    }

    public void DeleteCard()
    {
        if (!editable)
            return;

        model.deleteSelectedCard();

        List<GameObject> cardUIList = model.getCardUIList();

        if (cardUIList.Count == 0)
        {
            AddNewCard();
        }
        else
        {
            model.setSelectedCardUI(cardUIList[0]);
            view.updateCardContainer();
            view.updatePorEnglText();
        }        
    }

    public void SaveDeck()
    {
        if (!editable)
            return;
        
        if(deckName.text == "")
        {
            errorMsg.text = "Voce precisa definir um nome para o deck";
            return;
        }                    
        else
            errorMsg.text = "";

        StartCoroutine(saveLogic());        
    }

    private IEnumerator saveLogic()
    {
        panel.GetComponent<LoadingPanelCreator>().CreateLoadingPanel();

        List<GameObject> cardUIList = model.getCardUIList();
        Deck deck = model.getDeck();
        deck.CleanCardList();
        foreach (GameObject cardUI in cardUIList)
        {
            CardHolderES ch = cardUI.GetComponent<CardHolderES>();
            ch.updateCard();
            deck.addCard(ch.getCard());
        }

        deck.DeckName = deckName.text;

        DeckDao deckDao = new DeckDao();
        //yield return deckDao.saveDeck(deck);

        List<Card> cardsToDelete = model.getCardsToDelete();
        int total = cardsToDelete.Count;
        int count = 0;        
        foreach (Card card in cardsToDelete)
        {
            //card.DeleteAsync().ContinueWith(t=>{ count++; });
        }
        while(count < total)
        { yield return null; }

        model.cleanGarbage();

        Player player = Player.getInstance();
        if (!player.hasDeck(deck))
            player.addDeck(deck);

        panel.GetComponent<LoadingPanelCreator>().DestroyLoadingPanel();
    }

    public void Exit()
    {
        GlobalVariables.GetSelectedDeck().IsEditable = PreviousIsEditable;
        new LevelManager().LoadLevel(SceneBook.END_GAME_NAME);
    }
}
