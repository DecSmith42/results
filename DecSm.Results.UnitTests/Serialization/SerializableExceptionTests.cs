namespace DecSm.Results.UnitTests.Serialization;

internal sealed class SerializableExceptionTests
{
    [Test]
    public void FromException_WithException_ReturnsSerializableException()
    {
        // Arrange
        TestException testEx;

        try
        {
            throw new TestException("Test");
        }
        catch (TestException ex)
        {
            testEx = ex;
        }

        var exceptionType = testEx.GetType()
            .AssemblyQualifiedName;

        // Act
        var serializableException = SerializableException.FromException(testEx);

        // Assert
        serializableException.ShouldSatisfyAllConditions(() => serializableException.ExceptionType.ShouldBe(exceptionType),
            () => serializableException.Message.ShouldBe("Test"),
            () => serializableException.StackTrace.ShouldNotBeNull(),
            () => serializableException.InnerException.ShouldBeNull());
    }

    [Test]
    public void FromException_ToException_ReturnsException()
    {
        // Arrange
        TestException testEx;

        try
        {
            throw new TestException("Test");
        }
        catch (TestException ex)
        {
            testEx = ex;
        }

        var serializableException = SerializableException.FromException(testEx);

        // Act
        var exception = SerializableException.ToException(serializableException);

        // Assert
        exception.ShouldSatisfyAllConditions(() => exception
                .GetType()
                .AssemblyQualifiedName
                .ShouldBe(testEx.GetType()
                    .AssemblyQualifiedName),
            () => exception.Message.ShouldBe("Test"),
            () => exception.StackTrace.ShouldNotBeNull(),
            () => exception.InnerException.ShouldBeNull());
    }

    [Test]
    public void ToException_NonExistentType_FallsBackToException()
    {
        // Arrange
        var serializableException = new SerializableException
        {
            ExceptionType = "NonExistentType",
            Message = "Test",
            StackTrace = "Test",
            InnerException = null,
        };

        // Act
        var exception = SerializableException.ToException(serializableException);

        // Assert
        exception.ShouldSatisfyAllConditions(() => exception
                .GetType()
                .ShouldBe(typeof(Exception)),
            () => exception.Message.ShouldBe("Test"),
            () => exception.StackTrace.ShouldBe("Test"),
            () => exception.InnerException.ShouldBeNull());
    }

    [Test]
    public void ToException_UnConstructableType_FallsBackToNonConstructorCreation()
    {
        // Arrange
        var serializableException = new SerializableException
        {
            ExceptionType = typeof(UnConstructableException).AssemblyQualifiedName!,
            Message = "Test",
            StackTrace = "Test",
            InnerException = null,
        };

        // Act
        var exception = SerializableException.ToException(serializableException);

        // Assert
        exception.ShouldSatisfyAllConditions(() => exception
                .GetType()
                .ShouldBe(typeof(UnConstructableException)),
            () => exception.Message.ShouldBe("Test"),
            () => exception.StackTrace.ShouldBe("Test"),
            () => exception.InnerException.ShouldBeNull());
    }

#pragma warning disable RCS1194 // Test classes
#pragma warning disable CS9113 // Parameter is unread.
    private class TestException(string message) : Exception(message);

    private class UnConstructableException(string message, string ohNo) : Exception(message);
#pragma warning restore CS9113 // Parameter is unread.
#pragma warning restore RCS1194
}
