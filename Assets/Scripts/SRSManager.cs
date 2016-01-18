using System.Collections.Generic;

public interface SRSManager
{
    Card GetNextCard();
    bool IsCurrentDeckEmpty();
    List<Card> GetSessionCards();
}