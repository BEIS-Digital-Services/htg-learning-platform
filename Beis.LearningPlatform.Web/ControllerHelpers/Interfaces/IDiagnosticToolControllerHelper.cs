namespace Beis.LearningPlatform.Web.ControllerHelpers.Interfaces
{
    /// <summary>
    /// An interface that defines the behaviour of a helper for the Diagnostic Tool controller.
    /// </summary>
    public interface IDiagnosticToolControllerHelper : IControllerHelperBase
    {
        
        /// <summary>
        /// Creates a new Diagnostic Tool form.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.  A ControllerHelperOperationResponse with payload DiagnosticToolForm that was created.</returns>
        Task<ControllerHelperOperationResponse<DiagnosticToolForm>> CreateForm(FormTypes formType);

        /// <summary>
        /// Goes to the specified step of the specified Diagnostic Tool form.
        /// </summary>
        /// <param name="form">A DiagnosticToolForm that is the form data.</param>
        /// <param name="step">An int indicating the step to go to.</param>
        /// <returns>A Task representing the asynchronous operation.  A ControllerHelperOperationResponse with payload DiagnosticToolForm that was processed for the specified step.</returns>
        Task<ControllerHelperOperationResponse<DiagnosticToolForm>> GotoStep(DiagnosticToolForm form, int step);

        /// <summary>
        /// Processes the next step of the specified Diagnostic Tool form.
        /// </summary>
        /// <param name="form">A DiagnosticToolForm that is the form data.</param>
        /// <param name="isValid">A bool indicating whether the step is valid.</param>
        /// <param name="errorMessages">An array of string containing any error messages associated with the step.</param>
        /// <returns>A Task representing the asynchronous operation.  A ControllerHelperOperationResponse with payload DiagnosticToolForm that was processed for the next step.</returns>
        Task<ControllerHelperOperationResponse<DiagnosticToolForm>> NextStep(DiagnosticToolForm form, bool isValid, string[] errorMessages);

        /// <summary>
        /// Processes the previous step of the specified Diagnostic Tool form.
        /// </summary>
        /// <param name="form">A DiagnosticToolForm that is the form data.</param>
        /// <returns>A Task representing the asynchronous operation.  A ControllerHelperOperationResponse with payload DiagnosticToolForm that was processed for the previous step.</returns>
        Task<ControllerHelperOperationResponse<DiagnosticToolForm>> PreviousStep(DiagnosticToolForm form);

        /// <summary>
        /// Process an email answer.
        /// </summary>
        /// <param name="form">A DiagnosticToolForm that is the form data.</param>
        /// <returns>A Task representing the asynchronous operation.  A ControllerHelperOperationResponse with payload EmailAnswer containing the result.</returns>
        ControllerHelperOperationResponse<EmailAnswer> ProcessEmailAnswer(DiagnosticToolForm form);

        /// <summary>
        /// Processes the results for the specified Diagnostic Tool form.
        /// </summary>
        /// <param name="form">A DiagnosticTooLForm that is the form model to get results for.</param>
        /// <returns>A Task representing the asynchronous operation.  A ControllerHelperOperationResponse with payload bool containing the result.</returns>
        Task<ControllerHelperOperationResponse<bool>> ProcessResults(DiagnosticToolForm form, FormTypes formType);


        /// <summary>
        /// Sets the navigation and footer elements of the Diagnostic Tool form.
        /// </summary>
        /// <param name="form">A DiagnosticToolForm that is the form model to use.</param>
        /// <returns>A Task representing the asynchronous operation.  A ControllerHelperOperationResponse containing the result</returns>
        Task<ControllerHelperOperationResponse> SetNavAndFooter(DiagnosticToolForm form);

        /// <summary>
        /// Starts an existing Diagnostic Tool form.
        /// </summary>
        /// <param name="form">A DiagnosticTooLForm that is the form model to start.</param>
        /// <returns>A Task representing the asynchronous operation.  A ControllerHelperOperationResponse containing the result.</returns>
        Task<ControllerHelperOperationResponse> Start(DiagnosticToolForm form);

        Task UpdateScore(DiagnosticToolForm form);

        string GetUniqueContentKey(DiagnosticToolForm form);
        public void LoadSkillsThreeResponseByUniqueId(string uniqueId, DiagnosticToolForm form);
    }
}