using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    public class DBLoop
    {
        private readonly ITranslateMemory _translateMemory;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITranslatorService _translatorService;
        private readonly IDbRepository _dbRepository;

        public DBLoop(
            ITranslateMemory translateMemory,
            IServiceProvider serviceProvider,
            ITranslatorService translatorService)
        {
            this._translateMemory = translateMemory;
            this._serviceProvider = serviceProvider;
            this._translatorService = translatorService;
        }

        public void Run()
        {
            try
            {
                using (IDbRepository dbRepository = _serviceProvider.GetService<IDbRepository>())
                using (IDynamicRepository dynamicRepository = _serviceProvider.GetService<IDynamicRepository>())
                using (var writeRepository = _serviceProvider.GetService<IWriteRepository>())
                {
                    foreach (string tableName in dbRepository.GetTables())
                    {
                        Console.WriteLine($"Translate table {tableName}");

                        var columns = dbRepository.GetCharColumns(tableName);
                        var identityColumn = dbRepository.GetIdentity(tableName);
                        if (identityColumn == null)
                        {
                            Console.WriteLine($"Identity column for {tableName} not found");
                            continue;
                        }
                        columns.Add(identityColumn);
                        var result = dynamicRepository.GetDataRecord(columns.ToArray(), tableName).ToList();
                        foreach (System.Data.IDataRecord row in result)
                        {
                            foreach (var column in columns)
                            {
                                string stringValue = row[column] as string;
                                if (!string.IsNullOrEmpty(stringValue)
                                    && HebrewUtils.IsHebrewString(stringValue))
                                {
                                    string translate;
                                    if (_translateMemory.Contain(stringValue))
                                    {
                                        translate = _translateMemory.GetTm(stringValue);
                                        Console.WriteLine($"Used translate memory for {stringValue} - {translate}");
                                    }
                                    else
                                    {
                                        translate = _translatorService.Translate(stringValue, false);
                                        if (HebrewUtils.IsHebrewString(translate))
                                        {
                                            Console.WriteLine($"Can not translate {stringValue}");
                                            continue;
                                        }
                                        _translateMemory.Add(stringValue, translate);
                                    }

                                    int updated = writeRepository.Update(tableName, identityColumn, column, translate, row[identityColumn]);

                                    Console.WriteLine($"Update {updated}. Translate from {stringValue} to {translate} in {row[identityColumn]}");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                //throw;
            }
        }
    }
}
