using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using web.Models;

namespace web.ViewModels
{
    public class ApplicationViewModel
    {
        public IEnumerable<Application> Applications { get; set; }
        public Application NewApplication { get; set; }
        public SelectList Jobs { get; set; }
        
    }
}
