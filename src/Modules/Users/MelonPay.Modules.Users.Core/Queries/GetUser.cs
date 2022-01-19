﻿using MelonPay.Shared.Abstractions.Queries;
using MelonPay.Modules.Users.Core.DTO;

namespace MelonPay.Modules.Users.Core.Queries
{
    internal class GetUser : IQuery<UserDetailsDto>
    {
        public Guid UserId { get; set; }
    }
}
