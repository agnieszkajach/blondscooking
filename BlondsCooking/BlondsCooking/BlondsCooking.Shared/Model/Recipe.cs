using System;
using System.Collections.Generic;
using System.Text;

namespace BlondsCooking.Model
{
    public class Recipe
    {
        private String _category;
        private String _timeOfCooking;
        private int _temperatureOfCooking;
        private String _urlToImage;
        private String _text;
        private String _title;
        private String _ingredients;

        public String UrlToImage
        {
            get { return _urlToImage; }
            set
            {
                _urlToImage = value;
            }
        }

        public String Title
        {
            get { return _title; }
            set
            {
                _title = value;
            }
        }

        public String Category
        {
            get { return _category; }
            set
            {
                _category = value;
            }
        }

        public String Text
        {
            get { return _text; }
            set
            {
                _text = value;
            }
        }

        public String TimeOfCooking
        {
            get { return _timeOfCooking; }
            set
            {
                _timeOfCooking = value;
            }
        }

        public int TemperatureOfCooking
        {
            get { return _temperatureOfCooking; }
            set
            {
                _temperatureOfCooking = value;
            }
        }

        public String Ingredients
        {
            get { return _ingredients; }
            set
            {
                _ingredients = value;
            }
        }

        public Recipe(String category, String title, String text, String time, int temperature, String url, String ingredients)
        {
            this._category = category;
            this._title = title;
            this._text = text;
            this._timeOfCooking = time;
            this._temperatureOfCooking = temperature;
            this._urlToImage = url;
            this._ingredients = ingredients;
        }
    }
}
