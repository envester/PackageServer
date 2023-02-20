using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace package_management.Data
{
    public class Package
    {
        public int Id { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string AddressLine { get; set; }
        public string PostCode { get; set; }
        public RecipientContactModel Contact { get; set; }
        public InfoPackageModel InfoPackage { get; set; }

    }

    public class RecipientContactModel
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class InfoPackageModel
    {
        [Key]
        public int Id { get; set; }
        public long Length { get; set; }
        public long Height { get; set; }
        public long Width { get; set; }
        public long Weight { get; set; }
        public long Volume { get; set; }
    }




}

