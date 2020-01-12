using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore.Internal;

namespace BoardGames.TagHelpers
{
    [HtmlTargetElement("starrating", Attributes = AttributeRatingName)]
    [HtmlTargetElement("starrating", Attributes = AttributeRatingValue)]
    public class StarRatingTagHelper : TagHelper
    {
        private const string AttributeRatingName = "rating-name";
        private const string AttributeRatingValue = "rating-value";

        [HtmlAttributeName(AttributeRatingValue)]
        public int? RatingValue { get; set; }
        
        [HtmlAttributeName(AttributeRatingName)]
        public string RatingName { get; set; } = "";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            List<string> stars = new List<string>();
            for (var i = 1; i <= 6; i++)
            {
                var stringValue = ParseIntToWord(i);
                
                if (i <= RatingValue)
                {
                    stars.Add($@"<span class='dice-icon dice-icon--active fas fa-dice-{stringValue}'></span>");
                }
                else
                {
                    stars.Add($@"<span class='dice-icon dice-icon--inactive fas fa-dice-{stringValue}'></span>");

                }
            }

            var preparedIcons = stars.Join("");
            string content =
                $@"<div class='d-flex'>
                    <p class='font-weight-bold rating-name'>{ RatingName }:</p>
                    <div class='ml-2'>
                       { preparedIcons }
                    </div>
                </div>
                ";

            output.Content.AppendHtml(content);
        }
        
        private static string ParseIntToWord(int numberValue)
        {
            return numberValue switch
            {
                1 => "one",
                2 => "two",
                3 => "three",
                4 => "four",
                5 => "five",
                6 => "six",
                _ => ""
            };
        }
    }
}