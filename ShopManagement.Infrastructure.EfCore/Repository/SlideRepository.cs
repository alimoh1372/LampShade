using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.SlideContracts;
using SM.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class SlideRepository:BaseRepository<long,Slide>,ISlideRepository
    {
        private readonly ShopContext _context;
        public SlideRepository(ShopContext context) : base(context)
        {
            _context = context;
        }


        public List<SlideViewModel> Search(SlideSearchModel searchModel = null)
        {
            var query = _context.Slides.Select(x => new SlideViewModel
            {
                Id=x.Id,
                Picture = x.Picture,
                Heading = x.Heading,
                Title = x.Title,
                Text = x.Text,
                CreationDate = x.CreationDate.ToFarsi(),
                Link=x.Link,
                IsRemoved=x.IsRemoved
            });
            if (searchModel==null)
            {
               return query.OrderByDescending(x => x.Id).ToList();
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Text))
            {
                query = query.Where(x => x.Text.Contains(searchModel.Text));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Heading))
            {
                query = query.Where(x => x.Heading.Contains(searchModel.Heading));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Title))
            {
                query = query.Where(x => x.Title.Contains(searchModel.Title));
            }
            return query.OrderByDescending(x => x.Id).ToList();
        }

        public EditSlide GetDetails(long id)
        {
            EditSlide editSlide = _context.Slides.Select(x => new EditSlide
            {
                PicturePath = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Heading = x.Heading,
                Title = x.Title,
                Text = x.Text,
                BtnText = x.BtnText,
                Id = x.Id
            }).FirstOrDefault(x => x.Id == id);
            return editSlide;
        }

    }
}