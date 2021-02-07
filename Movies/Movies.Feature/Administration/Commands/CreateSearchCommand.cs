using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.Feature.Administration.Commands
{
    public class CreateSearchCommand : IRequest<string>
    {
        public CreateSearchCommand(
            string search_Token,
            string imdbID,
            long processing_Time_Ms,
            string ip_Address)
        {
            Search_Token = search_Token ?? throw new ArgumentNullException(nameof(search_Token));
            ImdbID = imdbID;
            Processing_Time_Ms = processing_Time_Ms;
            Ip_Address = ip_Address ?? throw new ArgumentNullException(nameof(ip_Address));
        }

        [Required]
        public string Search_Token { get; set; }
        public string ImdbID { get; set; }
        [Required]
        public long Processing_Time_Ms { get; set; }
        [Required]
        public string Ip_Address { get; set; }
    }
}
