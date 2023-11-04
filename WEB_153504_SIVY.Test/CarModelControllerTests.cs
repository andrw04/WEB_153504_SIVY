using Microsoft.AspNetCore.Hosting;
using Moq;
using WEB_153504_SIVY.Services.CarBodyService;
using WEB_153504_SIVY.Controllers;
using WEB_153504_SIVY.Services.CarModelService;
using Microsoft.AspNetCore.Mvc;
using WEB_153504_SIVY.Domain.Models;
using WEB_153504_SIVY.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Xunit.Sdk;

namespace WEB_153504_SIVY.Test
{
    public class CarModelControllerTests
    {

        [Fact]
        public async void InvalidCarModelListReturn404()
        {
            // Arange
            var carBodyTypeService = new Mock<ICarBodyTypeService>();
            var carModelService = new Mock<ICarModelService>();

            string errorMessage = "Error Message";

            var carModels = Task.FromResult(new ResponseData<CarModelListModel<CarModel>>() { Data = new(), Success = false, ErrorMessage = errorMessage });
            carModelService.Setup(m => 
                m.GetCarModelListAsync(It.IsAny<string?>(), It.IsAny<int>())).Returns(carModels);

            var controller = new CarModelController(carModelService.Object, carBodyTypeService.Object);

            // Act
            var result = controller.Index(null).Result;

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(errorMessage, (result as NotFoundObjectResult).Value);

        }

        [Fact]
        public void InvalidCarBodyTypeReturn404()
        {
            // Arange
            var carBodyTypeService = new Mock<ICarBodyTypeService>();
            var carModelService = new Mock<ICarModelService>();

            string errorMessage = "Error Message";

            var carModels = Task.FromResult(new ResponseData<CarModelListModel<CarModel>>() { Data = new(), Success = true, ErrorMessage = "" });
            carModelService.Setup(m =>
                m.GetCarModelListAsync(It.IsAny<string?>(), It.IsAny<int>())).Returns(carModels);

            var carBodyTypes = Task.FromResult(new ResponseData<List<CarBodyType>>() { Data = new(), Success = false, ErrorMessage = errorMessage });
            carBodyTypeService.Setup(m =>
                m.GetCarBodyTypeListAsync()).Returns(carBodyTypes);

            var controller = new CarModelController(carModelService.Object, carBodyTypeService.Object);

            // Act
            var result = controller.Index(null).Result;

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(errorMessage, (result as NotFoundObjectResult).Value);

        }

        [Fact]
        public void SuccessfullAcceptedDataReturnView()
        {
            // Arange
            var carBodyTypeService = new Mock<ICarBodyTypeService>();
            var carModelService = new Mock<ICarModelService>();

            var carModels = Task.FromResult(new ResponseData<CarModelListModel<CarModel>>() { Data = new(), Success = true, ErrorMessage = "" });
            carModelService.Setup(m =>
                m.GetCarModelListAsync(It.IsAny<string?>(), It.IsAny<int>())).Returns(carModels);

            var carBodyTypes = Task.FromResult(new ResponseData<List<CarBodyType>>() { Data = new(), Success = true, ErrorMessage = "" });
            carBodyTypeService.Setup(m =>
                m.GetCarBodyTypeListAsync()).Returns(carBodyTypes);

            var controller = new CarModelController(carModelService.Object, carBodyTypeService.Object);

            // Act
            var result = controller.Index(null).Result;

            // Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(carBodyTypes.Result.Data, controller.ViewBag.Categories);
            Assert.Null(controller.ViewBag.CurrentCategory);
            Assert.Equal(carModels.Result, viewResult.Model);
        }
    }
}