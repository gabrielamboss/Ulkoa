using Parse;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

[ParseClassName("Match")]
public class Match : ParseObject{    

    [ParseFieldName("CorrectPoints")]
    public int CorrectPoints
    {
        get { return GetProperty<int>("CorrectPoints"); }
        set { SetProperty<int>(value, "CorrectPoints"); }
    }

    [ParseFieldName("WrongPoints")]
    public int WrongPoints
    {
        get { return GetProperty<int>("WrongPoints"); }
        set { SetProperty<int>(value, "WrongPoints"); }
    }    

}
