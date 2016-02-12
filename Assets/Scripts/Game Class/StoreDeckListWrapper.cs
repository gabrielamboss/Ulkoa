using System;
using System.Collections.Generic;

[Serializable]
public class StoreDeckListWrapper {

    List<StoreDeck> sdList = new List<StoreDeck>();

    public void addStoreDeck(List<StoreDeck> list)
    {
        sdList = list;
    }

    public List<StoreDeck> getList()
    {
        return sdList;
    }

}
