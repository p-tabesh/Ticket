﻿using Ticket.Domain.Enums;

namespace Ticket.Application.Models;

public class AddTicketModel
{

    public string Subject { get; set; }
    public string Body { get; set; }
    public string NationalCode { get; set; }
    public string PhoneNumber { get; set; }
    public int SubmitedUserId { get; set; }
    public int CategoryId { get; set; }
    public Priority Priority { get; set; }
}
