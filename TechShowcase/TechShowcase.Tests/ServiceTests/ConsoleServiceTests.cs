﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShowcase.Services;
using Xunit;

namespace TechShowcase.Tests.ServiceTests;

public class ConsoleServiceTests
{
    private readonly ConsoleService _sut;
    public ConsoleServiceTests()
    {
        //_sut = new ConsoleService();
    }

    [Fact]
    public void GivenStartOfApplication_ThenReturnANiceGreeting()
    {

    }
}