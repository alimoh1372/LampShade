using System.ComponentModel;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contracts.ArticleContracts
{
    public class EditArticle : CreateArticle
    {
        public long Id { get; set; }
        [DisplayName("تصویر مقاله")]

        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidatingMessage.MaxLength)]
        [FileExtension(new[] { ".jpeg", ".jpg", ".png" })]
        public new IFormFile Picture { get; set; }
        public string PicturePath { get; set; }
    }
}