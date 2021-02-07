using MediatR;
using Movies.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.Feature.Administration.Queries
{
    public class GetApiKeyQuery : IRequest<ApiKeyDto>
    {
        public GetApiKeyQuery(string key)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
        }

        [Required]
        public string Key { get; }
    }
}
