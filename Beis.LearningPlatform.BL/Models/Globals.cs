namespace Beis.LearningPlatform.BL.Models
{
    /// <summary>
    /// An enumeration that defines the type of answer to Question 6.
    /// </summary>
    public enum DiagnosticToolQuestion6Type : int
    {
        /// <summary>
        /// The answer is undefined.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// The answer is no.
        /// </summary>
        No,

        /// <summary>
        /// The answer is something else.
        /// </summary>
        SomethingElse,

        /// <summary>
        /// The answer is yes.
        /// </summary>
        Yes
    }
}