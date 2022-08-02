using System.Text.Encodings.Web;

namespace Beis.LearningPlatform.DAL.Mappers;

public static class ExpressionOfInterestMapper
{
    public static ExpressionOfInterest GetExpressionOfInterest(ExpressionOfInterestDto expressionOfInterestDto)
    {
        return new ExpressionOfInterest
        {
            PageName = HtmlEncoder.Default.Encode(expressionOfInterestDto.PageName),
            UserName = HtmlEncoder.Default.Encode(expressionOfInterestDto.UserName),
            UserEmail = HtmlEncoder.Default.Encode(expressionOfInterestDto.UserEmail),
            UserBusinessName = HtmlEncoder.Default.Encode(expressionOfInterestDto.UserBusinessName),
            UserPhone = HtmlEncoder.Default.Encode(expressionOfInterestDto.UserPhone),
            OptInReadPrivacy = expressionOfInterestDto.OptInReadPrivacy,
            OptInMarketingEmail = expressionOfInterestDto.OptInMarketingEmail,
            OptInMarketingPhone = expressionOfInterestDto.OptInMarketingPhone
        };

    }
}
