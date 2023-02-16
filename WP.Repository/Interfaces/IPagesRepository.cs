﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Model.Models;

namespace WP.Repository.Interfaces
{
    public interface IPagesRepository
    {
        Task<List<PageModel>> ListPages();
        Task<List<PageModel>> ListPagesbyfilter(string[] filters);
        Task<bool> IsValid(string pageName);
        Task<string> CreatePage(PageModel page);
        Task<string> ModifyPage(PageModifyModel page);
        //Task<bool> DeltePage(string UserId, string PageId);
    }
}
