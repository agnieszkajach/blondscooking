using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Windows.Storage;
using BlondsCooking.Model;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlondsCooking.Helpers
{
    public class XmlHelper
    {
        public static async Task SaveRecipesToXmlLocally(IList<Recipe> recipesList)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Recipe>));
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync(App.FileName, CreationCollisionOption.ReplaceExisting);
            Stream stream = await file.OpenStreamForWriteAsync();
            using (stream)
            {
                serializer.Serialize(stream, recipesList);
            }
        }

        public static async Task<IList<Recipe>> GetRecipesByCategory(string category )
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Recipe>));
            List<Recipe> listFromXml;
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync(App.FileName);
            Stream stream = await file.OpenStreamForReadAsync();
            using (stream)
            {
                listFromXml = (List<Recipe>)serializer.Deserialize(stream);
            }
            List<Recipe> recipes = listFromXml.Where(r => r.Category == category).ToList();
            return recipes;
        }

        public static async Task<Recipe> GetRecipeByTitle(string title)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Recipe>));
            List<Recipe> listFromXml;
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync(App.FileName);
            Stream stream = await file.OpenStreamForReadAsync();
            using (stream)
            {
                listFromXml = (List<Recipe>)serializer.Deserialize(stream);
            }
            var recipe = listFromXml.FirstOrDefault(r => r.Title == title);
            return recipe;
        }

        public static async Task<string> GetCategoryByTitle(string title)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Recipe>));
            List<Recipe> listFromXml;
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync(App.FileName);
            Stream stream = await file.OpenStreamForReadAsync();
            using (stream)
            {
                listFromXml = (List<Recipe>)serializer.Deserialize(stream);
            }
            var category = listFromXml.FirstOrDefault(r => r.Title == title).Category;
            return category;
        }

        public static async Task<IList<String>> GetTitlesOfRecipes()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Recipe>));
            List<Recipe> listFromXml;
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync(App.FileName);
            Stream stream = await file.OpenStreamForReadAsync();
            using (stream)
            {
                listFromXml = (List<Recipe>)serializer.Deserialize(stream);
            }
            var titlesList = listFromXml.Select(r => r.Title).ToList();
            return titlesList;
        }

        public static async Task<List<string>> GetImages()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Recipe>));
            List<Recipe> listFromXml;
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync(App.FileName);
            Stream stream = await file.OpenStreamForReadAsync();
            using (stream)
            {
                listFromXml = (List<Recipe>)serializer.Deserialize(stream);
            }
            var imagesList = listFromXml.Select(r => r.UrlToImage).ToList();
            return imagesList;
        }
    }
}
