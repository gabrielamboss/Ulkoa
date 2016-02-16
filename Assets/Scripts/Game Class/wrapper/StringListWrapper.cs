using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class StringListWrapper  {

    [SerializeField]
    List<string> stringList = new List<string>();

    public void Add(String sdName)
    {
        stringList.Add(sdName);
    }

    public List<string> getList()
    {
        return stringList;
    }

}
