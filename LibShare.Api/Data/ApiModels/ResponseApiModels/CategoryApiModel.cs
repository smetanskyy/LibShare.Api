using System.Collections.Generic;

namespace LibShare.Api.Data.ApiModels.ResponseApiModels
{
    public class CategoryApiModel
    {
        /// <summary>
        /// Id of category
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of category
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Picture of category 
        /// </summary>
        public string Image { get; set; }

        public virtual ICollection<CategoryApiModel> Children { get; set; }
    }
}
