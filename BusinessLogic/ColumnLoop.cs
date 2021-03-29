using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class ColumnLoop
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITranslatorService _translatorService;

        public ColumnLoop(
            IServiceProvider serviceProvider,
            ITranslatorService translatorService)
        {
            this._serviceProvider = serviceProvider;
            this._translatorService = translatorService;
        }

        public void Run()
        {
            //List<TranslateInfoItem> data = new List<TranslateInfoItem>();
            //data.Add(new TranslateInfoItem { Value = "משקיע ACS" });
            //data.Add(new TranslateInfoItem { Value = "ר.ר.ם 191 בע\"מ" });
            //TranslationHandler.Translate(data, TRANSLATE_PROVIDER.GOOGLE, "iw", "ru", optimizeSpecialSequencesInTranslation);

            //TranslationHandler.Translate("משקיע ACS", TRANSLATE_PROVIDER.GOOGLE, "iw", "ru");

            
            try
            {
                using (IRepository repository = _serviceProvider.GetService<IRepository>())
                using (var writeRepository = _serviceProvider.GetService<IWriteRepository>())
                {
                    IEnumerable<Row> rows = repository.GetRow();
                    foreach (Row row in rows)
                    {
                        if (HebrewUtils.IsOnlyHebrewString(row.Name))
                        {
                            string translate = _translatorService.Translate(row.Name, false);
                            if (HebrewUtils.IsHebrewString(translate))
                            {
                                Console.WriteLine($"Can not translate {row.Name}");
                                continue;
                            }

                            int updated = writeRepository.Update(row.Id, translate);
                            Console.WriteLine($"Update {updated}. Translate from {row.Name} to {translate} in {row.Id}");
                        }
                    }
                }    
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
