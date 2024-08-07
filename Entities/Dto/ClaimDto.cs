﻿namespace Entities.Dto;

public class ClaimDto
{
    public string UCR { get; set; }
    public int CompanyId { get; set; }
    public string AssuredName { get; set; }
    public decimal IncurredLoss { get; set; }
    public int ClaimAgeInDays { get; set; }
    public DateTime ClaimDate { get; set; }
    public DateTime LossDate { get; set; }
    public bool Closed { get; set; }
}
