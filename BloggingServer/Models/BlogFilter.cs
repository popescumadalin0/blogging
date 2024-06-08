using System;
using System.Collections.Generic;

namespace Models;

public class BlogFilter
{
    public string FilterValue { get; set; }

    public List<string> BlogCategories { get; set; }
}