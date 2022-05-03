using Beis.LearningPlatform.Web.ControllerHelpers;
using Beis.LearningPlatform.Web.ControllerHelpers.Interfaces;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigOptions = Microsoft.Extensions.Options.Options;

namespace Beis.LearningPlatform.Web.Tests.ControllerTests
{
    public class FormControllerBaseTest
    {
        protected ApplicationForm applicationForm = new();
        protected Mock<IDiagnosticToolControllerHelper> _diagnosticToolControllerHelper;
        protected IOptions<ApplicationForm> _applicationFormOptions;
        protected readonly IOptions<ComparisonToolDisplayOption> _ctDisplayOptions = ConfigOptions.Create(new ComparisonToolDisplayOption());
        protected readonly Mock<ILogger<DiagnosticToolFormService>> _loggerDiagnosticToolFormService = new();

        public FormControllerBaseTest()
        {

        }

        [OneTimeSetUp]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("diagnosticForm.json")
                 .AddEnvironmentVariables()
                 .Build();


            config.GetSection(nameof(ApplicationForm)).Bind(applicationForm);
            _applicationFormOptions = ConfigOptions.Create(applicationForm);

        }

        protected virtual FormTypes GetFormType()
        {
            return FormTypes.DiagnosticTool;
        }

        protected void Setup_ControllerHelperCreateForm()
        {
            _diagnosticToolControllerHelper.Setup(h => h.CreateForm(GetFormType()))
                .ReturnsAsync(new ControllerHelperOperationResponse<DiagnosticToolForm>(It.IsAny<System.Guid>(), true, Get_DiagnosticToolForm(1)));

            _diagnosticToolControllerHelper.Setup(h => h.Start(It.IsAny<DiagnosticToolForm>()))
                .ReturnsAsync(new ControllerHelperOperationResponse<DiagnosticToolForm>(It.IsAny<System.Guid>(), true, Get_DiagnosticToolForm(1)));
        }

        protected DiagnosticToolForm Get_DiagnosticToolForm(int currentStep)
        {
            var diagnosticToolFormService = new DiagnosticToolFormService(_loggerDiagnosticToolFormService.Object, _ctDisplayOptions, _applicationFormOptions);
            var diagnosticToolForm = diagnosticToolFormService.LoadNewForm(GetFormType());
            diagnosticToolForm.CurrStep = currentStep;
            return diagnosticToolForm;
        }

        protected void SetHttpContext(ControllerContext controllerContext)
        {
            var mockHttpContext = new Mock<HttpContext>();
            var mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.Setup(x => x.Scheme).Returns("http");
            mockHttpRequest.Setup(x => x.Host).Returns(new HostString("localhost"));
            mockHttpContext.Setup(r => r.Request).Returns(mockHttpRequest.Object);

            var mockSession = new Mock<ISession>();
            var val = Encoding.UTF8.GetBytes("1");
            mockSession.Setup(r => r.TryGetValue("loggedinUserId", out val)).Returns(true);
            mockHttpContext.Setup(r => r.Session).Returns(mockSession.Object);
            controllerContext.HttpContext = mockHttpContext.Object;
        }
    }
}
