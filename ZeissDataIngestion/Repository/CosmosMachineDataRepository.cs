using Microsoft.Azure.Cosmos;
using ZeissDataIngestion.Model;

namespace ZeissDataIngestion.Repository
{
    public class CosmosDbMachineDataRepository : IMachineDataRepository
    {
        private readonly CosmosClient _client;
        private readonly string _databaseId = "ToDoList";
        private readonly string EndpointUri = "https://machinestatus.documents.azure.com:443/";
        private readonly string PrimaryKey = "nzoOBY5nNWCivNJfTZtM62gc5tlGgelWGcOrt14AU8X2OHg9ceLEcBcUL3BxoIGdqFYjtBH5Xh0YACDbuVsqQg==";
        private readonly string _containerId = "Items";

        public CosmosDbMachineDataRepository(string connectionString)
        {
            _client = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<MachineData>> GetMachineData()
        {
            var container = _client.GetContainer(_databaseId, _containerId);

            var query = container.GetItemQueryIterator<MachineData>();

            var results = new List<MachineData>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task AddDataToCosmosAsync(dynamic data)
        {
            var container = _client.GetContainer(_databaseId, _containerId);

            try
            {
                await container.CreateItemAsync(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding data to Cosmos DB: {ex.Message}");
            }
        }



    }

}
