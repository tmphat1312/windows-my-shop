﻿using System.Text.Json.Serialization;

namespace MyShop.Core.Models;
public class Review
{
    [JsonPropertyName("id")]
    public string Id
    {
        get; set;
    }

    [JsonPropertyName("userId")]
    public string UserId
    {
        get; set;
    }

    [JsonPropertyName("bookId")]
    public string BookId
    {
        get; set;
    }

    [JsonPropertyName("rating")]
    public int Rating
    {
        get; set;
    }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt
    {
        get; set;
    }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt
    {
        get; set;
    }

    [JsonPropertyName("book")]
    public Book Book
    {
        get; set;
    }

    [JsonPropertyName("user")]
    public User User
    {
        get; set;
    }

    [JsonPropertyName("review")]
    public string Content
    {
        get; set;
    }
}
