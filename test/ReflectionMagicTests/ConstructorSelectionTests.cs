using ReflectionMagic;
using Xunit;

namespace ReflectionMagicTests
{
    public class ConstructorSelectionTests
    {
        [Fact]
        public void CanFindNoArgsConstructor()
        {
            var type = typeof(TypeWithNoArgsConstructor).AsDynamicType();
            var instance = type.New();

            Assert.NotNull(instance);
        }

        [Fact]
        public void CanFindSingleArgumentIntConstructor()
        {
            var type = typeof(TypeWithSingleArgConstructor).AsDynamicType();
            var instance = type.New(1337);

            Assert.NotNull(instance);
            Assert.Equal(1337, instance.IntValue);
        }

        [Fact]
        public void CanFindSingleArgumentStringConstructor()
        {
            var type = typeof(TypeWithSingleArgConstructor).AsDynamicType();
            var instance = type.New("TestValue");

            Assert.NotNull(instance);
            Assert.Equal("TestValue", instance.StringValue);
        }

        [Fact]
        public void CanFindMultipleArgumentConstructor()
        {
            var type = typeof(TypeWithMultipleArgsConstructor).AsDynamicType();
            var instance = type.New(1337, "TestValue");

            Assert.NotNull(instance);
            Assert.Equal(1337, instance.IntValue);
            Assert.Equal("TestValue", instance.StringValue);
        }

        [Fact]
        public void CanHandleNullArgumentToConstructor()
        {
            var type = typeof(TypeWithMultipleArgsConstructor).AsDynamicType();
            var instance = type.New(12, null);

            Assert.NotNull(instance);
            Assert.Equal(12, instance.IntValue);
            Assert.Null(instance.StringValue);
        }

        [Fact(Skip = "Currently not supported")]
        public void CanFindConstructorWithByRefParameter()
        {
            object obj = new object();

            var type = typeof(TypeWithByRefConstructor).AsDynamicType();
            var instance = type.New(ref obj);

            Assert.NotNull(instance);
        }

        private class TypeWithNoArgsConstructor
        {
            public TypeWithNoArgsConstructor()
            {
            }
        }

        private class TypeWithSingleArgConstructor
        {
            public string StringValue { get; }

            public int IntValue { get; }

            public TypeWithSingleArgConstructor(int value)
            {
                IntValue = value;
            }

            public TypeWithSingleArgConstructor(string value)
            {
                StringValue = value;
            }
        }

        private class TypeWithMultipleArgsConstructor
        {
            public string StringValue { get; }

            public int IntValue { get; }

            public TypeWithMultipleArgsConstructor(int value, string stringValue)
            {
                IntValue = value;
                StringValue = stringValue;
            }
        }

        private class TypeWithByRefConstructor
        {
            private object _obj;

            public TypeWithByRefConstructor(ref object obj)
            {
                _obj = obj;
            }
        }
    }
}
