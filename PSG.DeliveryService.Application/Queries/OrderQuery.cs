﻿using MediatR;
using PSG.DeliveryService.Application.Responses;
using ResultMonad;

namespace PSG.DeliveryService.Application.Queries;

public record OrderQuery(Guid Id) : IRequest<Result<OrderResponse>>;