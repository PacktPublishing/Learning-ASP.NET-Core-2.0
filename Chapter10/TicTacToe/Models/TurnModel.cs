using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public class TurnModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public UserModel User { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Email { get; set; }
        public string IconNumber { get; set; }
    }
}
