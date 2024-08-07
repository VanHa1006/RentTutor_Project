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
        private readonly IUserBusiness _customerBusiness;

        public StudentManagerModel(IUserBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }
        public string Message { get; set; } = default!;
        public Paginate<User> Customer { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int Size { get; set; } = 10;

        private async Task<Paginate<User>> GetCustomers()
        {
            var result = await _customerBusiness.GetAll(PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {
                var customer = result.Data;
                return (Paginate<User>)customer;
            }
            return null;
        }

        private async Task<Paginate<User>> Search()
        {
            var result = await _customerBusiness.Search(SearchTerm, PageIndex, Size);
            if (result.Status > 0 && result.Data != null)
            {
                var customer = result.Data;
                return (Paginate<User>)customer;
            }
            return null;
        }
        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Customer = await Search();
            }
            else
            {
                Customer = await GetCustomers();
            }
        }
    }
}

