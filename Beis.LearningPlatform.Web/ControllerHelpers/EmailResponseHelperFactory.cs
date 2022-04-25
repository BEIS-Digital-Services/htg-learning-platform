using Beis.LearningPlatform.Web.ControllerHelpers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Beis.LearningPlatform.Web.ControllerHelpers
{
    public class EmailResponseHelperFactory : IEmailResponseHelperFactory
    {
        private readonly IEnumerable<IEmailResponseHelper> _emailResponseHelpers;

        public EmailResponseHelperFactory(IEnumerable<IEmailResponseHelper> emailResponseHelpers)
        {
            _emailResponseHelpers = emailResponseHelpers;
        }
        public IEmailResponseHelper Get(FormTypes formType)
        {
            return _emailResponseHelpers.Single(x => x.FormType == formType);
        }
    }
}