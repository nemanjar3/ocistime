using System.Collections.Generic;
using web.Models; // Adjust the namespace to where your Review and Worker models are located
using Microsoft.AspNetCore.Mvc.Rendering; // Required for SelectList

namespace web.ViewModels // Adjust the namespace to match your project
{
    public class ReviewViewModel
    {
        public IEnumerable<Review> Reviews { get; set; }
        public Review NewReview { get; set; }
        public SelectList Workers { get; set; } // SelectList for the Worker IDs
        public int? SelectedWorkerID { get; set; } // Nullable int to hold the selected Worker ID

        public ReviewViewModel()
        {
            Reviews = new List<Review>();
            NewReview = new Review();
            // Workers initialization will be handled in the Controller
        }
    }
}
