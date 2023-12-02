﻿using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Clean.Architecture.UseCases.Contributors.Update;

public record UpdateContributorCommand(int ContributorId, string NewFirstName) : ICommand<Result<ContributorDTO>>;
