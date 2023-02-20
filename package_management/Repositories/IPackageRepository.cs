using package_management.Data;

namespace package_management.Repositories
{
    public interface IPackageRepository
    {
        List<Package> GetPackages();
        Package GetPackage(int id);
        void AddPackage(Package package);
        void UpdatePackage(Package package);
        void RemovePackage(int id);
    }
}
