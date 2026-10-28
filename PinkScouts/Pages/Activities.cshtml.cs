using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PSClassLib.Interface;
using PSClassLib.Model;

namespace PinkScouts.Pages
{
    public class ActivitiesModel : PageModel
    {
        private readonly IRepo<Activity> _repo;

        public List<Activity> Activities { get; private set; } = new List<Activity>();
        public string? ErrorMessage { get; private set; }

        public ActivitiesModel(IRepo<Activity> repo)
        {
            _repo = repo;
        }

        public void OnGet()
        {
            try
            {
                Activities = _repo.ReadAll();
            }
            catch (Exception)
            {
                ErrorMessage = "Vi kunne ikke hente aktiviteterne lige nu. Prøv igen senere.";
            }
        }
    }
}