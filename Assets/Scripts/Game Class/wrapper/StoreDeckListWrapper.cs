using System;
using System.Collections.Generic;

[Serializable]
public class StoreDeckListWrapper {

    List<StoreDeck> sdList = new List<StoreDeck>();    

    public List<StoreDeck> getList()
    {
        return sdList;
    }

}
