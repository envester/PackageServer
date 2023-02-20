using Microsoft.EntityFrameworkCore;
using package_management.Data;

namespace package_management.Repositories
{
    public class PackageRepository : IPackageRepository
    {
        private readonly DatabaseContext _dbContext;

        public PackageRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddPackage(Package package)
        {
            _dbContext.Packages.Add(package);
            _dbContext.SaveChanges();
        }

        public Package GetPackage(int id)
        {
            var data = _dbContext.Packages
        .Include(p => p.Contact)
        .Include(p => p.InfoPackage)
        .FirstOrDefault(p => p.Id == id);
            return data;
        }

        public List<Package> GetPackages()
        {
           return  _dbContext.Packages
       .Include(p => p.Contact)
       .Include(p => p.InfoPackage)
       .ToList();
        }

        public void RemovePackage(int id)
        {
            var package = _dbContext.Packages.FirstOrDefault(p => p.Id == id);
            _dbContext.Packages.Remove(package);
            _dbContext.SaveChanges();
        }

        public void UpdatePackage(Package package)
        {
           _dbContext.Entry(package).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
