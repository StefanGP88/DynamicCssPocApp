using DynamicCssPocApp.Shared;
using HashidsNet;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace DynamicCssPocApp.Server.Services
{
    public class ThemeRepository
    {
        private readonly List<ThemeData> Data = new() { new ThemeData() { Color = ColorTranslator.ToHtml(Color.Blue), Height = "50px", Width = "100px" } };
        private readonly Hashids IdConverter = new(salt:"proof of concept", minHashLength: 15);

        public async Task<ThemeData> Get([NotNull] string? stringId)
        {
            if (stringId == null) throw new ArgumentNullException(nameof(stringId));

            var intId = IdConverter.Decode(stringId);
            return Data[intId[0]];
        }
        public string NewestId()
        {
            return IdConverter.Encode(Data.Count - 1);
        }
        public async Task AddNew([NotNull] ThemeData? themeData)
        {
            if(themeData == null) throw new ArgumentNullException(nameof(themeData));

            Data.Add(themeData!);
        }

    }
}
