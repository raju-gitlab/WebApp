﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Model.Models;

namespace WP.Repository.Interfaces
{
    public interface IPostsRepository
    {
        Task<bool> IsValid(string pageName);
        Task<string> CreatePage(PageModel page);

    }
}
