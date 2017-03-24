using System;
using System.Collections.Generic;
using System.Text;

namespace RegistryChecker
{
    public abstract class FeatureChecker
    {
        public FeatureChecker()
        {
            Versions = new List<string>();
        }
        
        public virtual string Name { get; protected set; }
        public virtual string Url { get; protected set; }
        public virtual List<string> Versions { get; protected set; }
        public virtual bool HasCorrectVersion { get; protected set; }
        public abstract bool Check();
        
        protected virtual string Message { get; set; }

        protected virtual string GetDetailedMessageInformation()
        {
            if (HasCorrectVersion)
            {
                return Message;
            }
            else
            {
                var sb = new StringBuilder();
                sb.AppendLine($"{Message} Please install any of these version(s):");
                foreach (var version in Versions)
                {
                    sb.AppendLine($" -> {version}");
                }
                sb.AppendLine($"Get Python here: {Url}");
                return sb.ToString();
            }
        }

        public override string ToString()
        {
            return GetDetailedMessageInformation();
        }
    }
}
