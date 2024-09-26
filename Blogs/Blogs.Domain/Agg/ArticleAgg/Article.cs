using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.SeedWorks.Base;

namespace Blogs.Domain.Agg.ArticleAgg
{
    public class Article : BaseEntityUpdateActive<int>
    {

        public string Title { get; private set; }

        public string ImageName { get; private set; }

        public string ImageAlt { get; private set; }

        public int UserId { get; private set; }

        public string Author { get; private set; }

        public int CategoryId { get; private set; }

        public int SubCategoryId { get; private set; }

        public string Slug { get; private set; }

        public string Summary { get; private set; }

        public string Content { get; private set; }

        public int TotalVisits { get; private set; }

        public bool IsSpecial { get; private set; }

        public int Likes { get; private set; }

        public int Dislikes { get; private set; }

        public DateTime? UpdateDate { get; private set; }

        public Article(string title, string imageName, string imageAlt, int userId, string author, int categoryId, int subCategoryId, string slug, string summary, string content, bool isSpecial)
        {
            Title = title;
            ImageName = imageName;
            ImageAlt = imageAlt;
            UserId = userId;
            Author = author;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
            Slug = slug;
            Summary = summary;
            Content = content;
            IsSpecial = isSpecial;
        }

        public void Edit(string title, string imageName, string imageAlt, int categoryId, int subCategoryId, string slug, string summary, string content, bool isSpecial)
        {
            Title = title;
            ImageName = imageName;
            ImageAlt = imageAlt;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
            Slug = slug;
            Summary = summary;
            Content = content;
            IsSpecial = isSpecial;
            UpdateDate = DateTime.Now;
        }

        public void IncreaseLikes()
        {
            Likes = Likes + 1;
        }

        public void IncreaseDislikes()
        {
            Dislikes = Dislikes + 1;
        }

        public void ChangeSpecialation()
        {
            IsSpecial = !IsSpecial;
        }

        public void IncreaseTotalVisits()
        {
            TotalVisits = TotalVisits + 1;
        }
    }
}
