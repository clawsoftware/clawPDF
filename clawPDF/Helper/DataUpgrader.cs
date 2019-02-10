using System;
using pdfforge.DataStorage;

namespace clawSoft.clawPDF.Helper
{
    internal class DataUpgrader
    {
        public Data Data { get; set; }

        public void MoveValue(string oldPath, string newPath)
        {
            var v = Data.GetValue(oldPath);
            Data.SetValue(newPath, v);
            Data.RemoveValue(oldPath);
        }

        public void MoveValue(string name, string oldSection, string newSection)
        {
            MoveValue(oldSection + name, newSection + name);
        }

        public void MapValue(string path, Func<string, string> mapFunction)
        {
            var v = Data.GetValue(path);
            Data.SetValue(path, mapFunction(v));
        }

        public void MoveSection(string path, string newPath)
        {
            foreach (var value in Data.GetValues(path)) MoveValue(path + value.Key, newPath + value.Key);

            Data.RemoveSection(path.TrimEnd('\\'));
        }
    }
}