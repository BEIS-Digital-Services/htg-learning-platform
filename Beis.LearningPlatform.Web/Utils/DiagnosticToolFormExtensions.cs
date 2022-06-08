namespace Beis.LearningPlatform.Web.Utils
{
    /// <summary>
    /// A class that defines extensions to a Diagnostic Tool Form.
    /// </summary>
    public static class DiagnosticToolFormExtensions
    {
        /// <summary>
        /// Gets the tags from selected form answers.
        /// </summary>
        /// <param name="diagnosticToolForm">A DiagnosticToolForm that is the form.</param>
        /// <param name="sortTags">A bool indicating whether to sort the tags.</param>
        /// <param name="returnUniqueTagsOnly">A bool indicating whether to return unique tags only.</param>
        /// <returns>An array of FormSearchTags containing the result.</returns>
        public static FormSearchTags[] GetTagsFromSelectedAnswers(this DiagnosticToolForm diagnosticToolForm, bool sortTags = true, bool returnUniqueTagsOnly = false)
        {
            FormSearchTags[] returnValue;

            // Get the search tags for any selected answers
            var searchTags = diagnosticToolForm.steps.SelectMany(s => s.elements)
                                                      .SelectMany(e => e.answerOptions)
                                                      .Where(ao => ao.IsSelected() && ao.searchTags != null && ao.searchTags.Count > 0)
                                                      .SelectMany(ao => ao.searchTags);

            if (returnUniqueTagsOnly)
                searchTags = searchTags.Distinct();

            if (sortTags)
            {
                var searchTagList = searchTags.ToList();
                searchTagList.Sort();
                returnValue = searchTagList.ToArray();
            }
            else
                returnValue = searchTags.ToArray();

            return returnValue;
        }

        /// <summary>
        /// Loads the answers into the Diagnostic Tool Form.
        /// </summary>
        /// <param name="diagnosticToolForm">A DiagnosticToolForm that is the form.</param>
        /// <param name="answers">An array of FormStepAnswer containing the answer data.</param>
        public static void Load(this DiagnosticToolForm diagnosticToolForm, FormStepAnswer[] answers)
        {
            if (answers != null)
            {
                if (answers.Length > 0)
                {
                    foreach (var answer in answers)
                    {
                        var element = diagnosticToolForm.steps.Where(s => s.id == answer.ID).FirstOrDefault();
                        if (element != default)
                            element.Load(answer);
                        else
                            throw new ArgumentException("The specified answer data contains a missing form step element", nameof(answers));
                    }
                }
            }
            else
                throw new ArgumentNullException(nameof(answers));
        }

        /// <summary>
        /// Saves the answers to a Diagnostic Tool Form.
        /// </summary>
        /// <param name="diagnosticToolForm">A DiagnosticToolForm that is the form.</param>
        /// <returns>An array of FormStepAnswer containing the answer data.</returns>
        public static FormStepAnswer[] Save(this DiagnosticToolForm diagnosticToolForm)
        {
            return diagnosticToolForm.steps.Select(s => s.Save()).ToArray();
        }
    }
}