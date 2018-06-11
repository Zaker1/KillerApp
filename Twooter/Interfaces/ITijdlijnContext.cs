using System.Collections.Generic;
using Twooter.Models;

namespace Twooter.Interfaces
{
    public interface ITijdlijnContext
    {
        List<Twoot> SelectTwoot(Tijdlijn tijdlijn);
        List<Profiel> SelectVriend(Tijdlijn tijdlijn);
        List<Twoot> SelectFavoTwoot(Tijdlijn tijdlijn);
    }
}
