﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Photography_Project.Pages
{
    public class ModelModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Model Photography";
        }
    }
}