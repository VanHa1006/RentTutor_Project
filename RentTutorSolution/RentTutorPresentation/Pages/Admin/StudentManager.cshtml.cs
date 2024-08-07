using BusinessAccess.Business;
using BusinessAccess.Services;
using DataAccess.Models;
using DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RentTutorPresentation.Pages.Admin
{
    public class StudentManagerModel : PageModel
    {
        private readonly IStudentsBusiness _studentsBusiness;

        public StudentManagerModel(IStudentsBusiness studentsBusiness)
        {
            _studentsBusiness = studentsBusiness;
        }

        public IList<DataAccess.Models.Student> Students { get; set; }

        public async Task OnGetAsync()
        {
            Students = await _studentsBusiness.GetAllStudentsAsync();
        }
    }
}

