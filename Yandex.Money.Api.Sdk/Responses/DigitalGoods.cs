using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Yandex.Money.Api.Sdk.Responses
{
    [DataContract]
    public class Article
    {
        /// <summary>
        /// Product identifier in the seller's system
        /// </summary>
        [DataMember(Name = "merchantArticleId")]
        public string MerchantArticleId { get; set; }

        /// <summary>
        /// Serial number of the product
        /// </summary>
        [DataMember(Name = "serial")]
        public string Serial { get; set; }

        /// <summary>
        /// Secret for the digital product
        /// </summary>
        [DataMember(Name = "secret")]
        public string Secret { get; set; }
    }

    [DataContract]
    public class Bonus
    {
        /// <summary>
        /// Serial number of the product
        /// </summary>
        [DataMember(Name = "serial")]
        public string Serial { get; set; }

        /// <summary>
        /// Secret for the digital product
        /// </summary>
        [DataMember(Name = "secret")]
        public string Secret { get; set; }
    }

    /// <summary>
    /// Data about a digital product (PIN codes and bonuses for games, iTunes, XBox, etc)
    /// </summary>
    [DataContract]
    public class DigitalGoods
    {
        [DataMember(Name = "article")]
        public List<Article> Article { get; set; }

        [DataMember(Name = "bonus")]
        public List<Bonus> Bonus { get; set; }
    }
}