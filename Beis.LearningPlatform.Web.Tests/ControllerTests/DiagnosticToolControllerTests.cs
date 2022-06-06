using Beis.LearningPlatform.Web.ComparisonTool.Models;
using Beis.LearningPlatform.Web.ControllerHelpers;
using Beis.LearningPlatform.Web.ControllerHelpers.Interfaces;
using Beis.LearningPlatform.Web.Controllers;
using Beis.LearningPlatform.Web.Interfaces;
using Beis.LearningPlatform.Web.Models.DiagnosticTool;
using Beis.LearningPlatform.Web.Options;
using Beis.LearningPlatform.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConfigOptions = Microsoft.Extensions.Options.Options;

namespace Beis.LearningPlatform.Web.Tests.ControllerTests
{
    public class DiagnosticToolControllerTests : FormControllerBaseTest
    {
        private Mock<ILogger<DiagnosticToolController>> _logger;
        private Mock<IComparisonToolService> _comparisonToolService;
        private DiagnosticToolController controller;


        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<DiagnosticToolController>>();
            _diagnosticToolControllerHelper = new();
            _comparisonToolService = new();
            _ctDisplayOptions.Value.LoadFormFromJson = true;
            
            controller = new DiagnosticToolController(_logger.Object, _diagnosticToolControllerHelper.Object, _comparisonToolService.Object, _ctDisplayOptions);
            SetHttpContext(controller.ControllerContext);
        }

        [Test]
        public async Task Start_WhenCalled_ReturnsOK()
        {
            
            //Arrange
            Setup_ControllerHelperCreateForm();

            //Act
            var result = await controller.Start();

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task StartForm_Post_WhenCalled_ReturnsOK()
        {
            //Arrange
            _diagnosticToolControllerHelper.Setup(h => h.Start(It.IsAny<DiagnosticToolForm>()))
                .ReturnsAsync(new ControllerHelperOperationResponse(It.IsAny<Guid>(), true));

            //Act
            var result = await controller.StartForm(Get_DiagnosticToolForm(1));

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task NextStep_Post_WhenCalled_ReturnsOK()
        {
            //Arrange
            _diagnosticToolControllerHelper.Setup(h => h.NextStep(It.IsAny<DiagnosticToolForm>(), true, It.IsAny<string[]>()))
                .ReturnsAsync(new ControllerHelperOperationResponse<DiagnosticToolForm>(It.IsAny<Guid>(), true, Get_DiagnosticToolForm(2)));

            //Act
            var result = await controller.NextStep(Get_DiagnosticToolForm(1));

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task PrevStep_Post_WhenCalled_ReturnsOK()
        {
            //Arrange
            _diagnosticToolControllerHelper.Setup(h => h.PreviousStep(It.IsAny<DiagnosticToolForm>()))
                .ReturnsAsync(new ControllerHelperOperationResponse<DiagnosticToolForm>(It.IsAny<Guid>(), true, Get_DiagnosticToolForm(1)));

            //Act
            var result = await controller.PrevStep(Get_DiagnosticToolForm(2));

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task GoToStep_WhenCalled_ReturnsOK()
        {
            //Arrange
            var goToStep = 3;
            _diagnosticToolControllerHelper.Setup(h => h.GotoStep(It.IsAny<DiagnosticToolForm>(), goToStep))
                .ReturnsAsync(new ControllerHelperOperationResponse<DiagnosticToolForm>(It.IsAny<Guid>(), true, Get_DiagnosticToolForm(goToStep)));

            //Act
            var result = await controller.GoToStep(Get_DiagnosticToolForm(2), goToStep);

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task Result_Post_SoftwareNeedNotKnown_ReturnsOK()
        {
            //Arrange
            _diagnosticToolControllerHelper.Setup(h => h.ProcessResults(It.IsAny<DiagnosticToolForm>(), FormTypes.DiagnosticTool))
                .ReturnsAsync(new ControllerHelperOperationResponse<bool>(It.IsAny<Guid>(), true, true));

            var model = Get_DiagnosticToolForm(8);

            //Act
            var result = await controller.Result(model);

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task Result_SoftwareNeedKnown_ReturnsOK()
        {
            //Arrange
            var ctpList = new List<ComparisonToolProduct>
            {
                new ComparisonToolProduct
                {
                    product_id = 1,
                }
            };

            _comparisonToolService.Setup(c => c.GetProducts())
                .ReturnsAsync(ctpList);
            _comparisonToolService.Setup(c => c.GetApprovedProductsFromApprovedVendors())
                .ReturnsAsync(ctpList);

            _diagnosticToolControllerHelper.Setup(h => h.ProcessResults(It.IsAny<DiagnosticToolForm>(), FormTypes.DiagnosticTool))
                .ReturnsAsync(new ControllerHelperOperationResponse<bool>(It.IsAny<Guid>(), true, true));


            var model = Get_DiagnosticToolForm(8);

            //set Software need known to yes
            model.steps[5].elements[0].value = "Yes";

            //set CRM software selected
            model.steps[6].elements[0].answerOptions[0].value = "true";

            //Act
            var result = await controller.Result(model);

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task NextStep_Get_WhenCalled_ReturnsOK()
        {
            //Arrange
            Setup_ControllerHelperCreateForm();

            //Act
            var result = await controller.NextStep();

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task PrevStep_Get_WhenCalled_ReturnsOK()
        {
            //Arrange
            Setup_ControllerHelperCreateForm();

            //Act
            var result = await controller.PrevStep();

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task StartForm_Get_WhenCalled_ReturnsOK()
        {
            //Arrange
            Setup_ControllerHelperCreateForm();

            //Act
            var result = await controller.StartForm();

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task Result_Get_WhenCalled_ReturnsOK()
        {
            //Arrange
            Setup_ControllerHelperCreateForm();

            //Act
            var result = await controller.Result();

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        #region Existing code commented

        //[Test]
        //public void Mock_httpContext_Returns_Scheme()
        //{
        //    var httpContextScheme = httpContext.Request.Scheme;
        //    Assert.IsNotEmpty(httpContextScheme);
        //}



        //[Test]
        //public void Mock_httpContext_Returns_Host()
        //{
        //    var httpContextHost = httpContext.Request.Host.Host;
        //    Assert.IsNotEmpty(httpContextHost);
        //}

        /*TODO: These tests have been removed as part of the rework of the settings configuration loader.  This should be refactored to not rely on external JSON files with mock data.
        [Test]
        public async Task Call_To_Start_Returns_OK()
        {
            var controller = CreateController();
            controller.ControllerContext.RouteData = new Microsoft.AspNetCore.Routing.RouteData();
            controller.ControllerContext.RouteData.Values["controller"] = "diagnostic-tool";
            controller.ControllerContext.RouteData.Values["action"] = "start";
            controller.TempData = _TempData.Object;

            ViewResult view = await controller.Start() as ViewResult;
            Assert.NotNull(view);
        }


        [Test]
        public async Task Call_To_DiagnosticTool_Returns_OK()
        {
            var controller = CreateController();
            controller.ControllerContext.RouteData = new Microsoft.AspNetCore.Routing.RouteData();
            controller.ControllerContext.RouteData.Values["controller"] = "diagnostic-tool";
            controller.ControllerContext.RouteData.Values["action"] = "start";
            controller.TempData = _TempData.Object;


            ViewResult view = await controller.DiagnosticTool() as ViewResult;
            Assert.NotNull(view);
        }

        [Test]
        public async Task Call_To_StartForm_Returns_OK()
        {
            var controller = CreateController();
            controller.ControllerContext.RouteData = new Microsoft.AspNetCore.Routing.RouteData();
            controller.ControllerContext.RouteData.Values["controller"] = "diagnostic-tool";
            controller.ControllerContext.RouteData.Values["action"] = "start";
            controller.TempData = _TempData.Object;


            ViewResult view = await controller.StartForm() as ViewResult;
            Assert.NotNull(view);
        }


        [Test]
        public async Task Call_To_Result_Returns_OK()
        {
            var controller = CreateController();
            controller.ControllerContext.RouteData = new Microsoft.AspNetCore.Routing.RouteData();
            controller.ControllerContext.RouteData.Values["controller"] = "diagnostic-tool";
            controller.ControllerContext.RouteData.Values["action"] = "start";
            controller.TempData = _TempData.Object;


            ViewResult view = await controller.Result() as ViewResult;
            Assert.NotNull(view);
        }



        [Test]
        public async Task Call_To_PrevStep_Returns_OK()
        {
            var controller = CreateController();
            controller.ControllerContext.RouteData = new Microsoft.AspNetCore.Routing.RouteData();
            controller.ControllerContext.RouteData.Values["controller"] = "diagnostic-tool";
            controller.ControllerContext.RouteData.Values["action"] = "start";
            controller.TempData = _TempData.Object;


            ViewResult view = await controller.PrevStep() as ViewResult;
            Assert.NotNull(view);
        }

        */

        /*
        [Test]
        public async Task Call_To_NextStep_Returns_OK()
        {
            var controller = CreateController();
            controller.ControllerContext.RouteData = new Microsoft.AspNetCore.Routing.RouteData();
            controller.ControllerContext.RouteData.Values["controller"] = "diagnostic-tool";
            controller.ControllerContext.RouteData.Values["action"] = "start";
            controller.TempData = _TempData.Object;


            ViewResult view = await controller.NextStep() as ViewResult;
            Assert.NotNull(view);
        }

        [Test]
        public async Task Call_To_StartForm_HttpPost_Returns_OK()
        {
            var controller = CreateController();
            controller.ControllerContext.RouteData = new Microsoft.AspNetCore.Routing.RouteData();
            controller.ControllerContext.RouteData.Values["controller"] = "diagnostic-tool";
            controller.ControllerContext.RouteData.Values["action"] = "start-form";
            controller.TempData = _TempData.Object;


            ViewResult view = await controller.StartForm(new Form()) as ViewResult;
            Assert.NotNull(view);
        }

        [Test]
        public async Task Call_To_Next_Returns_OK()
        {
            var controller = CreateController();
            controller.ControllerContext.RouteData = new Microsoft.AspNetCore.Routing.RouteData();
            controller.ControllerContext.RouteData.Values["controller"] = "diagnostic-tool";
            controller.ControllerContext.RouteData.Values["action"] = "NextStep";
            controller.TempData = _TempData.Object;

            ViewResult viewResult = await controller.StartForm(new Form()) as ViewResult;
            var model = (Form)viewResult.Model;
            ViewResult view = await controller.NextStep(model) as ViewResult;
            Assert.NotNull(view);
        }


        [Test]
        public async Task Call_To_PrevStep_HttpPost_Returns_OK()
        {
            var controller = CreateController();
            controller.ControllerContext.RouteData = new Microsoft.AspNetCore.Routing.RouteData();
            controller.ControllerContext.RouteData.Values["controller"] = "diagnostic-tool";
            controller.ControllerContext.RouteData.Values["action"] = "PrevStep";
            controller.TempData = _TempData.Object;

            ViewResult viewResult = await controller.StartForm(new Form()) as ViewResult;
            var model = (Form)viewResult.Model;
            ViewResult view = await controller.PrevStep(model) as ViewResult;
            Assert.NotNull(view);
        }


        [Test]
        public async Task Call_To_GoToStep_Returns_OK()
        {
            var controller = CreateController();
            controller.ControllerContext.RouteData = new Microsoft.AspNetCore.Routing.RouteData();
            controller.ControllerContext.RouteData.Values["controller"] = "diagnostic-tool";
            controller.ControllerContext.RouteData.Values["action"] = "GoToStep";
            controller.TempData = _TempData.Object;

            ViewResult viewResult = await controller.StartForm(new Form()) as ViewResult;
            var model = (Form)viewResult.Model;
            ViewResult view = await controller.GoToStep(model, 1) as ViewResult;
            Assert.NotNull(view);
        }



        [Test]
        public async Task Call_To_Result_HttpPost_Returns_OK()
        {
            var controller = CreateController();
            controller.ControllerContext.RouteData = new Microsoft.AspNetCore.Routing.RouteData();
            controller.ControllerContext.RouteData.Values["controller"] = "diagnostic-tool";
            controller.ControllerContext.RouteData.Values["action"] = "GoToStep";
            controller.TempData = _TempData.Object;

            ViewResult viewResult = await controller.StartForm(new Form()) as ViewResult;
            var model = (Form)viewResult.Model;
            ViewResult view = await controller.Result(model) as ViewResult;
            Assert.NotNull(view);
        }
        */
        //[Test]
        //public async Task Test_View_Returns_OK()
        //{
        //var controller = new DiagnosticToolController(_logger.Object, _Configuration.Object, _CookieService.Object, _DiagnosticToolService.Object, _CMSservice.Object, _MakeApiCallService);
        //controller.ControllerContext.RouteData.Values["controller"] = "diagnostic-tool";
        //    controller.ControllerContext.RouteData.Values["action"] = "start";
        //    ViewResult view = await controller.Start() as ViewResult;

        //    Assert.NotNull(view);
        //}

        #endregion Existing code commented
    }
}