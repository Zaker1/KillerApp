using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twooter.Models;
using Twooter.Interfaces;

namespace Twooter.Repos
{
    public class ProfielRepo
    {
        private readonly IProfielContext _context;
        public ProfielRepo(IProfielContext context)
        {
            _context = context;
        }
        public void ProfielOphalen(Profiel profiel)
        {
            profiel = _context.Select(profiel);
            profiel.Foto = _context.SelectFoto(profiel);
        }
        public Profiel ProfielOphalen(int Id)
        {
            Profiel profiel = _context.Select(Id);
            profiel.Foto = _context.SelectFoto(Id);
            return profiel;
        }
        public void ProfielVolgen(int id, int VolgerId)
        {
            _context.Volg(id, VolgerId);
        }
        public void ProfielUnVolgen(int id, int VolgerId)
        {
            _context.UnVolg(id, VolgerId);
        }
        public List<Profiel> ProfielZoeken(string zoekTerm)
        {
            if (String.IsNullOrEmpty(zoekTerm))
            {
                throw new ArgumentNullException("Zoekterm is null");
            }
            else
            {

                List<Profiel> profielen = _context.Select();
                var juisteProfielen = profielen.Where(x => x.Gebruikersnaam.ToLower().Contains(zoekTerm.ToLower())).ToList();

                return juisteProfielen;
            }
        }
        public void ReportProfiel(Report report)
        {
            if (String.IsNullOrEmpty(report.Bericht))
            {
                throw new ArgumentNullException("Bericht is null");
            }
            else
            {
                report.Tijd = DateTime.Today.ToString("d");
                _context.Report(report);
            }
        }
        public List<Profiel> GetReportedProfielen()
        {
            return _context.GetReportedProfiel();
        }
        public List<Report> GetReportsOfProfiel(int id)
        {
            return _context.GetReportsFromProfiel(id);
        }
        public void UpdateBan(int id, bool ban)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException("Ongeldige ID");
            }
            else
            {
                _context.UpdateBan(id, ban);
            }
        }
        public void UpdateProfiel(Profiel profiel)
        {
            _context.Update(profiel);
        }
        public List<Profiel> GetBannedProfiel()
        {
            return _context.GetBannedProfiel();
        }
    }
}
