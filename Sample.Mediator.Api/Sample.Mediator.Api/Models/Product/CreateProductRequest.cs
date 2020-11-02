using System.ComponentModel.DataAnnotations;

namespace Sample.Mediator.Api.Models.Product
{
    public class CreateProductRequest
    {
        [Required]
        public string Code { get; set; }

        [MinLength(5)]
        public string Name { get; set; }

        [Range(0, 1000)]
        public decimal Price { get; set; }
    }
}