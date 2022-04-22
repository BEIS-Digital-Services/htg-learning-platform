using AutoMapper;
using Beis.Htg.VendorSme.Database.Models;
using Beis.LearningPlatform.Web.ComparisonTool.Models;
using System.Collections.Generic;

namespace Beis.LearningPlatform.Web.ComparisonTool.DependencyInjection
{
    /// <summary>
    /// A class that defines a profile for a Diagnostic Tool Email Answer.
    /// </summary>
    public class ComparisonToolProductProfile : Profile
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public ComparisonToolProductProfile()
        {
            CreateMap<product, ComparisonToolProduct>();
            CreateMap<ComparisonToolProduct, product>();
            CreateMap<List<product>, List<ComparisonToolProduct>>();
            CreateMap<List<ComparisonToolProduct>, List<product>>();
        }
    }
}