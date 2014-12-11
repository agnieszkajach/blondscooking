using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage.Table;

namespace BlondsCooking.Model
{
    public class Recipe : TableEntity
    {
        public string Category { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Ingredients { get; set; }

        public string Temperature { get; set; }

        public string Time { get; set; }

        public string UrlToImage { get; set; }

    }
}
