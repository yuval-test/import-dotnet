using Import.APIs.Common;
using Import.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Import.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class SubscriptionTypeFindManyArgs
    : FindManyInput<SubscriptionType, SubscriptionTypeWhereInput> { }
