namespace ZenkoAPI.Services
{
    public class CalculationService
    {
        public async Task CreateAggregatedDataAsync(Guid userId)
        {
            // get id's from data base for transactions (this can be done by yielding so that we only get one at a time possibiliy)
            // create a loop to loop through each id and get the transaction row
            // populate aggregated info
            // store in database
            // repeat
        }
    }
}
