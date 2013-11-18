using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Ef
{
    [Table("UserStatistic")]
    public class UserStatistic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [Range(1, 100)]
        public int AgresivityRate { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}
