using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using ZenkoAPI.Dtos;
using ZenkoAPI.Models;
using ZenkoAPI.Repositories;

namespace ZenkoAPI.Services
{
    public class FileUploadService(ITransactionRepository transactionRepository) : IFileUploadService
    {
        public async Task AddTransactionToDatabase(Stream fileStream, Guid userId)
        {
            var validator = new TransactionDTOValidator();
            var batchSize = 250;
            List<Transaction> transactionsBatch = [];

            var errors = new List<string>();
            using (var reader = new StreamReader(fileStream))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                while (csvReader.Read())
                {
                    try
                    {
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
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        continue;
                    }
                }
                if(transactionsBatch.Count > 0)
                {
                    await transactionRepository.AddTransactionsToDatabaseAsync(transactionsBatch);
                    transactionsBatch.Clear();
                }
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

            await transactionRepository.AddFileMetadataToDatabase(fileMetadata);
        }

        public async Task DeleteTransactionAsync(Guid userId)
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
