﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Photography_Project.Pages
{
    public class AestheticModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Aesthetic Photography";
        }
    }
}