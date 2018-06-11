using Twooter.Models;

namespace Twooter.Interfaces
{
    public interface ITwootContext
    {
        void InsertTwoot(Twoot twoot);
        void InsertFavorieteTwoot(int twootID, int ProfielID);
        void DeleteFavorieteTwoot(int twootID, int ProfielID);
    }
}
