using Pathfinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pathfinder.ViewModels
{
    public class TagViewer
    {
        //public int CharacterId { get; set; }
        //public int CategoryId { get; set; }
        //public List<TagCategory> Categories { get; set; }
        //public List<Tag> Tags { get; set; }

        //private PathfinderContext db = new PathfinderContext();

        //public TagViewer()
        //{
        //    this.Categories = LoadCategories(0);
        //    this.Tags = new List<Tag>();
        //}

        //public TagViewer(int characterId) : this(characterId, 0) { }

        //public TagViewer(int characterId, int categoryId)
        //{
        //    this.CharacterId = characterId;
        //    this.CategoryId = categoryId;
            
        //    this.Categories = LoadCategories(characterId);
        //    this.Tags = LoadTags(characterId, categoryId);
        //}

        //public List<TagCategory> LoadCategories(int characterId)
        //{
        //    List<TagCategory> categories = db.TagCategories
        //        .Where(m => m.CharacterId == characterId)
        //        .OrderBy(m => m.Name)
        //        .ToList<TagCategory>();

        //    TagCategory allCategory = new TagCategory();
        //    allCategory.TagCategoryId = 0;
        //    allCategory.CharacterId = characterId;
        //    allCategory.Name = "All";
        //    categories.Insert(0, allCategory);
            
        //    return categories;
        //}

        //public List<Tag> LoadTags(int characterId, int categoryId)
        //{
        //    List<Tag> tags;

        //    if (categoryId == 0)
        //    {
        //        tags = (from tag in db.Tags
        //                where tag.CharacterId == characterId
        //                select tag).ToList<Tag>();
        //    }
        //    else
        //    {
        //        tags = (from tag in db.Tags
        //                where tag.CharacterId == characterId
        //                && tag.TagCategoryId == categoryId
        //                select tag).ToList<Tag>();
        //    }

        //    return tags;
        //}
    }
}