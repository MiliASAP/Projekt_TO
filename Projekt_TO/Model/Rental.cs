using System;
using System.ComponentModel.DataAnnotations.Schema;

public class Rental
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CarId { get; set; }
    public DateTime StartDate { get; set; }
    public int Days { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime EndDate { get; private set; }
}