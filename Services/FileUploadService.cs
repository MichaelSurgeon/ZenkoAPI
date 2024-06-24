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

                            await transactionRepository.AddTransactionToDatabaseAsync(transformedTransaction);
                        }
                        else
                        {
                            foreach (var error in validationResult.Errors)
                            {
                                errors.Add(error.ErrorMessage + " on row number: " + csvReader.Parser.Row);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        continue;
                    }
                }
            }

            foreach (var error in errors)
            {
                Console.WriteLine(error);
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

        public async Task DeleteTransactionAndFileInformationAsync(Guid userId)
        {
            await transactionRepository.DeleteFileInformationByUserIdAsync(userId);
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
