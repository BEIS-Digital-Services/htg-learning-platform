﻿using Beis.LearningPlatform.Web.StrapiApi.Models;
using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.Models
{
    public class ArticleSearchTagViewModel
    {
        public IList<CMSSearchTag> Tags { get; internal set; }
        public string YourTags { get; internal set; }
    }
}