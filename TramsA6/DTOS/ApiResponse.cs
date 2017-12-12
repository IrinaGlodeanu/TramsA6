using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TramsA6.DTOS
{
    // could be useful
    public class ApiResponse
    {
        public bool Status { get; set; }
        public User User { get; set; }
        public ModelStateDictionary ModelState { get; set; }
    }
}