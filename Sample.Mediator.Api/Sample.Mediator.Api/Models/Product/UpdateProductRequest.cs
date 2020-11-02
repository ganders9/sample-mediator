namespace Sample.Mediator.Api.Models.Product
{
    public class UpdateProductRequest
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}