using System;
using Common.Extensions;

namespace Common.Dto
{
    public class GoogleId : IGoogleId
    {
        public GoogleId(string resourceName, string eTag)
        {
            if (resourceName.IsNullOrEmpty() || eTag.IsNullOrEmpty()) throw new ArgumentException("Empty Google identification can't be created!");
            ResourceName = resourceName;
            ETag = eTag;
        }

        public GoogleId(IGoogleId id) : this(id.ResourceName, id.ETag)
        {
        }

        public string ResourceName { get; set; }
        public string ETag { get; set; }
    }
}