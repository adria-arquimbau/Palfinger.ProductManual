using System.Collections.Generic;
using MediatR;
using Palfinger.ProductManual.Domain.Commands.CreateProduct.Models;

namespace Palfinger.ProductManual.Domain.Commands.CreateProduct
{
    public class CreateProductCommandRequest : IRequest
    {
        public readonly string Name;
        public readonly string Description;
        public readonly string ImageUrl;
        public readonly IEnumerable<CreateAttributeRequest> Attributes;

        public CreateProductCommandRequest(string name, string description, string imageUrl, IEnumerable<CreateAttributeRequest> attributes)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Attributes = attributes;
        }
    }       
}   