using Parse;
using System.Collections;

public class PlayerDao {

    private Player player;

    public IEnumerator MakeQuerryGetPlayer(ParseUser user)
    {
        ParseQuery<Player> querry = new ParseQuery<Player>().WhereEqualTo("UserId", user.ObjectId);

        bool wait = true;
        querry.FirstAsync().ContinueWith(t => {
            player = t.Result;
            wait = false;
        });
        while (wait)
        {yield return null;}

    }    

    public Player getQuerryResultPlayer()
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
