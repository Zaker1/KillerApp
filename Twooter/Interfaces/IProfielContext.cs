using System.Collections.Generic;
using Twooter.Models;

namespace Twooter.Interfaces
{
    public interface IProfielContext
    {
        List<Profiel> Select();
        Profiel Select(Profiel profiel);
        Profiel Select(int Id);
        void Update(Profiel profiel);
        byte[] SelectFoto(Profiel profiel);
        byte[] SelectFoto(int Id);
        void Volg(int VolgendId, int VolgerID);
        void UnVolg(int VolgendId, int VolgerID);
        void Report(Report report);
        List<Profiel> GetReportedProfiel();
        List<Report> GetReportsFromProfiel(int id);
        void UpdateBan(int id, bool ban);
        List<Profiel> GetBannedProfiel();
    }
}
