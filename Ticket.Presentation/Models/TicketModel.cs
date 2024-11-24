using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ticket.Presentation.Models;

public class TicketModel
{   
    public TicketInfo TicketInfo {  get; set; }
    public CustomerInfo CustomerInfo { get; set; }
}

