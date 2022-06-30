using ReStore.Domain.Entities;

namespace ReStore.Application.DTOs;

public class CreateOrderDto
{
     public bool SaveAddress { get; set; }
     public ShippingAddress ShippingAddress { get; set; }
}
