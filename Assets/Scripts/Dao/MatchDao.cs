using Parse;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class MatchDao {

    List<Match> matchList;

    public IEnumerator MakeQuerryGetMatchList(Player player)
    {
        ParseQuery<Match> matchQuerry = new ParseQuery<Match>().WhereEqualTo("UserId", player.UserId);

        bool wait = true;
        matchQuerry.FindAsync().ContinueWith(t => {
            matchList = t.Result.ToList<Match>();
            wait = false;
        });
        while (wait)
        { yield return null; }
                
    }

    public List<Match> getQuerryResultMatchList()
    {
        return matchList;
    }

    //Quando for salvar faca isso dentro de uma corrotina e use 
    //yield return matchDao.saveMatch( myMatch );
    public IEnumerator saveMatch(Match match)
    {
        bool wait = true;
        match.SaveAsync().ContinueWith(t => { wait = false; });
        while (wait)
        { yield return null; }        
    }    
}
