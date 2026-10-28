using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PSClassLib.Interface;
using PSClassLib.Model;

namespace PinkScouts.Pages
{
    public class ScoutsModel : PageModel
    {
        private readonly IRepo<Scout> _repo;

        public List<Scout> Scouts { get; private set; } = new List<Scout>();
        public string? ErrorMessage { get; private set; }

        public ScoutsModel(IRepo<Scout> repo)
        {
            _repo = repo;
        }

        public void OnGet()
        {
            try
            {
                // Sort by last name, then first name
                Scouts = _repo.ReadAll()
                    .OrderBy(s => s.FirstName)
                    .ThenBy(s => s.LastName)
                    .ToList();
            }
            catch (Exception)
            {
                ErrorMessage = "Kan ikke finde spejderne.";
            }
        }
    }
}