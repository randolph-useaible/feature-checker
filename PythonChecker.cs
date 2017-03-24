using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace RegistryChecker
{
    public class PythonChecker : FeatureChecker
    {
        const string REGISTRY_BASE = @"SOFTWARE\Python\PythonCore";

        public PythonChecker()
        {
            Name = "Python";
            Url = $"https://www.python.org/downloads";

            Versions.Add("3.5.1");
        }

        public override bool Check()
        {
            HasCorrectVersion = false;
            Message = $"{Name} not installed on your machine.";

            using (var pythonCore = Registry.CurrentUser.OpenSubKey(REGISTRY_BASE))
            {
                if (pythonCore != null)
                {
                    var subKeyNames = pythonCore.GetSubKeyNames();
                    foreach(var subKeyName in subKeyNames)
                    {
                        using (var subKey = pythonCore.OpenSubKey(subKeyName))
                        {
                            if (subKey != null)
                            {
                                using (var installedFeaturesKey = subKey.OpenSubKey("InstalledFeatures"))
                                {
                                    var value = installedFeaturesKey.GetValue("exe");
                                    if (value != null)
                                    {
                                        var version = value.ToString().Substring(0, 5);
                                        foreach(var reqVersion in Versions)
                                        {
                                            if (reqVersion == version)
                                            {
                                                HasCorrectVersion = true;
                                                Message = $"{Name}... OK";
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (HasCorrectVersion) break;
                    }

                    if (!HasCorrectVersion)
                    {
                        Message = $"{Name} installed on your machine is incompatible.";
                    }
                }
            }

            return HasCorrectVersion;
        }
    }
}