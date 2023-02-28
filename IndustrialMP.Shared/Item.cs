﻿namespace IndustrialMP.Shared
{
    public class Item
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        public string Text { get; set; } = string.Empty;
        public bool Online { get; set; }
    }
}