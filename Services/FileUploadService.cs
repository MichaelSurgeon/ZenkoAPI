using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using ZenkoAPI.Dtos;
using ZenkoAPI.Models;
using ZenkoAPI.Repositories;

namespace ZenkoAPI.Services
{
    public class FileUploadService(ITransactionRepository transactionRepository, IFileRepository fileRepository) : IFileUploadService
    {
        public async Task ParseAndAddTransactionToDatabase(Stream fileStream, Guid userId)
        {
            // could probably speed up inserts for mass amount of data using parallel batching it would be quicker
            var validator = new TransactionDTOValidator();
            var batchSize = 250;
            List<Transaction> transactionsBatch = [];

            int rowNumber;
            using var reader = new StreamReader(fileStream);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            while (await csvReader.ReadAsync())
            {
                try
                {
                    rowNumber = csvReader.Parser.Row;
                    var record = csvReader.GetRecord<TransactionDto>();
                    var validationResult = await validator.ValidateAsync(record);
                    if (validationResult.IsValid)
                    {
                        var transformedTransaction = new Transaction()
                        {
                            TransactionId = new Guid(),
                            TransactionName = record.Name,
                            TransactionAmount = decimal.Parse(record.Amount),
                            TransactionDate = DateTime.Parse(record.Date).ToUniversalTime(),
                            TransactionLocation = record.Location,
                            CategoryName = record.Category,
                            UserId = userId
                        };

                        transactionsBatch.Add(transformedTransaction);
                        if (transactionsBatch.Count >= batchSize)
                        {
                            await transactionRepository.AddTransactionsToDatabaseAsync(transactionsBatch);
                            transactionsBatch.Clear();
                        }
                    }
                    //temp error logging
                    foreach (var error in validationResult.Errors)
                    {
                        Console.WriteLine(error + " on row number: " + rowNumber);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    continue;
                }
            }
            if (transactionsBatch.Count > 0)
            {
                await transactionRepository.AddTransactionsToDatabaseAsync(transactionsBatch);
                transactionsBatch.Clear();
            }
        }

        public async Task AddFileMetaDataToDatabaseAsync(IFormFile file, Guid userId)
        {
            var fileMetadata = new FileData()
            {
                FileId = new Guid(),
                FileName = file.FileName,
                FileSize = file.Length,
                UploadTime = DateTime.UtcNow,
                UserId = userId
            };

            await fileRepository.AddFileMetadataToDatabase(fileMetadata);
        }

        public async Task DeleteTransactionsByIdAsync(Guid userId)
        {
            await transactionRepository.DeleteTransactionsByUserIdAsync(userId);
        }

        private sealed class TransactionDtoMap : ClassMap<TransactionDto>
        {
            public TransactionDtoMap()
            {
                Map(m => m.Name);
                Map(m => m.Amount);
                Map(m => m.Location);
                Map(m => m.Date);
                Map(m => m.Category);
            }
        }
    }
}
