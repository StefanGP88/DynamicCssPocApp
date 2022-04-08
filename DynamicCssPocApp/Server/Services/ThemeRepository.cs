using DynamicCssPocApp.Shared;
using HashidsNet;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace DynamicCssPocApp.Server.Services
{
    public class ThemeRepository
    {
        private readonly List<ThemeData> Data = new() { new ThemeData() { Color = ColorTranslator.ToHtml(Color.Blue), Height = 50, Width = 100 } };
        private readonly Hashids IdConverter = new(salt:"proof of concept", minHashLength: 15);

        public async Task<ThemeData> Get([NotNull] string? stringId)
        {
            if (stringId == null) throw new ArgumentNullException(nameof(stringId));

            //await Task.Delay(15 * 1000);
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

            await Task.Delay(5000);
            Data.Add(themeData!);
        }

        public static void MapEndpoints()
        {

        }
    }
}
