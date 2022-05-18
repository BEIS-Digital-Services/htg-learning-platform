using AutoMapper;
using Beis.LearningPlatform.BL.Configuration;
using Beis.LearningPlatform.BL.Domain;
using Beis.LearningPlatform.BL.IntegrationServices;
using Beis.LearningPlatform.BL.IntegrationServices.Options;
using Beis.LearningPlatform.BL.Models;
using Beis.LearningPlatform.DAL;
using Beis.LearningPlatform.Library;
using Beis.LearningPlatform.Library.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Beis.LearningPlatform.BL.Services
{
    /// <summary>
    /// A class that defines an implementation of an email service.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly IEmailDataService _emailDataService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly INotifyIntegrationService _notifyIntegrationService;
        private readonly IEmailService _thisInterface;
        private readonly NotifyServiceOption _notifyServiceOption;

        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="logger">An ILogger that is the logger to use.</param>
        /// <param name="mapper">An IMapper that is the object mapper to use.</param>
        /// <param name="notifyIntegrationService">An INotifyIntegrationService that is the Notify integration service to use.</param>
        /// <param name="emailDataService">An IEmailDataService that is the email data service to use.</param>
        /// <param name="notifyServiceOptions">An IOptions<NotifyServiceOption> is the configuration option to use.</param>
        public EmailService(ILogger<EmailService> logger,
                            IMapper mapper,
                            INotifyIntegrationService notifyIntegrationService,
                            IEmailDataService emailDataService,
                            IOptions<NotifyServiceOption> notifyServiceOptions)
        {
            _logger = logger;
            _mapper = mapper;
            _notifyIntegrationService = notifyIntegrationService;
            _emailDataService = emailDataService;
            _notifyServiceOption = notifyServiceOptions.Value;
            _thisInterface = this;
        }

        async Task<IServiceResponse<bool>> IEmailService.IsUnsubscribed(Guid requestID, string emailAddress)
        {
            var isSuccessful = false;
            string message = default;
            var returnValue = false;

            var results = await _emailDataService.GetByEmail(emailAddress);
            if (results.Length > 0)
            {
                isSuccessful = true;

                if (results.Where(x => x.IsUnsubscribed ?? false).Count() == results.Length)
                    returnValue = true;
            }
            else
                message = "The specified email address does not exist";

            return new ServiceResponse<bool>(requestID, isSuccessful, message, returnValue);
        }

        IServiceResponse IEmailService.IsValidEmailAddress(Guid requestID, string emailAddress)
        {
            var returnValue = false;

            if (!string.IsNullOrWhiteSpace(emailAddress) &&
                emailAddress.Contains('@') &&
                !emailAddress.EndsWith('.'))
            {
                // LP-823: email address must be in format [string]@[string].[string]
                string[] parts = emailAddress.Split(new[] { '@' });
                if (parts.Length >= 2 && parts[^1].Contains('.'))
                {
                    try
                    {
                        var mailAddress = new MailAddress(emailAddress);
                        returnValue = mailAddress.Address == emailAddress;
                    }
                    catch (Exception)
                    { }
                }
            }

            return new ServiceResponse(requestID, returnValue);
        }

        async Task<IServiceResponse<int>> IEmailService.SaveEmailAddress(Guid requestID, DiagnosticToolEmailAnswerDto diagnosticToolEmailAnswer)
        {
            var isSuccessful = false;
            string message = default;
            int returnValue = default;

            if (diagnosticToolEmailAnswer != default)
            {
                returnValue = await _emailDataService.Add(diagnosticToolEmailAnswer);
                isSuccessful = true;
            }
            else
                throw new ArgumentNullException(nameof(diagnosticToolEmailAnswer));

            return new ServiceResponse<int>(requestID, isSuccessful, message, returnValue);
        }
        private static DiagnosticToolQuestion6Type ConvertQuestion6Type(string question6Type)
        {
            DiagnosticToolQuestion6Type returnValue;

            if (!Enum.TryParse(question6Type, out returnValue))
            {
                returnValue = question6Type.ToLower() switch
                {
                    "no" => DiagnosticToolQuestion6Type.No,
                    "somethingelse" => DiagnosticToolQuestion6Type.SomethingElse,
                    "yes" => DiagnosticToolQuestion6Type.Yes,
                    _ => DiagnosticToolQuestion6Type.Undefined
                };
            }

            return returnValue;
        }

        async Task<IServiceResponse> IEmailService.SendResultsRemail(Guid requestID, string emailAddress, IEmailDto dto)
        {
            var isSuccessful = false;
            string message = default;
            
            if (dto != null)
            {
                // Validate email
                if (_thisInterface.IsValidEmailAddress(requestID, emailAddress).IsValid)
                {
                    // Check that email is not unsubscribed
                    var isUnsubscribedResult = await _thisInterface.IsUnsubscribed(requestID, emailAddress);
                    if (isUnsubscribedResult.IsValid && isUnsubscribedResult.Payload == false)
                    {
                        string templateId;
                        var personalisation = Map(dto, emailAddress, out templateId);
                        if (personalisation != null)
                        {
                            try
                            {
                                await _notifyIntegrationService.SendDiagnosticToolResult(emailAddress, templateId, personalisation);
                                isSuccessful = true;
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, "Unable to send result email");
                                message = "An error occurred sending the result email";
                            }
                        }
                        else
                        {
                            message = $"Failed to map the data";
                        }
                    }
                    else
                        message = $"The email address \"{emailAddress}\" is unsubscribed";
                }
                else
                    message = $"The email address \"{emailAddress}\" is invalid";
            }
            else
                throw new ArgumentNullException(nameof(IEmailDto));

            return new ServiceResponse(requestID, isSuccessful, message);
        }

        private Dictionary<string, dynamic> Map(IEmailDto dto, string emailAddress, out string templateId)
        {
            templateId = string.Empty;
            Dictionary<string, dynamic> personalisation = null;
            if (dto is DiagnosticToolResultsEmailDataDto)
            {
                var emailData = _mapper.Map<DiagnosticToolResultsEmailData>(dto);
                if (emailData.Question6Answer == DiagnosticToolQuestion6Type.Undefined)
                    emailData.Question6Answer = ConvertQuestion6Type(((DiagnosticToolResultsEmailDataDto)dto).Question6Answer);

                templateId = GetTemplateID(emailData.Question6Answer);

                if (emailData.Question6Answer != DiagnosticToolQuestion6Type.Undefined)
                {
                    personalisation = GeneratePersonalisation(EmailPersonalisationNames.DiagnosticTool, new[] { emailData.BusinessSector, emailData.TradeMethod, emailData.SoftwareNeed, emailData.SoftwareChoices,
                                    emailData.StreamlineTasks, emailData.RecommendedSoftware, ConvertRelatedArticle(emailData.RelatedArticles), GetUnsubscribeLink(emailAddress) }, false);
                    return personalisation;
                }
            }
            else if (dto is SkillsResultsEmailDataDto)
            {
                var emailData = (SkillsResultsEmailDataDto)dto;
                
                templateId = _notifyServiceOption.Templates.SkillsModule1;
                var linkToSkilledModule2 = _notifyServiceOption.SkilledModule2Link;

                personalisation = GeneratePersonalisation(EmailPersonalisationNames.SkillsModule1, 
                                                new[] { emailData.DigitalAdoptionBenefits, emailData.DigitalAdoptionFrictionPointDescription, 
                                                    emailData.SoftwareUsage, emailData.InformationSharingMode, emailData.DigitalAdoptionBenefitsDescription, 
                                                    GetUnsubscribeLink(emailAddress), linkToSkilledModule2 }, 
                                                false);

                return personalisation;
            }
            else if (dto is SkilledModuleTwoDto)
            {
                var emailData = (SkilledModuleTwoDto)dto;

                templateId = GetTemplateID(emailData.SkilledModuleTwoResultType);
                var linkToSkilledModule2 = _notifyServiceOption.SkilledModule2Link;

                personalisation = GeneratePersonalisation(EmailPersonalisationNames.SkillsModule2,
                                                new[] { emailData.Priorities, GetUnsubscribeLink(emailAddress), linkToSkilledModule2 },
                                                false);

                return personalisation;
            }
            else if (dto is SkilledModuleThreeDto)
            {
                var emailData = (SkilledModuleThreeDto)dto;

                templateId = GetSkillsModuleThreeTemplateID(emailData.UserTypeActionPlanSection);
                
                personalisation = GeneratePersonalisation(EmailPersonalisationNames.SkillsModuleThree,
                                                new[] { emailData.QuestionOneStart, emailData.QuestionOneNext, emailData.QuestionOneFinally,
                                                        emailData.QuestionTwoStart, emailData.QuestionTwoNext, emailData.QuestionTwoFinally,
                                                        emailData.QuestionThreeStart, emailData.QuestionThreeNext, emailData.QuestionThreeFinally,
                                                        GetUnsubscribeLink(emailAddress) },
                                                false);

                return personalisation;
            }


            return personalisation;
        }

        private string GetSkillsModuleThreeTemplateID(string userTypeActionPlanSection)
        {
            switch (userTypeActionPlanSection)
            {
                // Mover
                case "mover-communication":
                    return _notifyServiceOption.Templates.SkillsModuleThree.MoverCommunication;
                case "mover-planning":
                    return _notifyServiceOption.Templates.SkillsModuleThree.MoverPlanning;
                case "mover-support":
                    return _notifyServiceOption.Templates.SkillsModuleThree.MoverSupport;
                case "mover-testing":
                    return _notifyServiceOption.Templates.SkillsModuleThree.MoverTesting;
                case "mover-training":
                    return _notifyServiceOption.Templates.SkillsModuleThree.MoverTraining;

                // Newcomer
                case "newcomer-communication":
                    return _notifyServiceOption.Templates.SkillsModuleThree.NewcomerCommunication;
                case "newcomer-planning":
                    return _notifyServiceOption.Templates.SkillsModuleThree.NewcomerPlanning;
                case "newcomer-support":
                    return _notifyServiceOption.Templates.SkillsModuleThree.NewcomerSupport;
                case "newcomer-testing":
                    return _notifyServiceOption.Templates.SkillsModuleThree.NewcomerTesting;
                case "newcomer-training":
                    return _notifyServiceOption.Templates.SkillsModuleThree.NewcomerTraining;

                //Performer

                case "performer-communication":
                    return _notifyServiceOption.Templates.SkillsModuleThree.PerformerCommunication;
                case "performer-planning":
                    return _notifyServiceOption.Templates.SkillsModuleThree.PerformerPlanning;
                case "performer-support":
                    return _notifyServiceOption.Templates.SkillsModuleThree.PerformerSupport;
                case "performer-testing":
                    return _notifyServiceOption.Templates.SkillsModuleThree.PerformerTesting;
                case "performer-training":
                    return _notifyServiceOption.Templates.SkillsModuleThree.PerformerTraining;

                default:
                    throw new ArgumentException($"The type '{userTypeActionPlanSection}' is invalid", nameof(userTypeActionPlanSection));
            }
        }
        private string GetTemplateID(SkilledModuleTwoResultType skilledModuleTwoResultType)
        {
            switch (skilledModuleTwoResultType)
            {
                case SkilledModuleTwoResultType.DigitalNewComer:
                    return _notifyServiceOption.Templates.SkillsModule2DigitalNewcomer;
                case SkilledModuleTwoResultType.DigitalMover:
                    return _notifyServiceOption.Templates.SkillsModule2DigitalMover;
                case SkilledModuleTwoResultType.DigitalPerfomer:
                    return _notifyServiceOption.Templates.SkillsModule2DigitalPerformer;
                default:
                    throw new ArgumentException($"The type '{skilledModuleTwoResultType}' is invalid", nameof(skilledModuleTwoResultType));
            }
        }

        private string GetTemplateID(DiagnosticToolQuestion6Type question6Answer)
        {
            return question6Answer switch
            {
                DiagnosticToolQuestion6Type.No => _notifyServiceOption.Templates.DtResultPageQ6No,
                DiagnosticToolQuestion6Type.SomethingElse => _notifyServiceOption.Templates.DtResultPageQ6Else,
                DiagnosticToolQuestion6Type.Yes => _notifyServiceOption.Templates.DtResultPageQ6Yes,
                _ => throw new ArgumentException($"The response to the Question 6 Answer ('{question6Answer}') was not recognised", nameof(question6Answer))
            };
        }

        private string GetUnsubscribeLink(string emailAddress)
        {
            return $"{_notifyServiceOption.UnsubscribeLink}{ Uri.EscapeDataString(emailAddress)}";
        }

        private Dictionary<string, dynamic> GeneratePersonalisation(string[] names, string[] values, bool skipNullOrEmptyValues = true)
        {
            Dictionary<string, dynamic> returnValue = new();

            if (names?.Length > 0)
            {
                if (values?.Length > 0)
                {
                    if (names.Length == values.Length)
                    {
                        for (int i = 0; i < values.Length; i++)
                            if (!skipNullOrEmptyValues || !string.IsNullOrWhiteSpace(values[i]))
                                returnValue.Add(names[i], values[i]);
                    }
                    else
                    {
                        _logger.LogWarning($"Inconsistent length of names and values supplied to personalisation - {names.Length} vs {values.Length}");
                        throw new ArgumentException("The personalisation names and values must be equal length", nameof(names));
                    }
                }
                else
                    throw new ArgumentNullException(nameof(values));
            }
            else
                throw new ArgumentNullException(nameof(names));

            return returnValue;
        }

        private static string ConvertRelatedArticle(DiagnosticToolResultsEmailRelatedArticle relatedArticle)
        {
            return $"* {relatedArticle.Title} - {relatedArticle.Description} - {relatedArticle.ArticleURL}\r\n";
        }
        private static string ConvertRelatedArticle(DiagnosticToolResultsEmailRelatedArticle[] relatedArticles)
        {
            StringBuilder returnValue = new();

            foreach (var relatedArticle in relatedArticles)
                returnValue.AppendLine(ConvertRelatedArticle(relatedArticle));

            return returnValue.ToString();
        }

        async Task<IServiceResponse> IEmailService.UnsubscribeEmail(Guid requestID, string emailAddress)
        {
            var isSuccessful = false;
            string message = default;

            var results = await _emailDataService.GetByEmail(emailAddress);
            if (results.Length > 0)
            {
                // Get subscribed email addresses
                var emails = results.Where(x => !x.IsUnsubscribed.HasValue || !x.IsUnsubscribed.Value);
                if (emails.Any())
                {
                    foreach (var email in emails)
                    {
                        email.IsUnsubscribed = true;
                        email.UnsubscribeTimestamp = DateTime.Now;

                        try
                        {
                            await _emailDataService.Update(email);
                            isSuccessful = true;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Unable to update email in database");
                            message = "Unable to updated the email address";
                        }
                    }
                }
                else
                    message = "This email address has already unsubscribed";
            }
            else
                message = "The specified email address does not exist";

            return new ServiceResponse(requestID, isSuccessful, message);
        }
    }
}