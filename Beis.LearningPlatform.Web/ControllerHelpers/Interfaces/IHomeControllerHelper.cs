﻿namespace Beis.LearningPlatform.Web.ControllerHelpers.Interfaces
{
    public interface IHomeControllerHelper : ICmsControllerHelperBase
    {
        Task SetReactiveTagComponents(CMSPageViewModel viewModel);
        IList<string> GetCurrentTags(string yourTags);
        IList<string> GetCurrentTags(string yourTags, string tag, bool removeTag);
    }
}