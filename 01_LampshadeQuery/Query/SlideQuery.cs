using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _01_LampshadeQuery.Contracts;
using _01_LampshadeQuery.Contracts.Slide;
using ShopManagement.Application.Contracts.SlideContracts;
using ShopManagement.Infrastructure.EfCore;

namespace _01_LampshadeQuery.Query
{
    public class SlideQuery:ISlideQuery
    {
        private readonly ShopContext _context;

        public SlideQuery(ShopContext context)
        {
            _context = context;
        }

        public List<SlideQueryModel> GetSlides()
        {
            List<SlideQueryModel> slideViewModels;
            slideViewModels = _context.Slides.Where(x => x.IsRemoved == false)
                .Select(x => new SlideQueryModel
                {
                    Picture = x.Picture,
                    Heading = x.Heading,
                    Title = x.Title,
                    Text = x.Text,
                    CreationDate = x.CreationDate.ToString(CultureInfo.InvariantCulture),
                    Link =x.Link,
                    BtnText=x.BtnText
                }).ToList();
            return slideViewModels;
        }
    }
}