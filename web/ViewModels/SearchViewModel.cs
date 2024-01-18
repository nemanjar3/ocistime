using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace web.ViewModels
{
    public class SearchViewModel
    {
        public SelectList Jobs { get; set; } // Contains JobID and JobName
        public int? SelectedJobID { get; set; } // Nullable int to hold the selected JobID from the dropdown
    }
}
