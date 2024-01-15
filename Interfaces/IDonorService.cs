public interface IDonorService
{
    DonorModel CreateDonor(DonorModel donorModel, string branchName, string branchPassword);
    List<DonorModel> GetAllDonors();
    DonorModel GetDonorById(int donorId);
}
