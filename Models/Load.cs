using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class Load
{
    public int Id { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? EmployeeId { get; set; }

    public double? LoadValue { get; set; }

    public int? StatusId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<LoadsContent> LoadsContents { get; set; } = new List<LoadsContent>(); 

    public virtual Status? Status { get; set; }
}
