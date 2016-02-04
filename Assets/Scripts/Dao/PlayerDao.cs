using Parse;
using System.Collections;

public class PlayerDao {

    private Player player;

    public IEnumerator MakeQueryGetPlayer(ParseUser user)
    {
        ParseQuery<Player> Query = new ParseQuery<Player>().WhereEqualTo("UserId", user.ObjectId);

        bool wait = true;
        Query.FirstAsync().ContinueWith(t => {
            player = t.Result;
            wait = false;
        });
        while (wait)
        {yield return null;}

    }    

    public Player getQueryResultPlayer()
    {
        return player;
    }

    public IEnumerator savePlayer(Player player)
    {
        bool wait = true;
        player.SaveAsync().ContinueWith(t => { wait = false; });
        while (wait)
        { yield return null; }
    }
	
}
