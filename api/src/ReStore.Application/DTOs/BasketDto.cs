﻿namespace ReStore.Application.DTOs;

public class BasketDto
{
     public int Id { get; set; }
     public string BuyerId { get; set; }
     public List<BasketItemDto> Items { get; set; }
     // for payment
     public string? PaymentIntentId { get; set; }
     public string? ClientSecret { get; set; }
}
