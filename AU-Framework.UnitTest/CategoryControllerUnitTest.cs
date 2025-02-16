using AU_Framework.Application.Features.CategoryFeatures.Command.CreateCategory;
using AU_Framework.Domain.Dtos;
using AU_Framework.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AU_Framework.UnitTest
{
    public class CategoryControllerUnitTest
    {
        [Fact]
        public async void CREATE_ReturnsOkResult_WhenRequestIsValýd()
        {
            //Arrange
            var mediatorMock= new Mock<IMediator>();
            CreateCategoryCommand createCategoryCommand = new("Teknoloji");
            MessageResponse response = new("Kategori baþarýyla eklendi");
            CancellationToken cancellationToken = new();

            mediatorMock.Setup(m=>m.Send(createCategoryCommand,cancellationToken)).ReturnsAsync(response);

            CategoriesController categoriesController=new(mediatorMock.Object);
            //Act
            var result = await categoriesController.CreateCategory(createCategoryCommand, cancellationToken);
            //Asset
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<MessageResponse>(okResult.Value);
            Assert.Equal(response, returnValue);
            mediatorMock.Verify(m => m.Send(createCategoryCommand, cancellationToken), Times.Once);


        }
    }
}