using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Model.Models;

namespace WP.Repository.Interfaces
{
    public interface IPagesRepository
    {
        Task<List<PageCardModel>> GetPagesByUserId(string UserId);
        Task<List<PageModel>> ListPages();
        Task<Tuple<PageModel, List<PostsViewModel>>> PageById(string PageId);
        Task<List<PageModel>> ListPagesbyfilter(string[] filters);
        Task<bool> IsValid(string pageName);
        Task<string> CreatePage(PageModel page);
        Task<string> ModifyPage(PageModifyModel page);
        Task<bool> DeletePage(string UserId, string PageId);
    }
}
