namespace Beis.LearningPlatform.Web.Utils
{
    public static class FormSearchTagsExtensions
    {
        public static async Task<string> DisplayName(this FormSearchTags formSearchTags, string tagName)
        {
            string Baseurl = StrapiApiUrl;


            List<CMSSearchTag> tagsCollection = new List<CMSSearchTag>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                //request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Call to api
                HttpResponseMessage Res = await client.GetAsync("tags");

                //check response
                if (Res.IsSuccessStatusCode)
                {
                    //deserialize json to .net class
                    var digitalPageResponse = Res.Content.ReadAsStringAsync().Result;
                    tagsCollection = JsonConvert.DeserializeObject<List<CMSSearchTag>>(digitalPageResponse);
                }
            }

            var retDisplayName = tagsCollection.Where(tag => tag.name.Equals(tagName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (retDisplayName != null)
            {
                return retDisplayName.displayName;
            }
            else
            {
                // TP-TODO: As part of refactoring out of this extension method, log warnings for unmatched crm tags.
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the Strapi API Url.
        /// </summary>
        /// <remarks>This is a hack to compensate for the fact that the configuration has not been implemented correctly.  It should be corrected in the future.</remarks>
        public static string StrapiApiUrl { get; set; }
    }
}