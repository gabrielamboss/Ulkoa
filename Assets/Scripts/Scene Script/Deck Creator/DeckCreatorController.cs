using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DeckCreatorController : MonoBehaviour {

    private DeckCreatorView view = null;
    private DeckCreatorModel model = null;
    private bool editable;

    public InputField deckName;
    public InputField portugueseText;
    public InputField englishText;

    public GameObject panel;
    public Text errorMsg;

    // Use this for initialization
    void Start () {
        view = gameObject.GetComponent<DeckCreatorView>();
        model = gameObject.GetComponent<DeckCreatorModel>();

        Deck deck = GlobalVariables.GetSelectedDeck();
        List<Card> cardList = deck.cardList;

        if(cardList.Count == 0)
        {
            Card newCard = new Card();
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
            deckName.interactable = false;
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
            CardHolder ch = cardUI.GetComponent<CardHolder>();
            ch.setPortugueseText(portugueseText.text);
            view.updateCardContainer();
        }        
    }

    public void OnEnglishTextFieldChange()
    {
        if (model != null)
        {
            GameObject cardUI = model.getSelectedCardUI();
            CardHolder ch = cardUI.GetComponent<CardHolder>();
            ch.setEnglishText(englishText.text);
            view.updateCardContainer();
        }        
    }

    public void AddNewCard()
    {
        if (!editable)
            return;

        Card newCard = new Card();
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
            CardHolder ch = cardUI.GetComponent<CardHolder>();
            ch.updateCard();
            deck.addCard(ch.getCard());
        }

        deck.DeckName = deckName.text;

        Player player = Player.getInstance();
        if (!player.hasDeck(deck))
            player.addDeck(deck);

        PlayerDao playerDao = new PlayerDao();
        yield return playerDao.savePlayer(player);

        //Desnecessario tirar
        /*
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
        */

        panel.GetComponent<LoadingPanelCreator>().DestroyLoadingPanel();
    }

    public void Exit()
    {
        if (GlobalVariables.GoBackToGame)
        {
            GlobalVariables.GoBackToGame = false;
            //GlobalVariables.GetSelectedDeck().IsEditable = false;
            new LevelManager().LoadLevel(SceneBook.GAME_NAME);
        }
        else
        {
            new LevelManager().LoadLevel(SceneBook.MAIN_MENU_NAME);
        }
    }
}
