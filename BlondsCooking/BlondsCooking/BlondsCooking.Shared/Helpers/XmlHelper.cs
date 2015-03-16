using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Windows.Storage;
using BlondsCooking.Model;
using System.Threading.Tasks;

namespace BlondsCooking.Helpers
{
    public class XmlHelper
    {
        public static async Task SaveRecipesToXmlLocally(IList<Recipe> recipesList)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Recipe));
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync("recipes.xml", CreationCollisionOption.ReplaceExisting);
            Stream stream = await file.OpenStreamForWriteAsync();
            using (stream)
            {
                foreach(Recipe recipe in recipesList)               
                {
                    serializer.Serialize(stream, recipe);
                }
            }
        }
    }
}
