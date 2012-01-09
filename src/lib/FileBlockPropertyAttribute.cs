using System;

namespace TechBoard
{
    [BlockPropertyEditor(typeof(FileBlockPropertyEditor))]
    public class FileBlockPropertyAttribute : BlockPropertyAttribute
    {
        public string Filter { get; private set; }

        public FileBlockPropertyAttribute(string xiFilter)
        {
            Filter = xiFilter;
        }

        public override object FromFile(string xiFileName,
                                        string xiValue)
        {
            Uri fileUri = new Uri(xiFileName);
            Uri propUri = new Uri(fileUri, xiValue as string);

            return propUri.ToString();
        }

        public override string ToFile(string xiFileName,
                                      object xiValue)
        {
            Uri fileUri = new Uri(xiFileName);
            Uri propUri = new Uri(xiValue as string);
            Uri relUri = fileUri.MakeRelativeUri(propUri);

            return relUri.ToString();
        }
    }
}
