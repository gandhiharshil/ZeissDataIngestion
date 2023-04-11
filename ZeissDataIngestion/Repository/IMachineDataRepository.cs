using ZeissDataIngestion.Model;

namespace ZeissDataIngestion.Repository
{
    public interface IMachineDataRepository
    {
        Task<List<MachineData>> GetMachineData();
        Task AddDataToCosmosAsync(dynamic data);

    }
}
