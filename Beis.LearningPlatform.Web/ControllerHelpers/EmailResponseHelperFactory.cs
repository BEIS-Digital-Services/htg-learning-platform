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
            var helper = _emailResponseHelpers.SingleOrDefault(x => x.FormType == formType);
            if(helper == null)
            {
                helper = _emailResponseHelpers.SingleOrDefault(x => x.FormType.HasFlag(formType));
            }

            return helper;
        }
    }
}