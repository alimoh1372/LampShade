using System.Reflection.Metadata.Ecma335;

namespace _0_Framework.Domain
{
    public class SeoProperties:EntityBase
    {
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string CanonicalAddress { get; private set; }

        public SeoProperties(string keywords, string metaDescription, string canonicalAddress)
        {
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
        }
        public  void Edit(string keywords, string metaDescription, string canonicalAddress)
        {
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
        }
    }
}