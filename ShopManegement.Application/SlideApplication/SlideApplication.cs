using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.SlideContracts;
using SM.Domain.SlideAgg;

namespace ShopManagement.Application.SlideApplication
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IFileUpload _fileUpload;
        public SlideApplication(ISlideRepository slideRepository, IFileUpload fileUpload)
        {
            _slideRepository = slideRepository;
            _fileUpload = fileUpload;
        }

        public OperationResult Create(CreateSlide command)
        {
            OperationResult result = new OperationResult();
            

            if (_slideRepository.IsExists(x => command.Picture.FileName.Contains(x.Picture) && x.Title == command.Title))
            {
                return result.Failed(ApplicationMessage.Duplication);
            }
            string basePath = $"UploadedFiles/Slides/";
            string picture = _fileUpload.UploadFile(command.Picture, basePath);
            Slide slide = new Slide(picture, command.PictureAlt, command.PictureTitle, command.Heading,
                command.Title, command.Text, command.BtnText, command.Link);
            _slideRepository.Create(slide);
            _slideRepository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult Edit(EditSlide command)
        {
            OperationResult result = new OperationResult();

            Slide slide = _slideRepository.Get(command.Id);
            if (slide == null)
            {
                return result.Failed(ApplicationMessage.NotFound);
            }
           
            if (_slideRepository.IsExists(x =>
                    command.Picture.FileName.Contains(x.Picture) 
                    && x.Title == command.Title && x.Id != command.Id))
            {
                return result.Failed(ApplicationMessage.Duplication);
            }

            string pictureBeforeUpdateSlide = slide.Picture;

            string basePath = $"UploadedFiles/Slides";
            string picture = _fileUpload.UploadFile(command.Picture, basePath);

            slide.Edit(picture, command.PictureAlt, command.PictureTitle, command.Heading,
                command.Title, command.Text, command.BtnText,command.Link); 
            _slideRepository.SaveChanges();
            _fileUpload.DeleteFile(pictureBeforeUpdateSlide);
            return result.Succedded();
        }

        public OperationResult DeActive(long id)
        {
            OperationResult result = new OperationResult();

            Slide slide = _slideRepository.Get(id);
            if (slide == null)
            {
                return result.Failed(ApplicationMessage.NotFound);
            }



            slide.DeActive();
            _slideRepository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult Active(long id)
        {
            OperationResult result = new OperationResult();

            Slide slide = _slideRepository.Get(id);
            if (slide == null)
            {
                return result.Failed(ApplicationMessage.NotFound);
            }



            slide.Active();
            _slideRepository.SaveChanges();
            return result.Succedded();
        }

        public List<SlideViewModel> Search(SlideSearchModel searchModel)
        {
          return  _slideRepository.Search(searchModel);
        }

        public EditSlide GetDetails(long id)
        {
            return _slideRepository.GetDetails(id);
        }
    }
}